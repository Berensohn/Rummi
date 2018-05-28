using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rummikub
{
    public abstract class Algorithm
    {
        /// <summary>
        /// Main Algorithm if the progect it plays a move and tries to get rid of as much tiles it can from the hand
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="hand"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        /// Future Error: will not do the algorithm at the beginning because there are no remainders on board


        public static void amen(Tile tile, List<Tile> comp, List<List<Tile>> board)
        {
            

            Bros colorbros = createColorGroup(tile, comp, board);
            if (colorbros != null)
            {
               
                board = new List<List<Tile>>(colorbros.board);

            }
            else
            {
                Bros straightbros = createStraightGroup(tile, comp, board);
                if (straightbros != null)
                {
          
                    board = new List<List<Tile>>(straightbros.board);

                }
            }
           /* if (!boolRemainder(board))
            {

                return;

            }*/
        
            
        }
      




        /// <summary>
        /// 
        /// Checks to see if their are any groups on the board that have less than 3
        /// If so : they are invalid groups
        /// true if remainder
        /// </summary>
        /// <param name="board"></param>
        /// <returns>true if remainder</returns>
        public static bool boolRemainder(List<List<Tile>> board)
        {
            foreach (List<Tile> group in board)
            {
                if (group.Count < 3)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if their are any groups on the board that have less than 3
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns>
        /// Invalid groups
        /// </returns>

        public static List<List<Tile>> getRemainder(List<List<Tile>> board)
        {
            List<List<Tile>> remainderGroups = new List<List<Tile>>();
            foreach (List<Tile> group in board)
            {
                if (group != null)
                {
                    if (group.Count < 3)
                        remainderGroups.Add(group);
                }
            }
            return remainderGroups;
        }

        /// <summary>
        /// Creates a color group given a tile and the tiles on the board
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="hand"></param>
        /// <param name="board"></param>
        /// <returns> true if was able to create a color group </returns>
        public static Bros createColorGroup(Tile tile, List<Tile> inputhand, List<List<Tile>> inputboard)
        {
            List<List<Tile>> board = inputboard;
            List<Tile> hand = inputhand;
            List<Tile> colorGroup = new List<Tile>();
            colorGroup.Add(tile);
            foreach (Tile handTile in hand)
            {
                if (tile.value == handTile.value)
                {
                    bool check = true;
                    for (int i = 0; i < colorGroup.Count; i++)
                    {
                        if ((handTile.type == colorGroup[i].type))
                            check = false;


                    }
                    if (check)
                    {
                        colorGroup.Add(handTile);
                        

                    }
                }
            }
           
            
            
            {
                foreach (List<Tile> group in board)
                {
                    if (group.Count > 3)
                    {

                        if (tile.value == group[0].value)
                        {
                            bool check = true;
                            for (int i = 0; i < colorGroup.Count; i++)
                            {
                                if ((group[0].type == colorGroup[i].type))
                                    check = false;


                            }
                            if (check)
                            {
                                colorGroup.Add(group[0]);
                                
                            }

                        }
                    }

                    if (group.Count > 3)
                    {

                        if (tile.value == group[group.Count-1].value)
                        {
                            bool check = true;
                            for (int i = 0; i < colorGroup.Count; i++)
                            {
                                if ((group[group.Count - 1].type == colorGroup[i].type))
                                    check = false;


                            }
                            if (check)
                            {
                                colorGroup.Add(group[group.Count - 1]);
                               
                            }

                        }
                    }
                    
                }

                if (colorGroup.Count >= 3)
                {
                    board.Add(colorGroup);


                    foreach (Tile colortile in colorGroup)
                        hand.Remove(colortile);


                    return new Bros(board, hand, colorGroup);
                }

                //return false;
            }
            return null;
        }
        /// <summary>
        /// Creates a straight group given a tile and the tiles on the board
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="hand"></param>
        /// <param name="board"></param>
        /// <returns> true if was able to create a straight group </returns>
        public static Bros createStraightGroup(Tile tile, List<Tile> inputhand, List<List<Tile>> inputboard)
        {
            //Tile removetile = null;
            List<List<Tile>> board = inputboard;
            List<Tile> straightGroup = new List<Tile>();
            List<Tile> hand = inputhand;
            straightGroup.Add(tile);

            foreach (Tile handTile in hand)
            {
                bool check = true;
                for (int i = 0; i < straightGroup.Count; i++)
                {
                    if (handTile.type != straightGroup[i].type)
                        check = false;


                }
                if (check)
                {

                    if (straightGroup[0].value - 1 == handTile.value)
                    {

                        straightGroup.Insert(0, handTile);
                       
                    }
                    else
                    {
                        if (straightGroup[straightGroup.Count - 1].value + 1 == handTile.value)
                        {
                            straightGroup.Add(handTile);
                        }
                    }



                }
            }

            
            {
                foreach (List<Tile> group in board)
                {
                    if (group.Count > 3)
                    {




                        bool check = true;
                        for (int i = 0; i < straightGroup.Count; i++)
                        {
                            if (group[0].type != straightGroup[i].type)
                                check = false;


                        }
                        if (check)
                        {

                            if (straightGroup[0].value - 1 == group[0].value)
                            {

                                straightGroup.Insert(0, group[0]);
                                
                            }
                            else
                            {
                                if (straightGroup[straightGroup.Count - 1].value + 1 == group[0].value)
                                {
                                    straightGroup.Add(group[0]);
                                    
                                }
                            }




                        }

                        if (group.Count > 3)
                        {

                            check = true;
                            for (int i = 0; i < straightGroup.Count; i++)
                            {
                                if (group[group.Count - 1].type != straightGroup[i].type)
                                    check = false;


                            }
                            if (check)
                            {

                                if (straightGroup[0].value - 1 == group[group.Count - 1].value)
                                {

                                    straightGroup.Insert(0, group[group.Count - 1]);
                                    
                                }
                                else
                                {
                                    if (straightGroup[straightGroup.Count - 1].value + 1 == group[group.Count - 1].value)
                                    {
                                        straightGroup.Add(group[group.Count - 1]);
                                       
                                    }
                                }




                            }
                        }

                       

                    }
                }
                if (straightGroup.Count >= 3)
                {
                    board.Add(straightGroup);
                    

                    for (int j = 0; j < straightGroup.Count; j++)
                    {
                        hand.Remove(straightGroup[j]);
                       
                    }
                   
                    
                    return new Bros(board, hand, straightGroup);

                   
                }
                
                return null;
            }
        }

        public static bool Easy(List<List<Tile>> board, List<Tile> hand)
        {
            foreach (List<Tile> lst in board)
            {
               
                if (lst.Count > 0)
                {
                    foreach (Tile tile in hand)
                    {
                        
                        if (tile != null)
                        {
                            bool colorchecker_straight = true;
                            foreach (Tile checktile in lst)
                            {

                                if (checktile.type != tile.type)
                                    colorchecker_straight = false;
                                
                            }

                            bool colorchecker_color = true;
                            foreach (Tile checktile in lst)
                            {
                                
                                if (checktile.type == tile.type)
                                    colorchecker_color = false;
                                
                            }

                            bool valuechecker = true;
                            foreach (Tile checktile in lst)
                            {
                             
                                if (checktile.value != tile.value)
                                    valuechecker = false;
                            
                            }

                            if (tile.value + 1 == lst[0].value)

                            {
                                if (colorchecker_straight)
                                {
                                    lst.Insert(0, tile);
                                    return true;
                                }
                            }

                            if (tile.value - 1 == lst[lst.Count - 1].value)
                            {
                                if (colorchecker_straight)
                                {
                                    lst.Add(tile);
                                    return true;
                                }

                            }

                            if (colorchecker_color)
                            {
                                if (valuechecker)
                                {
                                    lst.Add(tile);
                                    return true;
                                }

                            }
                        }
                    }


                }

            }
            

            return false;
        }


    }
}


