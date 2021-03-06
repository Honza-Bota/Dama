﻿using System;
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
    #region Úkoly
    // dodělat více tahů kamenem
    // všechny pravidla dámy
    // optimalizace kódu
    // update na github
    //----------------------------
    // dodělat AI
    // dokončit zbylé funkce
    #endregion

    public partial class Window1 : Window
    {
        Game Hra; 
        public Window1()
        {
            InitializeComponent();
            Hra = new Game();
            Hra.FormGame = this;
            Hra.LoadStones();
            
            Hra.Timer.Interval = TimeSpan.FromSeconds(1);
            Hra.Timer.Tick += Hra.Timer_Tick;
            Hra.Timer.Start(); 

            labelHrac1veHre.Content = Hra.Reds.Count;
            labelHrac2veHre.Content = Hra.Blues.Count;
            labelHrac1vyrazeno.Content = 12 - Hra.Reds.Count;
            labelHrac2vyrazeno.Content = 12 - Hra.Blues.Count;
        }

        public void ButKonec_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Opravdu chcete hru ukončit?", "Dotaz", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification))
                this.Close();
        }
        public void PoleClick(object sender, MouseButtonEventArgs e)
        {
            Hra.Move((Rectangle)sender);
        } 
        public void RedClick(object sender, RoutedEventArgs e)
        {
            if (Hra.Turn % 2 == 0)
            {
                Hra.StoneClick(sender);
            }
            else
            {
                MessageBox.Show("Na řadě je modrý hráč!", "Špatný tah", MessageBoxButton.OK);
            }
        } 
        public void BlueClick(object sender, RoutedEventArgs e)
        {
            if (Hra.Turn % 2 == 1)
            {
                Hra.StoneClick(sender);
            }
            else
            {
                MessageBox.Show("Na řadě je červený hráč!", "Špatný tah", MessageBoxButton.OK);
            }
        } 

    }

    public class Game
    {
        public Window1 FormGame { get; set; } = null;
        public Button SelectedStone { get; set; } = null;
        public List<Button> Reds { get; set; } = new List<Button>();
        public List<Button> Blues { get; set; } = new List<Button>();
        public DispatcherTimer Timer { get; set; } = new DispatcherTimer();
        public TimeSpan GameTime { get; set; } = new TimeSpan();
        public int Turn { get; set; } = 0;

        public void StoneClick(object sender)
        {
            try { SelectedStone.Background = null; }
            catch (Exception) { } //zde občas nastává chyba, je zakázána pro funkčnost, v předchozí verzi visual studia bez problému
            SelectedStone = (Button)sender;
            SelectedStone.Background = Brushes.LimeGreen;
        }
        public void Move(Rectangle field)
        {
            if (SelectedStone != null)
            {
                string barva = SelectedStone.Name.Split('_')[0];

                if (ValidationJumpQueen(SelectedStone, field, barva) ||
                    ValidationMoveQueen(SelectedStone, field, barva) ||
                    ValidationJump(SelectedStone, field, barva) ||
                    ValidationMove(SelectedStone, field, barva))
                {
                    bool validace = ValidationJump(SelectedStone, field, barva);

                    //int puvodniRow = Grid.GetRow(vybranyKamen);
                    //int puvodniColum = Grid.GetColumn(vybranyKamen);

                    Log(field, barva);

                    int novyRow = Grid.GetRow(field);
                    int novyColum = Grid.GetColumn(field);

                    Grid.SetRow(SelectedStone, novyRow);
                    Grid.SetColumn(SelectedStone, novyColum);

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



                    IsQueen(barva);
                    Turn++;
                    SelectedStone.Background = null;
                    SelectedStone = null;

                    Info();

                    //System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Sliding);
                    //player.Play();
                }

            }
        }
        public void Delete(Button stone, Rectangle field)
        {
            Point skoceny;

            int puvodniRow = Grid.GetRow(stone);
            int puvodniColum = Grid.GetColumn(stone);

            int novyRow = Grid.GetRow(field);
            int novyColum = Grid.GetColumn(field);

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
        public bool ValidationJumpQueen(Button stone, Rectangle field, string color)
        {

            Point poziceStart = new Point(Grid.GetColumn(stone), Grid.GetRow(stone));
            Point poziceCíl = new Point(Grid.GetColumn(field), Grid.GetRow(field));

            bool nalezeno = false;

            nalezeno = Occupied(poziceCíl);

            if (!nalezeno && SelectedStone.Tag.ToString() == "queen")
            {
                for (int i = 0; i < Reds.Count; i++)
                {
                    Point kamenNepritel = new Point(Grid.GetColumn(Reds[i]), Grid.GetRow(Reds[i]));

                    if (color == "Blue" &&
                        poziceStart.X != poziceCíl.X &&
                        Math.Abs(poziceCíl.X - poziceStart.X) == 2 &&
                        poziceCíl.Y != poziceStart.Y &&
                        (kamenNepritel.Y == poziceStart.Y + 1 || kamenNepritel.Y == poziceStart.Y - 1) &&
                        (kamenNepritel.X == poziceStart.X + 1 || kamenNepritel.X == poziceStart.X - 1) &&
                        (poziceCíl.Y == kamenNepritel.Y + 1 || poziceCíl.Y == kamenNepritel.Y - 1) &&
                        (poziceCíl.X == kamenNepritel.X - 1 || poziceCíl.X == kamenNepritel.X + 1))
                    {
                        Delete(SelectedStone, field);
                        return true;
                    }
                }
                for (int i = 0; i < Blues.Count; i++)
                {
                    Point kamenNepritel = new Point(Grid.GetColumn(Blues[i]), Grid.GetRow(Blues[i]));

                    if (color == "Red" &&
                        poziceStart.X != poziceCíl.X &&
                        Math.Abs(poziceCíl.X - poziceStart.X) == 2 &&
                        poziceCíl.Y != poziceStart.Y &&
                        (kamenNepritel.Y == poziceStart.Y - 1 || kamenNepritel.Y == poziceStart.Y + 1) &&
                        (kamenNepritel.X == poziceStart.X + 1 || kamenNepritel.X == poziceStart.X - 1) &&
                        (poziceCíl.Y == kamenNepritel.Y - 1 || poziceCíl.Y == kamenNepritel.Y + 1) &&
                        (poziceCíl.X == kamenNepritel.X - 1 || poziceCíl.X == kamenNepritel.X + 1))
                    {
                        Delete(SelectedStone, field);
                        return true;
                    }
                }
            }
            return false;
        }
        public bool ValidationMoveQueen(Button stone, Rectangle field, string color)
        {

            Point poziceStart = new Point(Grid.GetColumn(stone), Grid.GetRow(stone));
            Point poziceCíl = new Point(Grid.GetColumn(field), Grid.GetRow(field));

            bool mozno = false;

            if (SelectedStone.Tag.ToString() == "queen")
            {
                mozno = Occupied(poziceCíl) == true ? false : true;
                if (mozno == false) return mozno;

                if (Math.Abs(poziceCíl.Y - poziceStart.Y) == 1 &&
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
            else
            {
                mozno = false;
                return mozno;
            }


            #region Původní pokus

            //    Point poziceStart = new Point(Grid.GetColumn(kamen), Grid.GetRow(kamen));
            //    Point poziceCíl = new Point(Grid.GetColumn(pole), Grid.GetRow(pole));

            //    bool volno = false;
            //    bool mozno = true;

            //    foreach (Button item in Reds)
            //    {
            //        Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
            //        if (p == poziceCíl)
            //        {
            //            mozno = false;
            //        }
            //    }
            //    foreach (Button item in Blues)
            //    {
            //        Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
            //        if (p == poziceCíl)
            //        {
            //            mozno = false;
            //        }
            //    }

            //    if (kamen.Tag.ToString() == "queen" &&
            //        mozno &&
            //        Math.Abs(poziceStart.X - poziceCíl.X) == Math.Abs(poziceStart.Y - poziceCíl.Y)) //ověření pohybu po diagonále
            //    {
            //        int vzdalenost = Convert.ToInt32(Math.Abs(poziceCíl.X - poziceStart.X)); //vzdálenost o jakou se chce posunout
            //        volno = true;
            //        //MessageBox.Show("Královna" + volno);

            //        if (poziceCíl.Y < poziceStart.Y && poziceCíl.X < poziceStart.X) //zda se posouvá nahoru doleva
            //        {
            //            for (int i = 1; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
            //            {
            //                foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
            //                    if (p.X == poziceStart.X - i && p.Y == poziceStart.Y - i) //kontrola zda je kamen na cíli nebo na trae
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //                foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
            //                    if (p.X == poziceStart.X - i && p.Y == poziceStart.Y - i)
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //            }
            //            //MessageBox.Show("LevoHore " + volno);

            //        }
            //        else if (poziceCíl.Y < poziceStart.Y && poziceCíl.X > poziceStart.X) //zda se posouvá nahoru doprava
            //        {
            //            for (int i = 1; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
            //            {
            //                foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
            //                    if (p.X == poziceStart.X - i && p.Y == poziceStart.Y + i) //kontrola zda je kamen na cíli nebo na trae
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //                foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
            //                    if (p.X == poziceStart.X - i && p.Y == poziceStart.Y + i)
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //            }
            //            //MessageBox.Show("PravoHore " + volno);

            //        }
            //        else if (poziceCíl.Y > poziceStart.Y && poziceCíl.X < poziceStart.X) //zda se posouvá dolů doleva
            //        {
            //            for (int i = 1; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
            //            {
            //                foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
            //                    if (p.X == poziceStart.X + i && p.Y == poziceStart.Y - i) //kontrola zda je kamen na cíli nebo na trae
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //                foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
            //                    if (p.X == poziceStart.X + i && p.Y == poziceStart.Y - i)
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //            }
            //            //MessageBox.Show("LevoDole " + volno);

            //        }
            //        else if (poziceCíl.Y > poziceStart.Y && poziceCíl.X > poziceStart.X) //zda se posouvá dolů doprava
            //        {
            //            for (int i = 1; i < vzdalenost; i++) //projede všechny možnosti kde můž tím směrem být
            //            {
            //                foreach (Button item in Reds) //projede všechny červené zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item)); //konkrétní kámen
            //                    if (p.X == poziceStart.X + i && p.Y == poziceStart.Y + i) //kontrola zda je kamen na cíli nebo na trae
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //                foreach (Button item in Blues) // projede všechny modré zda jsou na dané pozici
            //                {
            //                    Point p = new Point(Grid.GetColumn(item), Grid.GetRow(item));
            //                    if (p.X == poziceStart.X + i && p.Y == poziceStart.Y + i)
            //                    {
            //                        volno = false;
            //                    }
            //                }
            //            }
            //            //MessageBox.Show("PravoDole " + volno);

            //        }
            //    }
            //    return volno; 
            #endregion
        }
        public bool ValidationMove(Button stone, Rectangle field, string color)
        {
            Point poziceStart = new Point(Grid.GetColumn(stone), Grid.GetRow(stone));
            Point poziceCíl = new Point(Grid.GetColumn(field), Grid.GetRow(field));

            bool mozno = false;

            mozno = Occupied(poziceCíl) == true ? false : true;
            if (mozno == false) return mozno;

            if (Math.Abs(poziceCíl.Y - poziceStart.Y) == 1 &&
                poziceCíl.Y < poziceStart.Y &&
                color == "Red" &&
                (poziceCíl.X - 1 == poziceStart.X || poziceCíl.X + 1 == poziceStart.X))
            {
                mozno = true;
                return mozno;
            }

            else if (Math.Abs(poziceCíl.Y - poziceStart.Y) == 1 &&
                poziceCíl.Y > poziceStart.Y &&
                color == "Blue" &&
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
        public bool ValidationJump(Button stone, Rectangle field, string color)
        {
            Point poziceStart = new Point(Grid.GetColumn(stone), Grid.GetRow(stone));
            Point poziceCíl = new Point(Grid.GetColumn(field), Grid.GetRow(field));

            bool nalezeno = false;

            nalezeno = Occupied(poziceCíl);

            if (!nalezeno)
            {
                for (int i = 0; i < Reds.Count; i++)
                {
                    Point kamenNepritel = new Point(Grid.GetColumn(Reds[i]), Grid.GetRow(Reds[i]));

                    if (color == "Blue" &&
                        poziceStart.X != poziceCíl.X &&
                        kamenNepritel.Y == poziceStart.Y + 1 &&
                        (kamenNepritel.X == poziceStart.X + 1 || kamenNepritel.X == poziceStart.X - 1) &&
                        poziceCíl.Y == kamenNepritel.Y + 1 &&
                        (poziceCíl.X == kamenNepritel.X - 1 || poziceCíl.X == kamenNepritel.X + 1))
                    {
                        Delete(SelectedStone, field);
                        return true;
                    }
                }
                for (int i = 0; i < Blues.Count; i++)
                {
                    Point kamenNepritel = new Point(Grid.GetColumn(Blues[i]), Grid.GetRow(Blues[i]));

                    if (color == "Red" &&
                        poziceStart.X != poziceCíl.X &&
                        kamenNepritel.Y == poziceStart.Y - 1 &&
                        (kamenNepritel.X == poziceStart.X + 1 || kamenNepritel.X == poziceStart.X - 1) &&
                        poziceCíl.Y == kamenNepritel.Y - 1 &&
                        (poziceCíl.X == kamenNepritel.X - 1 || poziceCíl.X == kamenNepritel.X + 1))
                    {
                        Delete(SelectedStone, field);
                        return true;
                    }
                }
            }
            return false;
        }
        public void IsQueen(string color)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Swooshing);

            if (color == "Red" && Grid.GetRow(SelectedStone) == 1 && SelectedStone.Tag.ToString() != "queen")
            {
                SelectedStone.Content = new Image
                {
                    Source = new BitmapImage(new Uri("../../Images/kamen-red-queen.png", UriKind.Relative))
                };

                player.Play();

                SelectedStone.Tag = "queen";
            }
            if (color == "Blue" && Grid.GetRow(SelectedStone) == 8 && SelectedStone.Tag.ToString() != "queen")
            {
                SelectedStone.Content = new Image
                {
                    Source = new BitmapImage(new Uri("../../Images/kamen-blue-queen.png", UriKind.Relative))
                };

                player.Play();

                SelectedStone.Tag = "queen";
            }
        }

        public bool Occupied(Point poziceCíl)
        {
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

            return nalezeno;
        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            FormGame.labelCasUkazatel.Content = GameTime;
            GameTime += TimeSpan.FromSeconds(1);
        }
        public void LoadStones()
        {
            Reds.Add(FormGame.Red_Kamen_01);
            Reds.Add(FormGame.Red_Kamen_02);
            Reds.Add(FormGame.Red_Kamen_03);
            Reds.Add(FormGame.Red_Kamen_04);
            Reds.Add(FormGame.Red_Kamen_05);
            Reds.Add(FormGame.Red_Kamen_06);
            Reds.Add(FormGame.Red_Kamen_07);
            Reds.Add(FormGame.Red_Kamen_08);
            Reds.Add(FormGame.Red_Kamen_09);
            Reds.Add(FormGame.Red_Kamen_10);
            Reds.Add(FormGame.Red_Kamen_11);
            Reds.Add(FormGame.Red_Kamen_12);

            Blues.Add(FormGame.Blue_Kamen_01);
            Blues.Add(FormGame.Blue_Kamen_02);
            Blues.Add(FormGame.Blue_Kamen_03);
            Blues.Add(FormGame.Blue_Kamen_04);
            Blues.Add(FormGame.Blue_Kamen_05);
            Blues.Add(FormGame.Blue_Kamen_06);
            Blues.Add(FormGame.Blue_Kamen_07);
            Blues.Add(FormGame.Blue_Kamen_08);
            Blues.Add(FormGame.Blue_Kamen_09);
            Blues.Add(FormGame.Blue_Kamen_10);
            Blues.Add(FormGame.Blue_Kamen_11);
            Blues.Add(FormGame.Blue_Kamen_12);
        }
        public void Info()
        {
            FormGame.labelHrac1veHre.Content = Reds.Count;
            FormGame.labelHrac2veHre.Content = Blues.Count;
            FormGame.labelHrac1vyrazeno.Content = 12 - Reds.Count;
            FormGame.labelHrac2vyrazeno.Content = 12 - Blues.Count;

            if (Reds.Count == 0)
            {
                MessageBox.Show("Vyhrál modrý!!", "Výhra", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                MainWindow Lobby = new MainWindow();
                Lobby.Show();
                FormGame.Close();
            }
            else if (Blues.Count == 0)
            {
                MessageBox.Show("Vyhrál červený!!", "Výhra", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                
                MainWindow Lobby = new MainWindow();
                Lobby.Show();
                FormGame.Close();
            }
        }
        public void Log(Rectangle field, string color)
        {
            int puvodniRow = Grid.GetRow(SelectedStone);
            int puvodniColum = Grid.GetColumn(SelectedStone);

            int novyRow = Grid.GetRow(field);
            int novyColum = Grid.GetColumn(field);

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

            string vypis = $"{color,-4} - {puvodniRadek}{puvodniColum} > {novyRadek}{novyColum}" + Environment.NewLine;


            FormGame.textBoxLog.Text = vypis + FormGame.textBoxLog.Text;

        }
    }
}
