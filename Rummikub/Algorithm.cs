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
        /// Main Algorithm of the progect it plays a move and tries to get rid of as much tiles it can from the playing hand
        /// </summary>
        /// <param name="board"></param>
        /// <param name="hand"></param>
        /// <param name="tile"></param>
        /// <returns> void </returns>

        private static bool notspecial = true;
        private static List<List<Tile>> inputboard;
        private static List<Tile> inputcomp;




        public static void amen(Tile tile, List<Tile> comp, List<List<Tile>> board)
        {


            Bros colorbros = createColorGroup(tile, comp, board);
            if (colorbros != null)
            {

                board = new List<List<Tile>>(colorbros.board);
                comp = new List<Tile>(colorbros.hand);

            }
            else
            {
                Bros straightbros = createStraightGroup(tile, comp, board);
                if (straightbros != null)
                {

                    board = new List<List<Tile>>(straightbros.board);
                    comp = new List<Tile>(straightbros.hand);
                }
            }
            /* if (!boolRemainder(board))
             {

                 return;

             }*/


        }

        public static Bros disorder(List<Tile> outcomp, List<List<Tile>> outboard)
        {
            outboard.Capacity = 15;
            outcomp.Capacity = 15;
            inputboard = new List<List<Tile>>(outboard);
            inputcomp = new List<Tile>(outcomp);

            inputboard.Capacity = 15;
            inputcomp.Capacity = 15;

            List<List<Tile>> board = new List<List<Tile>>(inputboard);
            List<Tile> comp = new List<Tile>(inputcomp);

            notspecial = false;
            foreach(List<Tile>group in inputboard)
            { 
                board=new List<List<Tile>>(inputboard);
                comp = new List<Tile>(inputcomp);
                board.Capacity = 15;
                comp.Capacity = 15;
                List<Tile> modifiedGroup = new List<Tile>(group);

                int length = group.Count;
                int count = 0;
                //foreach(Tile tile in group)
                for (int i =0;i<group.Count;i++)
                {
         
                    Bros colorbros = createColorGroup(modifiedGroup[i], comp, board);
                    if (colorbros == null)
                    {

                        Bros straightbros = createStraightGroup(modifiedGroup[i], comp, board);
                        if (straightbros == null)
                            break;
                        else
                        {
                            board = (straightbros.board);
                            comp = (straightbros.hand);
                            count++;
                        }
                    }
                    else
                    {
                        board = (colorbros.board);
                        comp = (colorbros.hand);
                        count++;
                    }

                        
                }
                if (count == length && !boolRemainder(board))
                {
                    notspecial = true; 
                    return new Bros(board, comp, null);
                }
                

            }
            notspecial = true;
            return null;
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
                        colorGroup.Add(new Tile(handTile));


                    }
                }
            }




            foreach (List<Tile> group in board)
            {
                if (notspecial)
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
                                colorGroup.Add(new Tile(group[0]));
                                group.RemoveAt(0);
                            }

                        }




                        if (tile.value == group[group.Count - 1].value)
                        {
                            bool check = true;
                            for (int i = 0; i < colorGroup.Count; i++)
                            {
                                if ((group[group.Count - 1].type == colorGroup[i].type))
                                    check = false;

                            }
                            if (check)
                            {
                                colorGroup.Add(new Tile(group[group.Count - 1]));
                                group.RemoveAt(group.Count - 1);

                            }

                        }

                    }
                    //extractedColor(tile, colorGroup, group);

                }

                else
                {
                    if (group.Count != 0)
                    {
                        for (int j = 0; j < group.Count; j++)
                        {
                            bool validColor = true;
                            for (int z = 0; z < colorGroup.Count; z++)
                            {
                                if (colorGroup[z].type == group[j].type)
                                    validColor = false;

                            }
                            if (validColor && group[j].value == colorGroup[0].value)
                            {
                                colorGroup.Add(new Tile(group[j]));
                                group.RemoveAt(j);
                                break;
                            }


                        }









                        /*

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
                                group.RemoveAt(0);
                            }

                        }

                    }

                    /*if (group.Count != 0)
                    {
                        if (tile.value == group[group.Count - 1].value)
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
                                group.RemoveAt(group.Count - 1);

                            }

                        }
                    
                    }*/

                        //extractedColor(tile, colorGroup, group);
                    }
                  }

                }
                if (colorGroup.Count >= 3)
                {
                    board.Add(new List<Tile>(colorGroup));


                    foreach (Tile colortile in colorGroup)
                        hand.Remove(colortile);


                    return new Bros(board, hand, colorGroup);
                }

                //return false;

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

                        straightGroup.Insert(0, new Tile(handTile));

                    }
                    else
                    {
                        if (straightGroup[straightGroup.Count - 1].value + 1 == handTile.value)
                        {
                            straightGroup.Add(new Tile(handTile));
                        }
                    }



                }
            }



            foreach (List<Tile> group in board)
            {
                bool ColorGroupBool = true;
                if (group.Count <= 1)
                {
                    ColorGroupBool = true;
                }
                else if (group[0].type == group[1].type)
                    ColorGroupBool = false;

                if (notspecial)
                {
                    if (group.Count > 3)
                    {
                        if (!ColorGroupBool)
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

                                    straightGroup.Insert(0, new Tile(group[0]));
                                    group.RemoveAt(0);

                                }
                                else
                                {
                                    if (straightGroup[straightGroup.Count - 1].value + 1 == group[0].value)
                                    {
                                        straightGroup.Add(new Tile(group[0]));
                                        group.RemoveAt(0);

                                    }
                                }


                            }





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

                                    straightGroup.Insert(0, new Tile(group[group.Count - 1]));
                                    group.RemoveAt(group.Count - 1);

                                }
                                else
                                {
                                    if (straightGroup[straightGroup.Count - 1].value + 1 == group[group.Count - 1].value)
                                    {
                                        straightGroup.Add(new Tile(group[group.Count - 1]));
                                        group.RemoveAt(group.Count - 1);

                                    }
                                }


                            }
                        }

                        else //might need to remove tile from group
                        {
                            //for color groups

                            if (straightGroup[0].value - 1 == group[0].value)
                            {
                                foreach (Tile colortile in group)
                                {
                                    if (colortile.type == straightGroup[0].type)
                                    {
                                        straightGroup.Insert(0, new Tile(colortile));
                                        group.Remove(colortile);
                                    }
                                }
                            }
                            else if (straightGroup[straightGroup.Count - 1].value + 1 == group[0].value)
                            {
                                foreach (Tile colortile in group)
                                {
                                    if (colortile.type == straightGroup[0].type)
                                    {
                                        straightGroup.Add(new Tile(colortile));
                                        group.Remove(colortile);
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    if (group.Count > 0)
                    {

                        for (int j = 0; j < group.Count; j++)
                        {
                            bool validStraight = true;
                            for (int z = 0; z < straightGroup.Count; z++)
                            {
                                if (straightGroup[z].type != group[j].type)
                                    validStraight = false;

                            }
                            if (validStraight && group[j].value - 1 == straightGroup[straightGroup.Count - 1].value)
                            {
                                straightGroup.Add(new Tile(group[j]));
                                group.RemoveAt(j);
                                break;

                            }

                            if (validStraight && group[j].value + 1 == straightGroup[0].value)
                            {
                                straightGroup.Insert(0, new Tile(group[j]));
                                group.RemoveAt(j);
                                break;
                            }


                        }

                        /*if (!ColorGroupBool)
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
                                    group.RemoveAt(0);

                                }
                                else
                                {
                                    if (straightGroup[straightGroup.Count - 1].value + 1 == group[0].value)
                                    {
                                        straightGroup.Add(group[0]);
                                        group.RemoveAt(0);

                                    }
                                }


                            }





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
                                    group.RemoveAt(group.Count - 1);

                                }
                                else
                                {
                                    if (straightGroup[straightGroup.Count - 1].value + 1 == group[group.Count - 1].value)
                                    {
                                        straightGroup.Add(group[group.Count - 1]);
                                        group.RemoveAt(group.Count - 1);

                                    }
                                }


                            }
                        }

                        else //might need to remove tile from group
                        {
                            //for color groups

                            if (straightGroup[0].value - 1 == group[0].value)
                            {
                                foreach (Tile colortile in group)
                                {
                                    if (colortile.type == straightGroup[0].type)
                                    {
                                        straightGroup.Insert(0, colortile);
                                        group.Remove(colortile);
                                        break;
                                    }
                                }
                            }
                            else if (straightGroup[straightGroup.Count - 1].value + 1 == group[0].value)
                            {
                                foreach (Tile colortile in group)
                                {
                                    if (colortile.type == straightGroup[0].type)
                                    {
                                        straightGroup.Add(colortile);
                                        group.Remove(colortile);
                                    }
                                }
                            }

                        }*/
                    }
                }





            }
            if (straightGroup.Count >= 3)
            {
                board.Add(new List<Tile> (straightGroup));


                for (int j = 0; j < straightGroup.Count; j++)
                {
                    hand.Remove(straightGroup[j]);

                }


                return new Bros(board, hand, straightGroup);


            }

            return null;
        }

        private static void extractedstraight(List<Tile> straightGroup, List<Tile> group, bool ColorGroupBool)
        {
            
        }



        /// <summary>
        /// checks to see if it can add a tile to the beginning or end of any group on the board
        /// </summary>
        /// <param name="board"></param>
        /// <param name="hand"></param>
        public static void Easy(List<List<Tile>> board, List<Tile> hand)
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
                                    return;
                                }
                            }

                            if (tile.value - 1 == lst[lst.Count - 1].value)
                            {
                                if (colorchecker_straight)
                                {
                                    lst.Add(tile);
                                    return;
                                }

                            }

                            if (colorchecker_color)
                            {
                                if (valuechecker)
                                {
                                    lst.Add(tile);
                                    return;
                                }

                            }
                        }
                    }


                }

            }



        }


    }
}


