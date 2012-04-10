using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;

namespace Scoreboard
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        // Constructor
        Player[] players;
        public MainPage()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.PlayersListBox.Items.Clear();
            this.PlayersListBox.Items.CopyTo(players, 0);
        }

        private void AddPlayerButton_Click(object sender, EventArgs e)
        {
            Player newItem = new Player("New Player", this.PlayersListBox.Items.Count);
            this.PlayersListBox.Items.Add(newItem);
            this.PlayersListBox.SelectedItem = newItem;

        }

        private void DecreaseScoreButton_Click(object sender, RoutedEventArgs e)
        {
            Player player = ((sender as RepeatButton).Tag as Player);
            player.DecreaseScore();
            this.PlayersListBox.Items[player.Index] = player;
        }

        private void IncreaseScoreButton_Click(object sender, RoutedEventArgs e)
        {
            Player player = ((sender as RepeatButton).Tag as Player);
            player.IncreaseScore();
            this.PlayersListBox.Items[player.Index] = player;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            textbox.SelectAll();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Player player = ((sender as TextBox).Tag as Player);
            player.Name = (sender as TextBox).Text;
            try
            {
                this.PlayersListBox.Items[player.Index] = player;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                System.Diagnostics.Debug.WriteLine("Ignoring Exception!");
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            players = new Player[this.PlayersListBox.Items.Count];
            this.PlayersListBox.Items.CopyTo(players, 0);
            if (settings.Contains("players"))
            {
                settings["players"] = players;
            }
            else
            {
                settings.Add("players", players);
            }
            settings.Save();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            try
            {
                if (settings.TryGetValue<Player[]>("players", out players))
                {
                    foreach (Player player in players)
                    {
                        this.PlayersListBox.Items.Add(player);
                    }
                }
            }
            catch (KeyNotFoundException exception)
            {
                System.Diagnostics.Debug.WriteLine("Ignoring Exception!");
            }
        }
    }
}