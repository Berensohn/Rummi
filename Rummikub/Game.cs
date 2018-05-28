using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rummikub
{
    /// <summary>
    /// class game consists of all the tile cards in the bowl, in the player's hand and in computer's hand
    /// </summary>
    public class Game
    {
        public List<Tile> bowl = new List<Tile>(106); //bowl consisting of 2 deck of cards, 53 cards in each deck with joker
        public List<Tile> hand = new List<Tile>(14); //cards in hand 
        public List<Tile> computer = new List<Tile>(14); //cards in bowl


        /// <summary>
        /// constructs the bowl with all cards, then gives 14 cards at random to player and computer each
        /// </summary>


        public Game ()
        {
            Type[] types = (Type[])Enum.GetValues(typeof(Type));
            for (int j=1;j<=2;j++)
            {
                for (int z=0;z<4;z++)
                {
                    
                    for (int i = 1; i <= 13; i++)
                    {
                        Tile tile = new Tile(i, types[z]);
                        bowl.Add(tile);
                    }
                }
                
            }

            Random random = new Random();

            for (int i=0;i<14;i++)
            {
                int r1 = random.Next(0, bowl.Count-1);
                hand.Add(bowl[r1]);
                bowl.Remove(bowl[r1]);
                int r2 = random.Next(0, bowl.Count);
                computer.Add( bowl[r2]);
                bowl.Remove(bowl[r2]);
            }
             
        }

        
        }
    }

