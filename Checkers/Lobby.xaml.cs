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

        private void ButKonec_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButNapoveda_Click(object sender, RoutedEventArgs e)
        {
            const string informace = @"Česká dáma

Úvod:
~ Toto jsou pravidla České dámy, podle nichž se hrají oficiální turnaje. 

Hrací potřeby:
~ Hrací deska 8×8 
~ 12 kamenů pro každého hráče 

Počet hráčů: 2 

Pravidla:
~ Cíl hry: Vyřadit všechny soupeřovy kameny. 

~ Výchozí situace: Kameny každého hráče jsou rozestavěny ve třech řadách, ob jedno pole. První kámen je v levém dolním rohu hrací desky. 

~ Hra:
 - Hráči se v tazích pravidelně střídají. Začíná bílý. 
 - Všechny kameny se pohybují vždy o jedno volné pole diagonálně vpřed. Jestliže na takovém poli stojí soupeřův kámen a další pole za ním ve směru tahu je volné, musí soupeřův kámen přeskočit a odklidit z desky. 
 - Kámen, kterým hráč táhne, může provést i skoky vícenásobné. Mezi přeskakovanými soupeřovými kameny musí být vždy jedno pole volné a nesmějí stát těsně za sebou. Po každém dopadu lze změnit směr dalšího skoku. Vícenásobný přeskok je nutno provést v plném rozsahu. Při možnosti výběru z několik a vícenásobných přeskoků si smí hráč zvolit pro něj nejvýhodnější. Nemusí přeskočit největší možný počet kamenů. Dáma má přednost ve skákání. 
 - Všechny kameny mají povolen pohyb i přeskoky pouze vpřed. 
 - Skákání je povinné. Kdo přehlédne možnost skoku, ztrácí kámen, kterým měl skákat a soupeř tento kámen odstraní z desky. 
 - Kámen, který dojde do poslední řady polí, stává se dámou. Dáma se vytvoří ze dvou kamenů položených na sebe. 
 - Dáma může během tahu postoupit o jakýkoliv počet volných polí v libovolném směru. I dáma smí přeskočit jen osamělé soupeřovy kameny. Stojí-li těsně za sebou nebo je v cestě vlastní kámen, skok není možný. Při vícenásobném skoku mohou být mezery mezi přeskakovanými soupeřovými kameny libovolně velké a dáma smí dopadnout za ně na libovolné volné pole. Smí také po každém dopadu změnit směr dalšího skákání. Nesmí však přeskočit žádný kámen dvakrát. Přeskočené kameny jsou z desky odebrány teprve po dokončení celého vícenásobného skoku. 

~ Konec hry:
 -Prohrává ten, komu nezbyl žádný kámen, nebo nemůže provést svými kameny žádný tah. 
 -Hra končí nerozhodně, když se hráči na tom dohodnou, nebo když se vyskytne třikrát stejná pozice. ";
            MessageBox.Show(informace,"Nápověda",MessageBoxButton.OK);
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
