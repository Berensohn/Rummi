using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rummikub
{
    /// <summary>
    /// data type for algorithm purposes consisting of board, hand, and group-either color or straight
    /// </summary>
    public class Bros
    {
        public List<List<Tile>> board = new List<List<Tile>>();//tiles on board
        public List<Tile> hand = new List<Tile>(); //tiles in hand-either player or computer
        public List<Tile> group = new List<Tile>(); //group either straight or color

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="hand"></param>
        /// <param name="group"></param>
        public Bros (List<List<Tile>> board, List<Tile> hand, List<Tile> group)
        {
            this.board = board;
            this.hand = hand;
            this.group = group;
        }
        
    }
}
