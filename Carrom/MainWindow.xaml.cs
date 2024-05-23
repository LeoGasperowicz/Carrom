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
using static System.Net.Mime.MediaTypeNames;

namespace Carrom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private MariaDB mariaDb;
        private Game game;
        private CarromPiece selectedPawn;
        private Hole selectedHole;
        public MainWindow()
        {
            this.mariaDb = new MariaDB();
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            StarterImage.Visibility = Visibility.Visible;
            PlayButton.Visibility = Visibility.Visible;
            this.game=new Game();

            /*mariaDb.testConnection();
            mariaDb.createDB();
            mariaDb.tryCreateAlterTable();
            mariaDb.createUser();
            mariaDb.listTable();
            mariaDb.deletePlayer();
            mariaDb.checkUser();*/

        }
        private void PopulateComboBoxes()
        {
            var allPawns = this.game.Player1.Pieces.Concat(this.game.Player2.Pieces)
                .Where(p => p.InGame)
                .ToList();

            allPawns.Add(this.game.Queen);

            PawnComboBox.ItemsSource = allPawns
                .Select(p => new { Display = $"{p.Number} ({p.Color})", Value = p })
                .ToList();

            PawnComboBox.DisplayMemberPath = "Display";
            PawnComboBox.SelectedValuePath = "Value";

            HoleComboBox.ItemsSource = this.game.Board.Holes
                .Select(h => new { Display = $"Hole {h.Number}", Value = h })
                .ToList();

            HoleComboBox.DisplayMemberPath = "Display";
            HoleComboBox.SelectedValuePath = "Value";
        }

        private void DrawBoard()
        {
            BoardCanvas.Children.Clear();

            foreach (var hole in this.game.Board.Holes)
            {
                Ellipse holeEllipse = new Ellipse
                {
                    Width = hole.Diameter,
                    Height = hole.Diameter,
                    Fill = Brushes.Black,
                    Stroke = Brushes.White,
                    StrokeThickness = 2
                };
                Canvas.SetLeft(holeEllipse, hole.Center.X - hole.Diameter / 2);
                Canvas.SetTop(holeEllipse, hole.Center.Y - hole.Diameter / 2);
                BoardCanvas.Children.Add(holeEllipse);

                // Draw the number on the hole
                TextBlock numberText = new TextBlock
                {
                    Text = hole.Number.ToString(),
                    FontWeight = FontWeights.Bold,
                    FontSize = 16,
                    Foreground = Brushes.White
                };
                Canvas.SetLeft(numberText, hole.Center.X - 8); 
                Canvas.SetTop(numberText, hole.Center.Y - 8);
                BoardCanvas.Children.Add(numberText);
            }

            DrawPawn(this.game.Queen);
            DrawStriker(this.game.Striker);

            foreach (CarromPiece pawn in this.game.Player1.Pieces.Concat(this.game.Player2.Pieces).Where(p => p.InGame))
            {
                    DrawPawn(pawn);
            }
        }

        private void DrawPawn(CarromPiece pawn)
        {
            Ellipse pawnEllipse = new Ellipse
            {
                Width = pawn.Diameter,
                Height = pawn.Diameter,
                Fill = new SolidColorBrush(pawn.Color),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            Canvas.SetLeft(pawnEllipse, pawn.Position.X - pawn.Diameter / 2);
            Canvas.SetTop(pawnEllipse, pawn.Position.Y - pawn.Diameter / 2);
            BoardCanvas.Children.Add(pawnEllipse);
            // Draw the number on the pawn
            TextBlock numberText = new TextBlock
            {
                Text = pawn.Number.ToString(),
                FontWeight = FontWeights.Bold,
                FontSize = 10
            };
            if (pawn.Color == Colors.White)
            {
                numberText.Foreground = Brushes.Black;
            }
            else
            {
                numberText.Foreground = Brushes.White;
            }
            Canvas.SetLeft(numberText, pawn.Position.X - 5); 
            Canvas.SetTop(numberText, pawn.Position.Y - 5); 
            BoardCanvas.Children.Add(numberText);
        }
        private void DrawStriker(Striker pawn)
        {
            Ellipse pawnEllipse = new Ellipse
            {
                Width = pawn.Diameter,
                Height = pawn.Diameter,
                Fill = new SolidColorBrush(pawn.Color),
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            Canvas.SetLeft(pawnEllipse, pawn.Position.X - pawn.Diameter / 2);
            Canvas.SetTop(pawnEllipse, pawn.Position.Y - pawn.Diameter / 2);
            BoardCanvas.Children.Add(pawnEllipse);
        }
        private void BreakButton_Click(object sender, RoutedEventArgs e)
        {
            RandomizePawnPositions();
            DrawBoard();
            BreakButton.IsEnabled = false;
            PlayTurnButton.IsEnabled = true;
            this.game.SwitchPlayerTurn();
            DrawBoard();
        }

        private void RandomizePawnPositions()
        {
            Random rand = new Random();
            double boardWidth = this.game.Board.Width;
            double boardHeight = this.game.Board.Height;
            double margin = 50; // Margin from the edges to avoid placing pawns too close to the edge

            foreach (var pawn in this.game.Player1.Pieces.Concat(this.game.Player2.Pieces).Where(p => p.InGame).Concat(new[] { this.game.Queen }))
            {
                double x = rand.NextDouble() * (boardWidth - 2 * margin) + margin;
                double y = rand.NextDouble() * (boardHeight - 2 * margin) + margin;
                pawn.Position = new Point(x, y);
            }
        }
        private void PawnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedPawn = PawnComboBox.SelectedValue as CarromPiece;
        }

        private void HoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HoleComboBox.SelectedValue != null)
            {
                this.selectedHole = (Hole)HoleComboBox.SelectedValue;
            }
        }

        private void PlayTurnButton_Click(object sender, RoutedEventArgs e)
        {
            Pawn testPawn = this.selectedPawn;
            Hole testhole = this.selectedHole;
            if (this.selectedPawn != null && this.selectedHole != null)
            {
                this.game.PlayTurn(this.selectedPawn, this.selectedHole);
                DrawBoard();
                UpdateScores();
                PopulateComboBoxes(); 

                // Check if the game is over
                if (this.game.IsGameOver())
                {
                    string winner;
                    if (this.game.Player1.Pieces.All(p => !p.InGame))
                    {
                        winner = this.game.Player1.Name;
                    }
                    else if (this.game.Player2.Pieces.All(p => !p.InGame))
                    {
                        winner = this.game.Player2.Name;
                    }
                    else
                    {
                        winner = "No winner yet"; 
                    }
                    MessageBox.Show($"Game Over! {winner} wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                    PlayTurnButton.IsEnabled = false;
                    int idp1 = this.mariaDb.getIdPlayer(this.game.Player1.Name);
                    int idp2 = this.mariaDb.getIdPlayer(this.game.Player2.Name);
                    DateTime time = DateTime.Now;
                    this.mariaDb.saveGame(idp1, time, this.game.Score.Scores[0]);
                    this.mariaDb.saveGame(idp2, time, this.game.Score.Scores[1]);
                }
                else
                {
                    this.game.SwitchPlayerTurn();
                    DrawBoard();
                }
            }
            else
            {
                MessageBox.Show("Please select both a pawn and a hole.", "Selection Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateScores()
        {
            Player1Score.Text = $"{this.game.Player1.Name}: {this.game.Score.Scores[0]}";
            Player2Score.Text = $"{this.game.Player2.Name}: {this.game.Score.Scores[1]}";
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
        private void UpdatePlayerNames()
        {
            Player1Name.Text = this.game.Player1.Name;
            Player2Name.Text = this.game.Player2.Name;
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
                List<int> score = new List<int> { 0, 0 };
                Score scores = new Score(score);
                PrintScore(1, 2,scores);
                this.game.InitializeGame(NamePlayer1TextBox.Text, NamePlayer2TextBox.Text);
                UpdatePlayerNames();
                UpdatePlayerColors();
                PopulateComboBoxes();
                DrawBoard();
                PlayTurnButton.IsEnabled = false;
                if (Probability1RadioButton.IsChecked == true)
                {
                    this.game.SetProbabilityMode(1); // Setting mode to fixed probability of 1
                }
                else if (RealisticProbabilityRadioButton.IsChecked == true)
                {
                    this.game.SetProbabilityMode(2); // Setting mode to realistic probability
                }
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
        public void PrintScore(int player1, int player2, Score scores)
        {
            Player1Score.Text = $"{NamePlayer1TextBox.Text}: {scores.GetScoreById(player1)}";
            Player2Score.Text = $"{NamePlayer2TextBox.Text}: {scores.GetScoreById(player1)}";
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
        private void UpdatePlayerColors()
        {
            Player1Color.Text = $"{NamePlayer1TextBox.Text}'s Color: White";
            Player2Color.Text = $"{NamePlayer2TextBox.Text}'s Color: Black";
        }
    }
}

