using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class WinnersDaterminant
    {
        public static string DaterminesWinner(string [] moves ,int move1,int move2)
        {
            var distanceToMove2Right = (move2 - move1 + moves.Length) % moves.Length;
            var distanceToMove2Left = (move1 - move2 + moves.Length) % moves.Length;
            return move1 == move2 ? "draw" : distanceToMove2Left < distanceToMove2Right ? "win" : "lose";
        }
    }
}
