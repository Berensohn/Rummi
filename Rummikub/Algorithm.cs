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

        /*public static void hallelujah (List<Tile> comp, List<List<Tile>> board)
        {
            
        }*/

        public static void amen(Tile tile, List<Tile> comp, List<List<Tile>> board)
        {
            //Easy(board, comp);
            Bros colorbros = createColorGroup(tile, comp, board);
            if (colorbros != null)
            {
                //comp = new List < Tile > (colorbros.group);
                board = new List<List<Tile>>(colorbros.board);

            }
            else
            {
                Bros straightbros = createStraightGroup(tile, comp, board);
                if (straightbros != null)
                {
                    //comp = new List<Tile>(straightbros.group);
                    board = new List<List<Tile>>(straightbros.board);

                }
            }
            // MessageBox.Show(boolRemainder(board).ToString());
            if (!boolRemainder(board))
            {

                return;

            }
            else
            {
                //MessageBox.Show(getRemainder(board)[0][0].ToString());
                amen(getRemainder(board)[0][0], comp, board);

            }

            /* int tilecount = 0;
             List<List<Tile>> remainder = getRemainder(board);
             Tile maybe=null;
             foreach (List<Tile> group in remainder)
             {
                 foreach (Tile t in group)
                 {
                     if (t!=null)
                     {
                         tilecount++;
                         maybe = t;
                     }
                 }
             }
             if (tilecount == 1)
             {
                 if (maybe.Equals(tile))
                     return;
             }
             else
             {

                 amen(maybe, comp,board);


             }*/


        }
        //createStraightGroup(hand[6], hand, board);

        //List<List<Tile>> rem = getRemainder(board);
        /*List<Tile> cg = createColorGroup(rem[0][0], hand, board);
        Easy(board, hand);
        amen(board, hand);*/
        //List<Tile> sg = createStraightGroup(rem[0][0], hand, board);
        //Easy(board, hand);








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
            //Tile removetile = null;
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
                        //hand.Remove(handTile);

                    }
                }
            }
            //colorGroup.Remove(tile);


            //if (board != null)
            {
                foreach (List<Tile> group in board)
                {
                    foreach (Tile boardTile in group)
                    {
                        if (tile.value == boardTile.value)
                        {
                            bool check = true;
                            for (int i = 0; i < colorGroup.Count; i++)
                            {
                                if ((boardTile.type == colorGroup[i].type))
                                    check = false;


                            }
                            if (check)
                            {
                                colorGroup.Add(boardTile);
                                //removetile = boardTile;
                                //hand.Remove(boardTile);
                            }

                        }
                    }
                    //group.Remove(removetile);
                }

                if (colorGroup.Count >= 3)
                {
                    board.Add(colorGroup);
                    foreach (Tile colortile in colorGroup)
                        hand.Remove(colortile);
                    return new Bros(board, hand, colorGroup);
                    //return true;
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
                        //straightGroup.Sort((x, y) => x.value.CompareTo(y.value));
                        //hand.Remove(handTile);
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

            //straightGroup.Remove(tile);
            //if (board != null)
            {
                foreach (List<Tile> group in board)
                {
                    foreach (Tile boardTile in group)
                    {


                        bool check = true;
                        for (int i = 0; i < straightGroup.Count; i++)
                        {
                            if (boardTile.type != straightGroup[i].type)
                                check = false;


                        }
                        if (check)
                        {

                            if (straightGroup[0].value - 1 == boardTile.value)
                            {

                                straightGroup.Insert(0, boardTile);
                                //removetile = boardTile;
                                //straightGroup.Sort((x, y) => x.value.CompareTo(y.value));
                                //group.Remove(boardTile);
                            }
                            else
                            {
                                if (straightGroup[straightGroup.Count - 1].value + 1 == boardTile.value)
                                {
                                    straightGroup.Add(boardTile);
                                    //removetile = boardTile;
                                    // group.Remove(boardTile);
                                }
                            }




                        }
                    }
                    //group.Remove(removetile);






                }
            }
            if (straightGroup.Count >= 3)
            {
                board.Add(straightGroup);
                for (int j = 0; j < straightGroup.Count; j++)
                {
                    hand.Remove(straightGroup[j]);
                    // for(int i=0;i<board.Count;i++)
                    //  board[i].Remove(straightGroup[j]);
                }
                return new Bros(board, hand, straightGroup);

                //board.Add(straightGroup);
                //return straightGroup;
                //return true;
            }
            //return false;
            return null;
        }

        public static bool Easy(List<List<Tile>> board, List<Tile> hand)
        {
            foreach (List<Tile> lst in board)
            {
                /*List<Tile> color =createColorGroup(tile, hand, board);
                if (color!=null)
                {
                    board.Add(color);
                    return true;
                }

                List<Tile> straight= createStraightGroup(tile, hand, board);
                if (straight != null)
                {
                    board.Add(straight);
                    return true;
                }*/
                if (lst.Count > 0)
                {
                    foreach (Tile tile in hand)
                    {
                        //checks to see if it can add tile easily to a straight group
                        // Type[] types = (Type[])Enum.GetValues(typeof(Type));
                        if (tile != null)
                        {
                            bool colorchecker_straight = true;
                            foreach (Tile checktile in lst)
                            {
                                // MessageBox.Show(checktile.type.ToString()+checktile.value.ToString());
                                if (checktile.type != tile.type)
                                    colorchecker_straight = false;
                                // MessageBox.Show(colorchecker.ToString());
                            }

                            bool colorchecker_color = true;
                            foreach (Tile checktile in lst)
                            {
                                // MessageBox.Show(checktile.type.ToString()+checktile.value.ToString());
                                if (checktile.type == tile.type)
                                    colorchecker_color = false;
                                // MessageBox.Show(colorchecker.ToString());
                            }

                            bool valuechecker = true;
                            foreach (Tile checktile in lst)
                            {
                                // MessageBox.Show(checktile.type.ToString()+checktile.value.ToString());
                                if (checktile.value != tile.value)
                                    valuechecker = false;
                                // MessageBox.Show(colorchecker.ToString());
                            }

                            if (tile.value + 1 == lst[0].value)

                            {
                                if (colorchecker_straight)
                                {
                                    lst.Insert(0, tile);
                                    return true;
                                }
                            }

                            if (tile.value - 1 == lst[lst.Count - 1].value) //check maybe without -1
                            {
                                if (colorchecker_straight)
                                {
                                    lst.Add(tile);
                                    return true;
                                }

                            }

                            if (!colorchecker_color)
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






        /* public static void Extra(List<List<Tile>> board, List<Tile> hand)
         {
             foreach (Tile handtile in hand)
             {
                 List<Tile> maybe = new List<Tile>() { handtile };
                 bool colorchecker = true;
                 foreach (Tile handtile2 in hand)
                 {
                     if (handtile.type != handtile2.type)
                         colorchecker = false;

                     if (handtile2.value + 1 == maybe[0].value)

                     {
                         if (colorchecker)
                         {

                             maybe.Insert(0, handtile2);

                         }
                     }

                     if (handtile2.value - 1 == maybe[maybe.Count - 1].value) //check maybe without -1
                     {
                         if (colorchecker)
                         {
                             maybe.Add(handtile2);

                         }

                     }

                     if (!colorchecker)
                     {
                         if (handtile2.value == maybe[0].value)
                         {
                             maybe.Add(handtile2);

                         }

                     }
                 }
                 if (maybe.Count >= 3)
                 {
                     board.Add(maybe);

                 }


             }*/

    }
}


