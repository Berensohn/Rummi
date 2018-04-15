using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rummikub
{
    public partial class Form1 : Form
    {
        static Game game = new Game();
        List<Tile> pot = game.bowl;
        List<Tile> handLst = game.hand;
        List<Tile> compLst = game.computer;
        List<List<Tile>> boardLst = new List<List<Tile>>();
        PictureBox CardPictureBox = new PictureBox();
        List<List<Tile>> reserveBoard = new List<List<Tile>>();
        List<Tile> reserveComp = new List<Tile>();
        int sum = 0;
        bool right = false;
        // PictureBox BoardCardPictureBox = new PictureBox();
        //List<PictureBox> handPB = Create_PictureBoxList(game.hand);
        //List<PictureBox> computerPB = Create_PictureBoxList(game.computer);

        public Form1()
        {
           
            InitializeComponent();
            boardTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            computerTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            handTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            

            

            for (int i = 0; i < 14; i++)
            {
                handTable.Controls.Add(handLst[i].getPicture(), i, 0);
                computerTable.Controls.Add(compLst[i].getPicture(), i, 0);
            }

            

            clear();


            AddMouseEventHandlerToHand();
            AddMouseEventHandlerToBoard();
            AddMouseEventHandlerToComputer();
            

        }

        private void AddMouseEventHandlerToBoard()
        {
            foreach (PictureBox dad in this.boardTable.Controls)
            {
                
                dad.MouseClick += new MouseEventHandler(boardclickOnSpace);
            }
        }

        private void AddMouseEventHandlerToHand()
        {
            foreach (PictureBox space in this.handTable.Controls)
            {
                space.MouseClick += new MouseEventHandler(handclickOnSpace);
            }
        }

        private void AddMouseEventHandlerToComputer()
        {
            foreach (PictureBox space in this.computerTable.Controls)
            {
                space.MouseClick += new MouseEventHandler(handclickOnSpace);
            }
        }

        private void clear ()
        {
           /* for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 14; i++)

                {
                    //Tile tile = new Tile(13, Type.CLUBS);
                    Control c = boardTable.GetControlFromPosition(i, j);
                    boardTable.Controls.Remove(c);
                    boardTable.Controls.Add(new PictureBox());
                }
            }*/

            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 14; i++)

                {
                    //Tile tile = new Tile(13, Type.CLUBS);
                    
                    boardTable.Controls.Add(new PictureBox());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            
            Tile t3 = new Tile(7, Type.HEARTS);
            Tile t4 = new Tile(8, Type.HEARTS);
            Tile t5 = new Tile(9, Type.HEARTS);
            //Tile t4 = new Tile(2, Type.SPADES);
            //Tile t5 = new Tile(2, Type.DIAMONDS);
            //Tile t6 = new Tile(2, Type.HEARTS);

            Tile t7 = new Tile(8, Type.DIAMONDS);
            Tile t8 = new Tile(9, Type.DIAMONDS);
            Tile t9 = new Tile(10, Type.DIAMONDS);
            //Tile t10 = new Tile(2, Type.CLUBS);
            /*List<Tile> addition2 = new List<Tile>() { t10 };
            boardLst.Add(addition2);*/
           // List<Tile> addition = new List<Tile>() { t7,t8,t9};
            //boardLst.Add(addition);

            //compLst.RemoveAt(0);
            compLst.Insert(1, t3);
            //compLst.RemoveAt(1);
            compLst.Insert(0, t4);
            //compLst.RemoveAt(1);
            compLst.Insert(2, t5);
            handLst.Insert(0, t7);
            handLst.Insert(1, t8);
            handLst.Insert(2, t9);

            /* Tile t1 = new Tile(5, Type.DIAMONDS);
             Tile t2 = new Tile(5, Type.CLUBS);
             Tile t3 = new Tile(5, Type.SPADES);

             Tile t4 = new Tile(5, Type.HEARTS);


             List<Tile> lst = new List<Tile>() { t1, t2, t3 };
             boardLst.Add(lst);*/


            //compTB(compLst);
            boardTB(boardLst,0);
            handTB(handLst);
            compTB(compLst);

            AddMouseEventHandlerToBoard();
            AddMouseEventHandlerToHand();
            AddMouseEventHandlerToComputer();


        }

        public bool contains_list (List<List<Tile>> reserve, List<Tile> grp)
        {
            int count;
            foreach (List<Tile> lst in reserve)
            {
                count = 1;
                if (lst.Count==grp.Count)
                {
                    for(int i=0;i<lst.Count;i++)
                    {
                        if (lst[i].equals_tile(grp[i]))
                            count++;
                    }
                }
                if (count == lst.Count)
                    return true;
            }
            return false;
        }

        public bool contains_tile (List<Tile> reserve, Tile tile)
        {
            foreach (Tile check in reserve)
            {
                if (tile.equals_tile(check))
                    return true;
            }
            return false;
        }

        public bool Equals_Check_Comp(List<Tile> reserve, List<Tile> comp)
        {
            foreach (Tile check in comp)
            {
                if (check!=null)
                    if (!contains_tile(reserve,check))
                        return false;
            }
            return true;
        }


        public bool Equals_Check_List(List<List<Tile>>reserve, List<List<Tile>> board)
        {

            foreach (List<Tile> grp in board)
            {
                if (contains_list(reserve, grp))
                    return true;
            }
            return false;

            /*List<Tile> reserve2= new List<Tile> ();
            List<Tile> board2 = new List<Tile>();
          
            foreach (List<Tile>grp in reserve)
            {
                foreach(Tile t in grp)
                {
                    if (t!=null)
                    {
                        reserve2.Add(t);
                    }
                }
            }

            foreach (List<Tile> grp1 in board)
            {
                foreach (Tile t1 in grp1)
                {
                    if (t1 != null)
                    {
                        board2.Add(t1);
                    }
                }
            }

           
            foreach (Tile t in reserve2)
            {
                if (!contains(board2,t))
                    return false;
            }
            return true;*/

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
           


            //if (right != false)
            //{
                reserveBoard = new List<List<Tile>>(boardLst);
                reserveComp = new List<Tile>(compLst);
                UpdateBoard();
                AddMouseEventHandlerToComputer();
                boardTable.Visible = false;
               
                //did = 

               
                for (int i = 0; i < compLst.Count; i++)
                {
                    // did = 


                    Algorithm.amen(compLst[i], compLst, boardLst);
                    

                //AddMouseEventHandlerToBoard();
                }
                
                //UpdateBoard();
                //boardTB(boardLst, 0);
                boardTB(boardLst, 0);
                //UpdateBoard();
                //AddMouseEventHandlerToBoard();
                //AddMouseEventHandlerToBoard();

                /*bool nothingHappended=false;
                for (int j = 0; j < 2; j++)
                {


                    for (int i = 0; i < compLst.Count; i++)
                    {
                        Bros bros1 = Algorithm.createColorGroup(compLst[i], compLst, boardLst);
                        if (bros1 != null)
                        {
                            if (!Algorithm.boolRemainder(bros1.board))
                            {
                                compLst = bros1.hand;
                                boardLst = bros1.board;
                                nothingHappended = true;
                                break;
                            }
                        }

                        Bros bros2 = Algorithm.createStraightGroup(compLst[i], compLst, boardLst);
                        if (bros2 != null)
                        {
                            if (!Algorithm.boolRemainder(bros2.board))
                            {
                                compLst = bros2.hand;
                                boardLst = bros2.board;
                                nothingHappended = true;
                                break;
                            }
                        }
                    }
                }*/
                /* foreach (List<Tile> group in boardLst)
                 {
                     foreach (Tile t in group)
                     {
                         MessageBox.Show(t.type.ToString() + t.value.ToString());
                     }
                     // break;

                 }*/



                /*if (Equals_Check_Comp(reserveComp, compLst))

                {
                    Random random = new Random();
                    int r = random.Next(0, game.bowl.Count - 1);
                    Tile take = game.bowl[r];
                    //compLst.RemoveAt(0);
                    compLst.Insert(0, take);
                    //compTB(compLst);
                    computerTable.Controls.Add(take.picture);
                    AddMouseEventHandlerToComputer();
                    game.bowl.Remove(take);
                }*/


                if (handLst.Count == 0)
                    MessageBox.Show("You Win!!!");
                if (compLst.Count == 0)
                    MessageBox.Show("Computer Wins! :(");

                /*Algorithm.Easy(boardLst, compLst);

                if (nothingHappended)
                    {
                        Random random = new Random();
                        int r = random.Next(0, game.bowl.Count - 1);
                        Tile take = game.bowl[r];
                        compLst.Insert(0, take);
                        computerTable.Controls.Add(take.picture);
                        //AddMouseEventHandlerToHand();
                        game.bowl.Remove(take);
                    }*/








                /*foreach (List<Tile> group in boardLst)
                {
                    foreach (Tile t in group)
                    {
                        MessageBox.Show(t.type.ToString() + t.value.ToString());
                    }
                    break;

                }*/

                /* List<List<Tile>> clearlst = Algorithm.getRemainder(boardLst);
                 foreach (List<Tile> lst in clearlst)
                 {
                     if (lst != null)
                         boardLst.Remove(lst);

                 }*/

                //boardTB(boardLst, 0);
                //AddMouseEventHandlerToBoard();
                UpdateBoard();
                boardTable.Visible = true;
            // compTB(compLst);
            //AddMouseEventHandlerToComputer();
            //}
            //else
            //{
            //MessageBox.Show("Enter right answer!");
            //}

           /* foreach (List<Tile> grp in boardLst)
            {
                foreach (Tile t in grp)
                {
                    MessageBox.Show(t.ToString());
                }
                MessageBox.Show("End of group");
            }*/
        }

           
        public void compTB(List<Tile> comp)
        { 
            for (int i = 0; i < comp.Count; i++)
            {
                computerTable.Controls.Remove(computerTable.GetControlFromPosition(i, 0));
                computerTable.Controls.Add(comp[i].getPicture(), i, 0);
            }

        }

        public void handTB(List<Tile> hand)
        {
            /*for (int i = 0; i <14; i++)
            {
                handTable.Controls.Remove(handTable.GetControlFromPosition(i,0));
                

            }*/
            for (int i = 0; i < hand.Count; i++)
            {
                //handTable.Controls.Remove(handTable.GetControlFromPosition(i, 0));
                handTable.Controls.Add(hand[i].getPicture(), i, 0);

            }
        }

        public void groupTB(List<Tile> group,int row,int column)
        {
            int i;
            for (i = 0; i < group.Count; i++)
            {
                //boardTable.Controls.Remove(boardTable.GetControlFromPosition(i, row));
                //MessageBox.Show(group[i].type.ToString() + group[i].value.ToString());
                //group[i].picture.MouseClick += new MouseEventHandler(boardclickOnSpace);
                boardTable.Controls.Add(group[i].picture, /*new TableLayoutPanelCellPosition*/column, row);
                column++;

            }
            /*PictureBox pb = new PictureBox();
            pb.Image = Properties.Resources.back;
            boardTable.Controls.Add(pb, /*new TableLayoutPanelCellPositioncolumn, row);*/


        }

        public void boardTB(List<List<Tile>> board, int row)
        {
            //clear();
            
            int column = 0;
            foreach (List<Tile> group in board)
            {
                if (group.Count>=3)
                {
                    groupTB(group, row,column);

                    row++;
                }
                if (row > 9)
                {
                    column = 6;
                    row = 0;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            MessageBox.Show(sum.ToString());
            String answer = textBox1.Text;


            if (answer.Equals(sum.ToString()))
            {
                MessageBox.Show("Correct!");
                right = true;
                sum = 0;
            }
            else
                MessageBox.Show("Wrong!");



        }
        
        /// <summary>
        /// gets the tile that was clicked on and inserts it into CardPictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void handclickOnSpace(object sender, MouseEventArgs e)
        {
           
            PictureBox check = (PictureBox)sender;
            Tile t = new Tile(check);
            sum += t.value;
            CardPictureBox = check;
           // Tile tile = new Tile(check);
            //handLst.Remove(tile);

        }

       


        /// <summary>
        /// pastes the tile onto the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void boardclickOnSpace(object sender, MouseEventArgs e)
        {
            TableLayoutPanelCellPosition position = boardTable.GetPositionFromControl((PictureBox)sender);
            
            if (CardPictureBox!=null)
                boardTable.Controls.Add(CardPictureBox, position.Column, position.Row);
            CardPictureBox = null;
            
        }

            
            
            
            


         

        /// <summary>
        /// 
        /// Method that updates the backend of the board by the UI
        /// </summary>
        

       
        public void UpdateBoard()
        {
            
            List<Tile> lst = new List<Tile>();
            List<List<Tile>> newboard = new List<List<Tile>>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    //Debug.Write(i.ToString(),j.ToString());
                    PictureBox pb = (PictureBox)boardTable.GetControlFromPosition(j, i);
                    if (pb.Image!=null)
                    {
                        pb.MouseClick += new MouseEventHandler(handclickOnSpace);
                        Tile tile = new Tile(pb);
                       // MessageBox.Show(tile.value.ToString());
                        lst.Add(tile);
                        handLst.Remove(tile);
                        

                    }
                    else
                    {
                        //foreach (Tile t in lst)
                        //MessageBox.Show(t.value.ToString());
                        if (lst.Count >= 3)
                            newboard.Add(lst);
                        else
                        {
                            foreach (Tile tile in lst)
                            {
                                boardTable.Controls.Remove(tile.picture);
                            }
                        }
                        lst = new List<Tile>();
                    }

                }
            }
            boardLst = newboard;
            boardTB(boardLst, 0);
            //AddMouseEventHandlerToBoard();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random random= new Random();
            int r = random.Next(0, game.bowl.Count - 1);
            Tile take = game.bowl[r];
            //handLst.RemoveAt(0);
            handLst.Insert(0,take);
            //handTB(handLst);
            handTable.Controls.Add(take.picture);
            AddMouseEventHandlerToHand();
            game.bowl.Remove(take);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            boardTable.Visible = false;
            boardLst = new List<List<Tile>> (reserveBoard);
            compLst = new List<Tile>(reserveComp);
            compTB(compLst);
            boardTB(boardLst,0);
            boardTable.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            
            AddMouseEventHandlerToHand();
            boardTable.Visible = false;


            for (int j = 0; j < 2;j++)
            {
                Algorithm.Easy(boardLst, handLst);
                for (int i = 0; i < handLst.Count; i++)
                {



                    Algorithm.amen(handLst[i], handLst, boardLst);


                }
                Algorithm.Easy(boardLst, handLst);
            }
            //UpdateBoard();
            //boardTB(boardLst, 0);
            boardTB(boardLst, 0);
            UpdateBoard();
           

            if (handLst.Count == 0)
                MessageBox.Show("You Win!!!");
            if (compLst.Count == 0)
                MessageBox.Show("Computer Wins! :(");

            UpdateBoard();
            boardTable.Visible = true;
           
        }

        
    }
}


/*private Point BoardTable_MouseClick(object sender, MouseEventArgs e)
        {
            int row = 0;
            int column = 0;
            int verticalOffset = 0;
            
            foreach (int h in boardTable.GetRowHeights())
            {
                column = 0;
                int horizontalOffset = 0;
                foreach (int w in boardTable.GetColumnWidths())
                {
                    Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                    if (rectangle.Contains(e.Location))
                    {
                        Debug.WriteLine(String.Format("row {0}, column {1} was clicked", row, column));
                        
                    }
                    horizontalOffset += w;
                    column++;
                }
                verticalOffset += h;
                row++;
            }
            return new Point(row, column);


        }*/

/*private void Move_Picture(object sender, MouseEventArgs e)
{
    TableLayoutPanelCellPosition handcellPos = handclickOnSpace(sender,e);
    PictureBox control = (PictureBox)handTable.GetControlFromPosition(handcellPos.Column, handcellPos.Row);
    TableLayoutPanelCellPosition boardcellPos = boardclickOnSpace(sender, e);
    boardTable.Controls.Add(control, boardcellPos.Column, boardcellPos.Row);

}*/

/*private void HandTable_Click(object sender, MouseEventArgs e)
{
    //List<PictureBox> Mouse = new List<PictureBox>();
    TableLayoutPanelCellPosition cellPos= handclickOnSpace(sender,e);
    //Mouse.Add((PictureBox)handTable.GetControlFromPosition(cellPos.X,cellPos.Y));
    MessageBox.Show(cellPos.X.ToString(), cellPos.Y.ToString());
}*/

/*(private Point GetRowColIndex(TableLayoutPanel tlp, Point point)
{
    if (point.X > tlp.Width || point.Y > tlp.Height)
        return new Point(-1,-1);

    int w = tlp.Width;
    int h = tlp.Height;
    int[] widths = tlp.GetColumnWidths();

    int i;
    for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
        w -= widths[i];
    int col = i + 1;

    int[] heights = tlp.GetRowHeights();
    for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
        h -= heights[i];

    int row = i + 1;

    MessageBox.Show(col.ToString(), row.ToString());
    return new Point(col, row);
}*/


//Tile tile = new Tile(CardPictureBox);
//MessageBox.Show(tile.type.ToString());


//MessageBox.Show("Cell chosen: (" +position+")");

/*if (CardPictureBox != null)
{
    boardTable.Controls.Add(CardPictureBox, position.Column, position.Row);
    CardPictureBox = null;
}
else
{
    boardTable.Controls.Add(BoardCardPictureBox, position.Column, position.Row);
}*/



//MessageBox.Show(tile.value.ToString());
/*TableLayoutPanelCellPosition handposition = handTable.GetPositionFromControl((PictureBox)sender);
TableLayoutPanelCellPosition boardposition = boardTable.GetPositionFromControl((PictureBox)sender);

if (handposition!=null)
{
    Control handcontrol = handTable.GetControlFromPosition(handposition.Column, handposition.Row);
    CardPictureBox = (PictureBox)handcontrol;
}


// MessageBox.Show("Cell chosen: (" +handTable.GetPositionFromControl((PictureBox)sender) + ")");

if (boardposition != null)
{

    Control boardcontrol = boardTable.GetControlFromPosition(boardposition.Column, boardposition.Row);
    CardPictureBox = (PictureBox)boardcontrol;
}*/
// Tile tile = new Tile(CardPictureBox);
//MessageBox.Show(tile.type.ToString());


/*public void boardclickOnSpaceTwice(object sender, MouseEventArgs e)
   {
       TableLayoutPanelCellPosition position = boardTable.GetPositionFromControl((PictureBox)sender);
       Control control = boardTable.GetControlFromPosition(position.Column, position.Row);
       //MessageBox.Show("Cell chosen: (" +handTable.GetPositionFromControl((PictureBox)sender) + ")");
       BoardCardPictureBox = (PictureBox)control;
   }*/



/* private void button2_Click(object sender, EventArgs e)
        {

            foreach (List<Tile> group in boardLst)
            {
                foreach (Tile t in group)
                {
                    MessageBox.Show(t.type.ToString() + t.value.ToString());
                }
            }

            //Algorithm.Easy(boardLst, compLst);

            //List<Tile> straight = Algorithm.createStraightGroup(compLst[0],compLst,boardLst);
            //List<Tile> color = Algorithm.createColorGroup(compLst[3], compLst, boardLst);
            //List<List<Tile>> newboard = new List<List<Tile>>();

            //foreach (Tile tile in compLst)
            //{
            /* List<Tile> cs = Algorithm.createStraightGroup(compLst[2], compLst, boardLst);
             foreach (Tile t in cs)
                 MessageBox.Show(t.type.ToString() + t.value.ToString());
             if (cs!=null&&cs.Count>=3)
             {
                 boardLst.Add(cs);
             }*/

/*Bros cg = Algorithm.createColorGroup(compLst[0], compLst, boardLst);
if (cg != null&&cg.group.Count>=3)
{
    boardLst = cg.board;
}*/

///}

//boardTB(boardLst,0);

//Algorithm.amen(boardLst, compLst);
//List<Tile> color= Algorithm.createColorGroup(handLst[3], handLst, boardLst);
//Algorithm.createColorGroup(compLst[3], compLst, boardLst);
//Algorithm.amen(boardLst, compLst);
//compTB(compLst);
//groupTB(straight,0);
//groupTB(color,1);
/*List<List<Tile>> reallyboard = new List<List<Tile>>();
foreach (List<Tile> lst in newboard)
    if (lst.Count >= 3)
        reallyboard.Add(lst);
boardTB(boardLst,0);*/



//}


/*private static List<PictureBox> Create_PictureBoxList(List<Tile>tileGroup)
    {
        List<PictureBox> group = new List<PictureBox>();
        int i = 0;
        foreach(Tile tile in tileGroup)
        {
            group.Add(tile.getPicture());
            group[i].SizeMode = PictureBoxSizeMode.StretchImage;
            group[i].Size = new Size(45, 60);
            i++;

        }

        return group;
    }

   /* private static PictureBox Create_PictureBox(Tile tile)
    {
        PictureBox pb = new PictureBox();

        pb = tile.getPicture();

        pb.SizeMode = PictureBoxSizeMode.StretchImage;
        pb.Size = new Size(45, 60);

        return pb;
    }*/
