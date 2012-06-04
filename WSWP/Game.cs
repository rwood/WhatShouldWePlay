using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WSWP
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PlayingTime { get; set; }
        public double UserRating { get; set; }
        public double BGGRating { get; set; }

        public Game(XmlElement item)
        {
            Id = item.GetAttribute<int>("objectid", -1);
            Name = ((XmlElement)item.SelectSingleNode("name")).InnerText;
            XmlElement stats = (XmlElement)item.SelectSingleNode("stats");
            MinPlayers = stats.GetAttribute<int>("minplayers", -1);
            MaxPlayers = stats.GetAttribute<int>("maxplayers", -1);
            PlayingTime = stats.GetAttribute<int>("playingtime", -1);
            UserRating = ((XmlElement)item.SelectSingleNode("stats/rating")).GetAttribute<double>("value", -1);
            BGGRating = ((XmlElement)item.SelectSingleNode("stats/rating/average")).GetAttribute<double>("value", -1);
        } 
    }
}
