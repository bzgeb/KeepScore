using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;

namespace Scoreboard
{
    [DataContract]
    public class Player
    {
        [DataMember(Name="Name")]
        public String Name { get; set; }

        [DataMember(Name="Score")]
        public int Score { get; set; }

        [DataMember(Name="Index")]
        public int Index { get; set; }

        public Player(string name, int index)
        {
            this.Name = name;
            this.Score = 0;
            this.Index = index;
        }

        public void IncreaseScore()
        {
            this.Score += 1;
        }

        public void DecreaseScore()
        {
            this.Score -= 1;
        }

        public void ClearScore()
        {
            this.Score = 0;
        }
    }
}
