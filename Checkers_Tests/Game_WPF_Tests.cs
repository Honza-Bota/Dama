using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;

namespace Checkers_Tests
{
    [TestClass]
    public class Game_WPF_Tests
    {
        Game Hra;
        public Game_WPF_Tests()
        {
            Hra = new Game();
        }

        [TestMethod]
        public void Stone_Click_Save_Selected()
        {
            Button stone = new Button(); //prázný kámen

            Hra.StoneClick(stone); //testování zda metoda správně přiřadí hodnotu

            Assert.IsNotNull(Hra.SelectedStone); //validace přiřazení (defaultně je hodnota null, pokud není, funguje)
        }

        [TestMethod]
        public void Is_ValidationMove_Working()
        {
            Button stone = new Button();
            Grid.SetRow(stone, 8);
            Grid.SetColumn(stone, 1); //tvorba testovacího kamene
            Hra.SelectedStone = stone; //vložení kamene jako vybraného

            Rectangle field = new Rectangle();
            Grid.SetRow(field, 7);
            Grid.SetColumn(field, 2); //vytvoření testovacího cíle

            bool correct = Hra.ValidationMove(stone, field, "Red"); //zkouška skoku na danou pozici

            Assert.IsTrue(correct);
        }

        [TestMethod]
        public void Is_ValidationJump_Working()
        {
            Button stone = new Button();
            Grid.SetRow(stone, 8);
            Grid.SetColumn(stone, 1); //tvorba testovacího kamene
            Hra.SelectedStone = stone; //vložení kamene jako vybraného

            Button enemyStone = new Button();
            Grid.SetRow(enemyStone, 7);
            Grid.SetColumn(enemyStone, 2); //tvorba testovacího kamene
            Hra.Blues.Add(enemyStone); //vložení nepřátelského kamene do hry

            Rectangle field = new Rectangle();
            Grid.SetRow(field, 6);
            Grid.SetColumn(field, 3); //vytvoření testovacího cíle

            bool correct = Hra.ValidationJump(stone, field, "Red"); //zkouška skoku na danou pozici

            Assert.IsTrue(correct);
        }
    }
}
