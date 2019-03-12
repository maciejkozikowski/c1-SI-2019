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
            /* wypisuje wszystkie klasy decyzyjne
            for (int i = 0; i < wczytaneDane.Count(); i++)
            {
                string[] tablica = new string[wczytaneDane.Count()];
                tablica[i] = wczytaneDane[i][wczytaneDane[i].Count() - 1];
                Console.Write("\nSymbole decyzyjne to: " + tablica[i]);
            }
            */
            List<string> symbole = new List<string>();
            bool flag = false;

            for (int i = 0; i < wczytaneDane.Count(); i++)
            {
                string[] tablica = new string[wczytaneDane.Count()];
                tablica[i] = wczytaneDane[i][wczytaneDane[i].Count() - 1];
                foreach (string znak in symbole)
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
            Console.WriteLine("Ktoremu atrybutowi wyznaczyc min i max? Podaj cyfre 0-7");
            int numerAtrybutu = int.Parse(Console.ReadLine());
            List<string> wartosciAtrybutu = new List<string>();
            double max = -999999;
            double min = 999999;
            double sumaAtrybutow = 0;
            double sredniaArytmetyczna = 0;
            if (numerAtrybutu >= 0 && numerAtrybutu < 8)
            {
                for (int i = 0; i < atrType.Length; i++)//wiersze
                {
                    for (int j = 0; j < atrType[i].Length; j++)//kolumny
                    {
                        if (i == numerAtrybutu) // zatrzymuje się na odpowiednim miejscu kolumny
                        {
                            for (int k = 0; k < wczytaneDane.Length; k++)// zczytuje dane i dodaje do listy
                            {
                                wartosciAtrybutu.Add(wczytaneDane[k][numerAtrybutu]);
                                    if (atrType[i][1] == "n") //czy jest numeryczny atrybut
                                    {
                                        if (StringToDouble(wczytaneDane[k][numerAtrybutu]) < min) min = StringToDouble(wczytaneDane[k][numerAtrybutu]);
                                        if (StringToDouble(wczytaneDane[k][numerAtrybutu]) > max) max = StringToDouble(wczytaneDane[k][numerAtrybutu]);
                                    sumaAtrybutow = sumaAtrybutow + StringToDouble(wczytaneDane[k][numerAtrybutu]);
                                    }
                            }
                            break;
                        }
                    }
                }
            }
            if (atrType[numerAtrybutu][1] == "n")
            {
                Console.WriteLine("Dla atrybutu :" + numerAtrybutu + " min=" + min + " max=" + max);
            }
            #endregion
            #region dla każdego atrybutu wypisujemy listę wszystkich różnych dostępnych wartości oraz ilosc
            Console.WriteLine("Lista roznych wartosci danego atrybutu oraz ilosc wystapien");
            string[] wszystkieWartosciAtrybutu = new string[wczytaneDane.Length];
            string[] wartosciAtrybutuDistinc = new string[1000];
            int[] zlicz = new int[1000];

            for (int i = 0; i < wczytaneDane.Length; i++)// zczytuje dane i dodaje do listy
            {
               wszystkieWartosciAtrybutu[i]=wczytaneDane[i][numerAtrybutu];
                
            }
            int licznik = 0;
            foreach (string x in wszystkieWartosciAtrybutu.Distinct())//wartosci bez duplikatow
            {
                wartosciAtrybutuDistinc[licznik] = x;
                licznik++;
            }
            for (int i = 0; i < wczytaneDane.Length; i++)
            {

                for (int j = 0; j < wartosciAtrybutuDistinc.Length; j++)
                {
                    if (wartosciAtrybutuDistinc[j] == wczytaneDane[i][numerAtrybutu])
                    {
                        zlicz[j]++;
                    }
                }
            }
            for (int i = 0; i < wartosciAtrybutuDistinc.Length; i++)
            {
                if (zlicz[i] != 0)
                {
                    Console.WriteLine("Wartosc atrybutu " + wartosciAtrybutuDistinc[i] + " wystepuje: " + zlicz[i]);

                }
            }


            #endregion
            #region odchylenie standardowe dla poszczególnych atrybutów w całym systemie i w klasach decyzyjnych(dotyczy atrybutów numerycznych)
            double wariancja = 0;
            double odchylenieStandardowe;
            for(int i = 0; i < wczytaneDane.Length; i++)
            {
                if (atrType[numerAtrybutu][1] == "n")
                {
                    wariancja = wariancja + (StringToDouble(wczytaneDane[i][numerAtrybutu]) - sredniaArytmetyczna) * (StringToDouble(wczytaneDane[i][numerAtrybutu]) - sredniaArytmetyczna);
                }
            }
            odchylenieStandardowe = Math.Sqrt(wariancja / wczytaneDane.Length);
            Console.WriteLine("Odchylenie standardowe= " + odchylenieStandardowe);

            #endregion

            #region Wygeneruj 10 procent wartości nieznanych, wpisując na miejsce danych znak zapytania i napraw metodą szukania najczęściej wystepującej wartości, lub wartością średnią(dla atrybutów numerycznych)

            Random rand = new Random();
            
            Console.Write("{0}\n", rand.Next(100));
            for(int i = 0; i < wczytaneDane.Length; i++)
            {
                for (int j = 0; j < wczytaneDane[i].Length; j++)
                {
                    if (rand.Next(10) == 0)
                    {
                        wczytaneDane[i][j] = "?";
                    }
                }
            }

            Console.WriteLine("\nDane systemu po wygenerowaniu 10 procent wartości nieznanych");
            Console.ReadKey();
            wynik = TablicaDoString(wczytaneDane);
            Console.Write(wynik);

            //naprawianie
            double[] srednia = new double[wczytaneDane.Length];
            double temp = 0;
            for (int i = 0; i < srednia.Length; i++)
            {
                srednia[i] = 0;
            }
            for (int i = 0; i < wczytaneDane.Length; i++)
            {
                for (int j = 0; j < wczytaneDane[i].Length; j++)
                {
                    if (wczytaneDane[i][j] != "?")
                    {
                        double.TryParse(wczytaneDane[i][j], out temp);
                        srednia[i] += temp;
                    }

                }
                srednia[i] = srednia[i] / srednia.Length;
            }

            for (int i = 0; i < wczytaneDane.Length; i++)
            {
                for (int j = 0; j < wczytaneDane[i].Length; j++)
                {
                    if (wczytaneDane[i][j] == "?")
                    {
                        wczytaneDane[i][j] = srednia[j].ToString();
                    }
                }
            }
            Console.WriteLine("\nDane systemu po naprawieniu");
            Console.ReadKey();
            wynik = TablicaDoString(wczytaneDane);
            Console.Write(wynik);
            #endregion



            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
    }
}
