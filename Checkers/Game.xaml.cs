using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Checkers
{
    /// <summary>
    /// Interakční logika pro Window1.xaml
    /// </summary>

    #region Úkoly
    // zprovoznit dámu
    // implementovat testy (3)
    // dodělat více tahů kamenem
    // oběktově orientovaný kód
    // dodělat AI
    // dokončit zbylé funkce
    // update na github
    #endregion

    public partial class Window1 : Window
    {
        Button vybranyKamen = null;
        int kolo = 0;
        List<Button> Reds = new List<Button>();
        List<Button> Blues = new List<Button>();
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan stopky = new TimeSpan();

        public Window1()
        {
            InitializeComponent();
            Zapis();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            labelHrac1veHre.Content = Reds.Count;
            labelHrac2veHre.Content = Blues.Count;
            labelHrac1vyrazeno.Content = 12 - Reds.Count;
            labelHrac2vyrazeno.Content = 12 - Blues.Count;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelCasUkazatel.Content = stopky;
            stopky += TimeSpan.FromSeconds(1);
        }

        private void ButKonec_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.OK == MessageBox.Show("Opravdu chcete hru ukončit?", "Dotaz", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification))
                this.Close();
        }

        private void KamenClick(object sender)
        {
            try { vybranyKamen.Background = null; }
            catch (Exception) { }
            vybranyKamen = (Button)sender;
            vybranyKamen.Background = Brushes.LimeGreen;
        }

        private void Posun(Rectangle pole)
        {
            if (vybranyKamen != null)
            {
                string barva = vybranyKamen.Name.Split('_')[0];

                if (//ValidaceSkokuDama(vybranyKamen, pole, barva) ||
                    //ValidaceKrokuDama(vybranyKamen,pole,barva) || 
                    ValidaceSkoku(vybranyKamen, pole, barva) ||
                    ValidaceKroku(vybranyKamen, pole, barva))
                {
                    bool validace = ValidaceSkoku(vybranyKamen, pole, barva);

                    //int puvodniRow = Grid.GetRow(vybranyKamen);
                    //int puvodniColum = Grid.GetColumn(vybranyKamen);

                    Log(vybranyKamen, pole, barva);

                    int novyRow = Grid.GetRow(pole);
                    int novyColum = Grid.GetColumn(pole);

                    Grid.SetRow(vybranyKamen, novyRow);
                    Grid.SetColumn(vybranyKamen, novyColum);

                    #region Pokusy s více tahy
                    /////////////////////
                    //if (validace)
                    //{
                    //    Rectangle pole2 = new Rectangle();
                    //    Rectangle pole3 = new Rectangle();
                    //    int posun = barva == "Red" ? -2 : 2;

                    //    Grid.SetRow(pole2, Grid.GetRow(pole) + posun);
                    //    Grid.SetColumn(pole2, Grid.GetColumn(pole) + 2);

                    //    Grid.SetRow(pole3, Grid.GetRow(pole) + posun);
                    //    Grid.SetColumn(pole3, Grid.GetColumn(pole) - 2);

                    //    novyRow = Grid.GetRow(pole2);
                    //    novyColum = Grid.GetColumn(pole3);

                    //    if (ValidaceSkoku(vybranyKamen, pole2, barva))
                    //    {
                    //        MessageBox.Show("zprava");
                    //    } 
                    //    else if (ValidaceSkoku(vybranyKamen, pole3, barva))
                    //    {
                    //        MessageBox.Show("zleva");
                    //    }
                    //}
                    /////////////////
                    #endregion

                    

                    JeKralovna(barva);
                    kolo++;
                    vybranyKamen.Background = null;
                    vybranyKamen = null;

                    Info();
                }

            }
        }

        private void Info()
        {
            labelHrac1veHre.Content = Reds.Count;
            labelHrac2veHre.Content = Blues.Count;
            labelHrac1vyrazeno.Content = 12 - Reds.Count;
            labelHrac2vyrazeno.Content = 12 - Blues.Count;

            if (Reds.Count == 0)
            {
                MessageBox.Show("Vyhrál modrý!!", "Výhra", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                this.Close();
            }
            else if (Blues.Count == 0)
            {
                MessageBox.Show("Vyhrál červený!!", "Výhra", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                this.Close();
            }
        }

        private void Log(Button kamen, Rectangle pole, string barva)
        {
            int puvodniRow = Grid.GetRow(vybranyKamen);
            int puvodniColum = Grid.GetColumn(vybranyKamen);

            int novyRow = Grid.GetRow(pole);
            int novyColum = Grid.GetColumn(pole);

            string puvodniRadek = "", novyRadek = "";

            if (puvodniRow == 1) puvodniRadek = "H";
            else if (puvodniRow == 2) puvodniRadek = "G";
            else if (puvodniRow == 3) puvodniRadek = "F";
            else if (puvodniRow == 4) puvodniRadek = "E";
            else if (puvodniRow == 5) puvodniRadek = "D";
            else if (puvodniRow == 6) puvodniRadek = "C";
            else if (puvodniRow == 7) puvodniRadek = "B";
            else if (puvodniRow == 8) puvodniRadek = "A";

            if (novyRow == 1) novyRadek = "H";
            else if (novyRow == 2) novyRadek = "G";
            else if (novyRow == 3) novyRadek = "F";
            else if (novyRow == 4) novyRadek = "E";
            else if (novyRow == 5) novyRadek = "D";
            else if (novyRow == 6) novyRadek = "C";
            else if (novyRow == 7) novyRadek = "B";
            else if (novyRow == 8) novyRadek = "A";

            string vypis = $"{barva,-4} - {puvodniRadek}{puvodniColum} > {novyRadek}{novyColum}" + Environment.NewLine;


            textBoxLog.Text = vypis + textBoxLog.Text;

        }

        private void Vymazat(Button kamen, Rectangle pole)
        {
            Point skoceny;

            int puvodniRow = Grid.GetRow(kamen);
            int puvodniColum = Grid.GetColumn(kamen);

            int novyRow = Grid.GetRow(pole);
            int novyColum = Grid.GetColumn(pole);

            //string barva = kamen.Name.Split('_')[0];

            skoceny = new Point((puvodniColum + novyColum) / 2, (puvodniRow + novyRow) / 2);

            foreach (Button item in Reds)
            {
                if (Grid.GetRow(item) == skoceny.Y && Grid.GetColumn(item) == skoceny.X)
                {
                    Reds.Remove(item);
                    item.Visibility = Visibility.Hidden;
                    return;
                }
            }
            foreach (Button item in Blues)
            {
                if (Grid.GetRow(item) == skoceny.Y && Grid.GetColumn(item) == skoceny.X)
                {
                    Blues.Remove(item);
                    item.Visibility = Visibility.Hidden;
                    return;
                }
            }

        }

        private bool ValidaceSkokuDama(Button kamen, Rectangle pole, string barva)
        {
            Point poziceStart = new Point(Grid.GetColumn(kamen), Grid.GetRow(kamen));
            Point poziceCíl = new Point(Grid.GetColumn(pole), Grid.GetRow(pole));

            if (kamen.Tag.ToString() == "queen" &&
                Math.Abs(poziceStart.X - poziceCíl.X) == Math.Abs(poziceStart.Y - poziceCíl.Y))
            {
                if (true)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        private bool ValidaceKrokuDama(Button kamen, Rectangle pole, string barva)
        {
            Point poziceStart = new Point(Grid.GetColumn(kamen), Grid.GetRow(kamen));
            Point poziceCíl = new Point(Grid.GetColumn(pole), Grid.GetRow(pole));

            bool volno = true;

            if (kamen.Tag.ToString() == "queen" &&
                Math.Abs(poziceStart.X - poziceCíl.X) == Math.Abs(poziceStart.Y - poziceCíl.Y)) //ověření pohybu po diagonále
            {
                int vzdalenost = Convert.ToInt32(poziceCíl.X - poziceCíl.X); //vzdálenost o jakou se chce posunout

                if (poziceCíl.Y < poziceStart.Y && poziceCíl.X < poziceStart.X) //zda se posouvá nahoru doleva
                {
                    for (int i = 0; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
                    {
                        foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
                            if (p.X == poziceStart.X - i && p.Y == poziceStart.Y - i) //kontrola zda je kamen na cíli nebo na trae
                            {
                                volno = false;
                                return volno;
                            }
                        }
                        foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                            if (p.X == poziceStart.X - i && p.Y == poziceStart.Y - i)
                            {
                                volno = false;
                                return volno;
                            }
                        }
                    }
                }
                if (poziceCíl.Y < poziceStart.Y && poziceCíl.X > poziceStart.X) //zda se posouvá nahoru doprava
                {
                    for (int i = 0; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
                    {
                        foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
                            if (p.X == poziceStart.X - i && p.Y == poziceStart.Y + i) //kontrola zda je kamen na cíli nebo na trae
                            {
                                volno = false;
                                return volno;
                            }
                        }
                        foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                            if (p.X == poziceStart.X - i && p.Y == poziceStart.Y + i)
                            {
                                volno = false;
                                return volno;
                            }
                        }
                    }
                }
                if (poziceCíl.Y > poziceStart.Y && poziceCíl.X < poziceStart.X) //zda se posouvá dolů doleva
                {
                    for (int i = 0; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
                    {
                        foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
                            if (p.X == poziceStart.X + i && p.Y == poziceStart.Y - i) //kontrola zda je kamen na cíli nebo na trae
                            {
                                volno = false;
                                return volno;
                            }
                        }
                        foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                            if (p.X == poziceStart.X + i && p.Y == poziceStart.Y - i)
                            {
                                volno = false;
                                return volno;
                            }
                        }
                    }
                }
                if (poziceCíl.Y > poziceStart.Y && poziceCíl.X > poziceStart.X) //zda se posouvá dolů doprava
                {
                    for (int i = 0; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
                    {
                        foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
                            if (p.X == poziceStart.X + i && p.Y == poziceStart.Y + i) //kontrola zda je kamen na cíli nebo na trae
                            {
                                volno = false;
                                return volno;
                            }
                        }
                        foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
                        {
                            Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                            if (p.X == poziceStart.X + i && p.Y == poziceStart.Y + i)
                            {
                                volno = false;
                                return volno;
                            }
                        }
                    }
                }

            }

            return volno;
        }

        private bool ValidaceKroku(Button kamen, Rectangle pole, string barva)
        {
            Point poziceStart = new Point(Grid.GetColumn(kamen), Grid.GetRow(kamen));
            Point poziceCíl = new Point(Grid.GetColumn(pole), Grid.GetRow(pole));

            bool mozno = false;

            foreach (Button item in Reds)
            {
                Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                if (p == poziceCíl)
                {
                    mozno = false;
                    return mozno;
                }
            }

            foreach (Button item in Blues)
            {
                Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                if (p == poziceCíl)
                {
                    mozno = false;
                    return mozno;
                }
            }

            if (Math.Abs(poziceCíl.Y - poziceStart.Y) == 1 &&
                poziceCíl.Y < poziceStart.Y && 
                barva == "Red" && 
                (poziceCíl.X - 1 == poziceStart.X || poziceCíl.X + 1 == poziceStart.X))
                 {
                    mozno = true;
                    return mozno;
                 }

            else if (Math.Abs(poziceCíl.Y - poziceStart.Y) == 1 && 
                poziceCíl.Y > poziceStart.Y && 
                barva == "Blue" && 
                (poziceCíl.X - 1 == poziceStart.X || poziceCíl.X + 1 == poziceStart.X))
                 {
                    mozno = true;
                    return mozno;
            }

            else
            {
                mozno = false;
                return mozno;
            }
        }

        private bool ValidaceSkoku(Button kamen, Rectangle pole, string barva)
        {
            Point poziceStart = new Point(Grid.GetColumn(kamen), Grid.GetRow(kamen));
            Point poziceCíl = new Point(Grid.GetColumn(pole), Grid.GetRow(pole));

            bool nalezeno = false;

            foreach (Button item in Reds)
            {
                Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                if (p == poziceCíl) nalezeno = true;
            }

            foreach (Button item in Blues)
            {
                Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
                if (p == poziceCíl) nalezeno = true;
            }


            if (!nalezeno)
            {
                for (int i = 0; i < Reds.Count; i++)
                {
                    Point kamenNepritel = new Point(Grid.GetColumn(Reds[i]), Grid.GetRow(Reds[i]));
                    if (barva == "Blue" && kamenNepritel.Y == poziceStart.Y + 1 && (kamenNepritel.X == poziceStart.X + 1 || kamenNepritel.X == poziceStart.X - 1) && poziceCíl.Y == kamenNepritel.Y + 1 && (poziceCíl.X == kamenNepritel.X - 1 || poziceCíl.X == kamenNepritel.X + 1))
                    {
                        Vymazat(vybranyKamen, pole);
                        return true;
                    }
                }
                for (int i = 0; i < Blues.Count; i++)
                {
                    Point kamenNepritel = new Point(Grid.GetColumn(Blues[i]), Grid.GetRow(Blues[i]));
                    if (barva == "Red" && kamenNepritel.Y == poziceStart.Y - 1 && (kamenNepritel.X == poziceStart.X + 1 || kamenNepritel.X == poziceStart.X - 1) && poziceCíl.Y == kamenNepritel.Y - 1 && (poziceCíl.X == kamenNepritel.X - 1 || poziceCíl.X == kamenNepritel.X + 1))
                    {
                        Vymazat(vybranyKamen, pole);
                        return true;
                    }
                } 
            }
            return false;
        }

        private void JeKralovna(string barva)
        {
            if (barva == "Red" && Grid.GetRow(vybranyKamen) == 1 && vybranyKamen.Tag.ToString() != "queen")
            {
                vybranyKamen.Content = new Image 
                {
                    Source = new BitmapImage(new Uri("../../Images/kamen-red-queen.png", UriKind.Relative))
                };
                vybranyKamen.Tag = "queen";  
            }
            if (barva == "Blue" && Grid.GetRow(vybranyKamen) == 8 && vybranyKamen.Tag.ToString() != "queen")
            {
                vybranyKamen.Content = new Image
                {
                    Source = new BitmapImage(new Uri("../../Images/kamen-blue-queen.png", UriKind.Relative))
                };
                vybranyKamen.Tag = "queen";
            }
        }

        private void PoleClick(object sender, MouseButtonEventArgs e)
        {
            Posun((Rectangle)sender);
        }

        private void RedClick(object sender, RoutedEventArgs e)
        {
            if (kolo % 2 == 0)
            {
                KamenClick(sender);
            }
            else
            {
                MessageBox.Show("Na řadě je modrý hráč!", "Varování", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BlueClick(object sender, RoutedEventArgs e)
        {
            if (kolo % 2 == 1)
            {
                KamenClick(sender);
            }
            else
            {
                MessageBox.Show("Na řadě je červený hráč!", "Varování", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Zapis()
        {
            Reds.Add(Red_Kamen_01);
            Reds.Add(Red_Kamen_02);
            Reds.Add(Red_Kamen_03);
            Reds.Add(Red_Kamen_04);
            Reds.Add(Red_Kamen_05);
            Reds.Add(Red_Kamen_06);
            Reds.Add(Red_Kamen_07);
            Reds.Add(Red_Kamen_08);
            Reds.Add(Red_Kamen_09);
            Reds.Add(Red_Kamen_10);
            Reds.Add(Red_Kamen_11);
            Reds.Add(Red_Kamen_12);

            Blues.Add(Blue_Kamen_01);
            Blues.Add(Blue_Kamen_02);
            Blues.Add(Blue_Kamen_03);
            Blues.Add(Blue_Kamen_04);
            Blues.Add(Blue_Kamen_05);
            Blues.Add(Blue_Kamen_06);
            Blues.Add(Blue_Kamen_07);
            Blues.Add(Blue_Kamen_08);
            Blues.Add(Blue_Kamen_09);
            Blues.Add(Blue_Kamen_10);
            Blues.Add(Blue_Kamen_11);
            Blues.Add(Blue_Kamen_12);
        }

    }
}
