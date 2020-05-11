using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkers
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButNapoveda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nápověda není v této verzi k dispozici!","Informace",MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.OK,MessageBoxOptions.ServiceNotification);
        }

        private void ButNastaveniHrac2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nastavení není v této verzi k dispozici!", "Informace", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);

        }

        private void ButNastaveniHrac1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nastavení není v této verzi k dispozici!", "Informace", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);

        }

        private void ButHracPc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tato možnost není v této verzi k dispozici!", "Informace", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);

        }

        private void ButHracHrac_Click(object sender, RoutedEventArgs e)
        {
            Window1 Game = new Window1();
            Game.Show();
            this.Close();
        }

    }
}
