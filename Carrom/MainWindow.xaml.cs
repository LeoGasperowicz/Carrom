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

namespace Carrom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Cachez l'image et affichez le formulaire de configuration des joueurs
            GameImage.Visibility = Visibility.Collapsed;
            ConfigGrid.Visibility = Visibility.Visible;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérez les noms des joueurs à partir des TextBox
            string player1Name = Player1TextBox.Text;
            string player2Name = Player2TextBox.Text;

            // Initialisez le jeu avec les noms des joueurs
            //InitializeGame(player1Name, player2Name);

            // Autres actions liées au démarrage du jeu

            // Cachez le formulaire de configuration des joueurs
            ConfigGrid.Visibility = Visibility.Collapsed;
            // Affichez l'image du jeu (ou une autre image pertinente)
            GameImage.Visibility = Visibility.Visible;
        }

        private void Player2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
