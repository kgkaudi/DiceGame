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

namespace DiceGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int cpuMoney = 50000;
        int playerMoney = 50000;
        int playerBet = 0;
        int cpuBet = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private static bool IsDup(int tmp, int[] arr)
        {
            foreach (var item in arr)
            {
                if (item == tmp)
                {
                    return true;
                }
            }
            return false;
        }

        private void Roll_Click(object sender, RoutedEventArgs e)
        {
            
                int[] arr = new int[4];
                Random rnd = new Random();
                int tmp;

                for (int i = 0; i < arr.Length; i++)
                {
                    tmp = rnd.Next(7);
                    while (IsDup(tmp, arr))
                    {
                        tmp = rnd.Next(7);
                    }
                    arr[i] = tmp;
                }
                CPUDice1.Content = arr[0].ToString();
                CPUDice2.Content = arr[1].ToString();
                PlayerDice1.Content = arr[2].ToString();
                PlayerDice2.Content = arr[3].ToString();

                int cpuRoll = arr[0] + arr[1];
                int playerRoll = arr[2] + arr[3];

                if (cpuRoll == playerRoll)
                {
                    Narration.Content = "Draw";
                    cpuMoney = cpuMoney + playerBet;
                    playerMoney = playerMoney + Int32.Parse(PlayerBet.Text);
                    PlayerMoney.Text = playerMoney.ToString();
                    CPUMoney.Text = cpuMoney.ToString();
                }

                else if (cpuRoll > playerRoll)
                {
                    Narration.Content = "CPU Won";
                    cpuMoney = cpuMoney + cpuBet + Int32.Parse(CPUBet.Text);
                    CPUMoney.Text = cpuMoney.ToString();
                }

                else if (cpuRoll < playerRoll)
                {
                    Narration.Content = "Player Won";
                    playerMoney = playerMoney + playerBet + Int32.Parse(PlayerBet.Text);
                    PlayerMoney.Text = playerMoney.ToString();
                }
            BetButton.IsEnabled = true;
            Roll.IsEnabled = false;
            if (Int32.Parse(CPUMoney.Text) < 0)
            {
                Narration.Content = "Player Won";
                BetButton.IsEnabled = false;
                Roll.IsEnabled = false;
            }
            if (Int32.Parse(PlayerMoney.Text) < 0)
            {
                Narration.Content = "CPU Won";
                BetButton.IsEnabled = false;
                Roll.IsEnabled = false;
            }
            CPUBet.Text = "";
            PlayerBet.Text = "";
        }

        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(PlayerBet.Text) <= 0)
                {
                    Narration.Content = "You must bet money.";
                    return;
                }
                if (Int32.Parse(PlayerMoney.Text) < Int32.Parse(PlayerBet.Text))
                {
                    Narration.Content = "Not enough funds.";
                    return;
                }
                if (Int32.Parse(CPUMoney.Text) < Int32.Parse(PlayerBet.Text))
                {
                    Narration.Content = "Last chance!!!!!!!";
                    cpuBet = cpuMoney;
                    return;
                }
                if (Int32.Parse(PlayerMoney.Text) <= 0)
                {
                    Narration.Content = "You lost.";
                    return;
                }
                if (Int32.Parse(CPUMoney.Text) <= 0)
                {
                    Narration.Content = "You won.";
                    return;
                }
                playerMoney = playerMoney - Int32.Parse(PlayerBet.Text);
                playerBet = Int32.Parse(PlayerBet.Text);
                CPUBet.Text = playerBet.ToString();
                cpuBet = playerBet;
                cpuMoney = cpuMoney - cpuBet;
                PlayerMoney.Text = playerMoney.ToString();
                CPUMoney.Text = cpuMoney.ToString();
                BetButton.IsEnabled = false;
                Roll.IsEnabled = true;
            }
            catch
            {
                if (PlayerBet.Text == "")
                {
                    Narration.Content = "Invalid value. Please place bet.";
                }
                
            }
        }
    }
}
