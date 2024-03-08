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
        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            // Change the Grid
            StarterImage.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Collapsed;
            ConfigGrid.Visibility = Visibility.Visible;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Verify that all the names are filled
            bool arePlayerNamesFilled = !string.IsNullOrWhiteSpace(Player1TextBox.Text) &&
                                            !string.IsNullOrWhiteSpace(Player2TextBox.Text) &&
                                            Player1TextBox.Text != "Name Player 1" &&
                                            Player2TextBox.Text != "Name Player 2";

            // Verify that a BDD is selected
            bool isDatabaseSelected = MariaDBRadioButton.IsChecked == true || PostgreSQLRadioButton.IsChecked == true;

            if (arePlayerNamesFilled && isDatabaseSelected)
            {
                string player1Name = Player1TextBox.Text;
                string player2Name = Player2TextBox.Text;
                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);
                string databaseChoice = "";
                if (MariaDBRadioButton.IsChecked == true)
                {
                    databaseChoice = "MariaDB";
                }
                else if (PostgreSQLRadioButton.IsChecked == true)
                {
                    databaseChoice = "PostgreSQL";
                }
                // Change the Grid
                ConfigGrid.Visibility = Visibility.Collapsed;
                GameGrid.Visibility = Visibility.Visible;
                List<int> score = new List<int> { 0, 0 };
                Score scores = new Score(score);
                PrintScore(player1, player2, scores);
            }
            else
            {
                MessageBox.Show("Please fill in both player names and select a database to start the game.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        public void PrintScore(Player player1, Player player2, Score scores )
        {
            Player1Score.Text = $"{player1.name}: {scores.GetScoreById(player1.id)}";
            Player2Score.Text = $"{player2.name}: {scores.GetScoreById(player1.id)}";
        }
        // To raise what is write in the textbox when a person click on it
        private void Player1TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Name Player 1")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray; 
            }
        }

        // To fill the textbox when it is null
        private void Player1TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Name Player 1";
                tb.Foreground = Brushes.Gray; 
            }
        }
        private void Player2TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Name Player 2")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void Player2TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Name Player 2";
                tb.Foreground = Brushes.Gray;
            }
        }
    }
}
