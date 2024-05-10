using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
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
        private MariaDB mariaDb;
        public MainWindow()
        {
            mariaDb = new MariaDB();
            InitializeComponent();
            WindowState = WindowState.Maximized;
            

            /*mariaDb.testConnection();
            mariaDb.createDB();
            mariaDb.tryCreateAlterTable();
            mariaDb.createUser();
            mariaDb.listTable();
            mariaDb.deletePlayer();
            mariaDb.checkUser();*/

        }
        private void SetButtonContent(string player1Name)
        {
            btnBestScore1.Content = $"Best score of {player1Name}";
        }
        private void SetButtonContent2(string player2Name)
        {
            btnBestScore2.Content = $"Best score of {player2Name}";
        }
        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            // Change the Grid
            StarterImage.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Collapsed;
            ConfigGridP1.Visibility = Visibility.Visible;
        }
        private void PassToPlayer2(object sender, RoutedEventArgs e)
        {
            // Verify that all the names are filled
            bool arePlayerNamesFilled = !string.IsNullOrWhiteSpace(NamePlayer1TextBox.Text) &&
                                            !string.IsNullOrWhiteSpace(MdpPlayer1TextBox.Text) &&
                                            NamePlayer1TextBox.Text != "Name Player 1" &&
                                            MdpPlayer1TextBox.Text != "Password Player 1";
            bool areWellLogin = mariaDb.checkUser(NamePlayer1TextBox.Text, MdpPlayer1TextBox.Text);
            string player1Name = "";
            if (arePlayerNamesFilled && areWellLogin)
            {
                player1Name = NamePlayer1TextBox.Text;
                string player1Password = MdpPlayer1TextBox.Text;
                ConfigGridP1.Visibility = Visibility.Collapsed;
                ConfigGridP2.Visibility = Visibility.Visible;
                SetButtonContent(player1Name);
                //InitializeGame(player1Name, player2Name, databaseChoice);   
            }
            else
            {
                if (areWellLogin == false) // Becareful it remains the button to be created player 
                {
                    MessageBox.Show("There is an error in your name or in your password", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Please fill in the player name and the password to log in.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public void createPlayer(object sender, RoutedEventArgs e)
        {
            ConfigGridP1.Visibility = Visibility.Collapsed;
            ConfigGridP2.Visibility = Visibility.Collapsed;
            ConfigGridNP.Visibility = Visibility.Visible;
        }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Verify that all the names are filled
            bool arePlayerNamesFilled = !string.IsNullOrWhiteSpace(NamePlayer2TextBox.Text) &&
                                            !string.IsNullOrWhiteSpace(MdpPlayer2TextBox.Text) &&
                                            NamePlayer2TextBox.Text != "Name Player 2" &&
                                            MdpPlayer2TextBox.Text != "Password Player 2";

            // Verify that a BDD is selected
            bool areWellLogin = mariaDb.checkUser(NamePlayer2TextBox.Text, MdpPlayer2TextBox.Text);

            if (arePlayerNamesFilled && areWellLogin)
            {
                string player2Name = NamePlayer2TextBox.Text;
                string player2Mdp = MdpPlayer2TextBox.Text;
                ConfigGridP2.Visibility = Visibility.Collapsed;
                GameGrid.Visibility = Visibility.Visible;
                SetButtonContent2(player2Name);
                //InitializeGame(player1Name, player2Name, databaseChoice);   
            }
            else
            {
                if (areWellLogin == false)
                {
                    MessageBox.Show("There is an error in your name or in your password", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Please fill in the player name and the password to log in.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }
        public void PrintScore(Player player1, Player player2, Score scores)
        {
            Player1Score.Text = $"{player1.Name}: {scores.GetScoreById(player1.Id)}";
            Player2Score.Text = $"{player2.Name}: {scores.GetScoreById(player1.Id)}";
        }
        // To raise what is write in the textbox when a person click on it
        private void NamePlayer1TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Name Player 1")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void NamePlayer1TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Name Player 1";
                tb.Foreground = Brushes.Gray;
            }
        }

        // To fill the textbox when it is null
        private void MdpPlayer1TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Password Player 1")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void MdpPlayer1TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Password Player 1";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void NamePlayer2TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Name Player 2")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void NamePlayer2TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Name Player 2";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void MdpPlayer2TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Password Player 2")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void MdpPlayer2TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Password Player 2";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void NamePlayerNPTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Choose a name")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void NamePlayerNPTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Choose a name";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void MdpPlayerNPTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Write your password")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void MdpPlayerNPTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Write your password";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void CMdpPlayerNPTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Confirm your password")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void CMdpPlayerNPTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Confirm your password";
                tb.Foreground = Brushes.Gray;
            }
        }
        private void goBackToLogin(object sender, RoutedEventArgs e)
        {
            bool arePlayerNamesFilled = !string.IsNullOrWhiteSpace(NamePlayerNPTextBox.Text) &&
                                            !string.IsNullOrWhiteSpace(MdpPlayerNPTextBox.Text) &&
                                            !string.IsNullOrWhiteSpace(CMdpPlayerNPTextBox.Text) &&
                                            NamePlayerNPTextBox.Text != "Choose a name" &&
                                            MdpPlayerNPTextBox.Text != "Write your password" &&
                                            CMdpPlayerNPTextBox.Text != "Confirm your password";
            bool passwordAreEquals = (MdpPlayerNPTextBox.Text == CMdpPlayerNPTextBox.Text);
            // Verify that the player doesn't exist
            bool exist = mariaDb.isExist(NamePlayerNPTextBox.Text);

            if (arePlayerNamesFilled && exist == false && passwordAreEquals)
            {
                mariaDb.createUser(NamePlayerNPTextBox.Text, MdpPlayerNPTextBox.Text);
                ConfigGridNP.Visibility = Visibility.Collapsed;
                ConfigGridP1.Visibility = Visibility.Visible;
                //InitializeGame(player1Name, player2Name, databaseChoice);   
            }
            else
            {
                if (exist == true)
                {
                    MessageBox.Show("This player is already register", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (passwordAreEquals == false)
                {
                    MessageBox.Show("The 2 passwords that you writed aren't the same", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Please fill in the player name and the password to create a new player.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void listPlayers(object sender, EventArgs e)
        {
            ListPlayers.Visibility = Visibility.Visible;
            List<string> allPlayers = mariaDb.listTable();
            PlayersListBox.ItemsSource = allPlayers;
        }
        private void goBackToLogin2(object sender, RoutedEventArgs e)
        {
            ListPlayers.Visibility = Visibility.Collapsed;
            ConfigGridP1.Visibility = Visibility.Visible;
        }
        public void DisplayBestScore(string playerName)
        {
            var result = mariaDb.searchBestScore(playerName);
            if (result.success)
            {
                MessageBox.Show($"Best score for {result.name} is {result.bestScore} made on {result.date}");
            }
            else
            {
                MessageBox.Show("No score data found or an error occurred.");
            }
        }
        private void bestScoreP1(object sender, RoutedEventArgs e)
        {
            DisplayBestScore(NamePlayer1TextBox.Text);
        }
        private void bestScoreP2(object sender, RoutedEventArgs e)
        {
            DisplayBestScore(NamePlayer2TextBox.Text);
        }
    }
}

