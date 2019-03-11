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
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
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
            string nazwaPlikuZDanymi = @"F:\Moje Programy\Sztuczna Ćw1\Ćw1\Ćw1\dane\heartdisease.txt";
            string nazwaPlikuZTypamiAtrybutow = @"F:\Moje Programy\Sztuczna Ćw1\Ćw1\Ćw1\dane\heartdisease-type.txt";

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
            int rozmiar = wczytaneDane[0].Count() - 1;                                          //rozmiar systemu (ile atrybotow)
            int wlkSys = wczytaneDane.Count();                                                  //wielkosc systemu calego (ile obiektow)
            List<string> symbole = new List<string>();                                          //symbole na msc w systemie
            List<double> max = new List<double>();                                              //max wartosci (tylko liczby) 
            List<double> min = new List<double>();                                              //min wartosci (tylko liczby)
            List<List<double>> atrDouble = new List<List<double>>();                            //atrybuty liczbowe
            List<int> ktore_to_numeryczne = new List<int>();                                    //lista przechowujaca wiadomosci ktory atrybut byl liczba
            List<List<string>> rozne_znaki = new List<List<String>>();
            
            //----------------------------------------------------------------------------
            Console.WriteLine("\nKolejne symbole:");
            for (int indZnaku = 0; indZnaku < rozmiar; indZnaku++)
            {
                symbole.Add(atrType[indZnaku][1]);
                Console.Write(symbole[indZnaku]);
            }
            //----------------------------------------------------------------------------

            int help = 0;
            for (int i = 0; i < rozmiar; i++) //rozmiar = 13

            {
                if (symbole[i] == "n")
                {
                    atrDouble.Add(new List<Double>());
                    ktore_to_numeryczne.Add(i+1);
                    for (int j = 0; j < wlkSys; j++)
                    {
                        atrDouble[help].Add(StringToDouble(wczytaneDane[j][i]));
                    }
                    help++;
                }
            }
            //----------------------------------------------------------------------------

            /*
            d) dla kazdego atrybutu wypisujemy liczbe róznych dostepnych wartosci 
            e) dla kazdego atrybutu wypisujemy liste wszystkich róznych dostepnych wartosci
            */
            for (int j = 0; j < rozmiar; j++)
            {
                rozne_znaki.Add(new List<string>());
            }
            
            int c = 1;
            for (int j = 0; j < rozmiar; j++) //lewo prawo (1)
            {
                help = 0;
                for (int i = 0; i < wlkSys; i++) //góra dół (1)
                {
                    c = 1;
                    if (i==0)
                    {
                        rozne_znaki[j].Add(wczytaneDane[i][j]);
                        help++;
                    }
                    for (int k = 0; k < help; k++)
                    {
                        //Console.WriteLine(rozne_znaki[j][i] + "-" + wczytaneDane[i][j]);
                        if (rozne_znaki[j][k] == wczytaneDane[i][j])
                        {
                            c = 0;
                            break;
                        }
                        
                    }
                    if (c == 1)
                    {
                        rozne_znaki[j].Add(wczytaneDane[i][j]);
                        help++;
                    }

                }
            }

            //----------------------------------------------------------------------------











            //------------------Wypisanie danych-----------------------------

            for (int i = 0; i < atrDouble.Count(); i++)
            {
                min.Add(atrDouble[i].Min());
                max.Add(atrDouble[i].Max());
                Console.WriteLine("\nAtrybut nr:" + ktore_to_numeryczne[i]);
                Console.WriteLine("Min:" + min[i]);
                Console.WriteLine("Max:" + max[i]);
            }
            

            Console.WriteLine("\nRozmiar systemu:" + rozmiar);
            Console.WriteLine("\nWielkosc systemu:" + wlkSys);
            for (int i = 0; i < rozne_znaki.Count() ; i++)
            {
                for (int j = 0; j < rozne_znaki[i].Count() ; j++)
                {
                    Console.WriteLine(rozne_znaki[i][j]);
                }
                Console.WriteLine("Ilosc roznych znakow: " + rozne_znaki[i].Count());
            }
            //---------------------------------------------------------------

            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
    }
}
