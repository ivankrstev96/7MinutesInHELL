using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7MinutesInHELL
{
    [Serializable]
    public class playerPoints
    {
        public String name;
        public int points;
        public playerPoints(String name, int points)
        {
            this.name = name;
            this.points = points;
        }
    }
    [Serializable]
    public class LeaderBoardsList
    {

        public List<playerPoints> players;
        public LeaderBoardsList()
        {
            players = new List<playerPoints>();
        }
        public void addNew(String name, int points)
        {
            if(players.Count >= 10)
            {
                players.Add(new playerPoints(name, points));
                players.Sort((a, b) => b.points.CompareTo(a.points));
                players.RemoveAt(10);
            }
            else
            {
                players.Add(new playerPoints(name, points));
                players.Sort((a, b) => b.points.CompareTo(a.points));
            }
            
        }
        public List<playerPoints> getPlayers()
        {
            return players;
        }
    }
}
