using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuConsole
{
    class Program
    {
        static string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)//wiersze
            {
                for (int j = 0; j < tab[i].Length; j++)//kolumny
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }
            return wynik;
        }

        static double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");

            return wynik;
        }


        static int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");
            return wynik;
        }

        static string[][] StringToTablica(string sciezkaDoPliku)
        {
            string trescPliku = System.IO.File.ReadAllText(sciezkaDoPliku); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            string[][] wczytaneDane = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                wczytaneDane[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    wczytaneDane[i][j] = cyfra;
                }
            }
            return wczytaneDane;
        }

        static void Main(string[] args)
        {
            string nazwaPlikuZDanymi = @"diabetes.txt";
            string nazwaPlikuZTypamiAtrybutow = @"diabetes-type.txt";

            string[][] wczytaneDane = StringToTablica(nazwaPlikuZDanymi);
            string[][] atrType = StringToTablica(nazwaPlikuZTypamiAtrybutow);

            Console.WriteLine("Dane systemu");
            string wynik = TablicaDoString(wczytaneDane);
            Console.Write(wynik);

            Console.WriteLine("");
            Console.WriteLine("Dane pliku z typami");

            string wynikAtrType = TablicaDoString(atrType);
            Console.Write(wynikAtrType);

            /****************** Miejsce na rozwiązanie *********************************/
            #region wypisujemy istniejące w systemie symbole klas decyzyjnych
            Console.WriteLine(wczytaneDane.Count() + " " + (wczytaneDane[0].Count() - 1));
            List<string> symbole = new List<string>();
            bool flag = false;

            for (int i = 0; i < wczytaneDane.Count(); i++)
            {
                string[] tablica = new string[wczytaneDane.Count()];
                tablica[i] = wczytaneDane[i][wczytaneDane[i].Count() - 1];
                // Console.Write(wczytaneDane[i][wczytaneDane[0].Count() - 1]);
                //Console.Write("\nSymbole decyzyjne to: " + tablica[i]);
                foreach(string znak in symbole)
                {
                    if (znak == tablica[i])
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    symbole.Add(tablica[i]);
                }
            }
            foreach (string znak in symbole)
            {
                Console.Write("\nSymbole decyzyjne to: " + znak);
            }            
            #endregion

            #region wielkości klas decyzyjnych (liczby obiektów w klasach)
            int prawda = 0;
            int falsz = 0;
                for (int i = 0; i < wczytaneDane.Count(); i++) //zlicza wystapienia 0 i 1
                    {
                    if (wczytaneDane[i][wczytaneDane[0].Count() - 1] == "1") prawda++;
                    else falsz++;
                }
                Console.WriteLine("\nWielkosc klasy 0 jest rowna: "+falsz+" a klasy 1 jest rowna: "+prawda);
            #endregion

            #region minimalne i maksymalne wartości poszczególnych atrybutów(dotyczy atrybutów numerycznych) 
                      
                int[] tabInt=new int[wczytaneDane.Count() - 1];
                double[] tabDouble = new double[wczytaneDane.Count() - 1];
            int licznik = wczytaneDane[0].Count() - 2;//ilość kolumn do przeszukania wszystkich atrybutów bez ostatniego decyzyjnego
            while (licznik > 0)//przechodzi po wszystkich kolumnach
            {
                
                for (int i = 0; i < wczytaneDane.Count() - 1; i++)//zamienia stringi na liczby INT do tablicy
                {
                    int liczba = StringToInt(wczytaneDane[i][0]);
                    //int liczba1 = StringToInt(wczytaneDane[i][1]);
                    tabInt[i] = liczba;
                }
                int tmp;
                for (int i = 0; i < tabInt.Length - 1; i++)//sortuje tablice
                {
                    for (int j = 0; j < tabInt.Length - 1; j++)
                    {
                        if (tabInt[j] > tabInt[j + 1])
                        {
                            tmp = tabInt[j];
                            tabInt[j] = tabInt[j + 1];
                            tabInt[j + 1] = tmp;
                        }
                    }
                }
                Console.WriteLine("Min int to: " + tabInt[0] + " Max int to: " + tabInt[tabInt.Length - 1]);


                for (int i = 0; i < wczytaneDane.Count() - 1; i++)//zamienia stringi na liczby DOUBLE do tablicy
                {
                    double liczba = StringToDouble(wczytaneDane[i][5]);
                    //int liczba1 = StringToInt(wczytaneDane[i][1]);
                    tabDouble[i] = liczba;
                }
                double tmp1;
                for (int i = 0; i < tabDouble.Length - 1; i++)//sortuje tablice
                {
                    for (int j = 0; j < tabDouble.Length - 1; j++)
                    {
                        if (tabDouble[j] > tabDouble[j + 1])
                        {
                            tmp1 = tabDouble[j];
                            tabDouble[j] = tabDouble[j + 1];
                            tabDouble[j + 1] = tmp1;
                        }
                    }
                }
                Console.WriteLine("Min double to: " + tabDouble[0] + " Max double to: " + tabDouble[tabInt.Length - 1]);
                licznik--;

            }





            #endregion

            #region dla każdego atrybutu wypisujemy liczbę różnych dostępnych wartości



            #endregion
            #region dla każdego atrybutu wypisujemy listę wszystkich różnych dostępnych wartości



            #endregion

            #region odchylenie standardowe dla poszczególnych atrybutów w całym systemie i w klasach decyzyjnych(dotyczy atrybutów numerycznych)



            #endregion





            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
    }
}
