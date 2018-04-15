using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rummikub
{
    public class Bros
    {
        public List<List<Tile>> board = new List<List<Tile>>();
        public List<Tile> hand = new List<Tile>();
        public List<Tile> group = new List<Tile>();

        public Bros (List<List<Tile>> board, List<Tile> hand, List<Tile> group)
        {
            this.board = board;
            this.hand = hand;
            this.group = group;
        }
        
    }
}
