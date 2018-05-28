using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        List<Tile> reserveHand = new List<Tile>();
        static  Random random = new Random();
        int boardsum;
        List<int> chosen;
        string path = @"C:\Users\yehon\Desktop\Rummikub\Game.txt";
        bool right = false;
        
        /// <summary>
        /// initializes the board. Gives computer hand and player hand each 14 cards.
        /// </summary>
        public Form1()
        {
            File.Delete(@"C:\Users\yehon\Desktop\Rummikub\Game.txt");
            InitializeComponent();
            boardTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            computerTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            handTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            
            for (int i = 0; i < 14; i++)
            {
                handTable.Controls.Add(handLst[i].getPicture(), i, 0);
                computerTable.Controls.Add(compLst[i].getPicture(), i, 0);
            }



            fillBoard();


            AddMouseEventHandlerToHand();
            AddMouseEventHandlerToBoard();
            AddMouseEventHandlerToComputer();
            

        }

        /// <summary>
        /// Adds an event handler to all tiles on the board, making it possible to move them around with mouse clicks.
        /// </summary>
        private void AddMouseEventHandlerToBoard()
        {
            foreach (PictureBox dad in this.boardTable.Controls)
            {
                
                dad.MouseClick += new MouseEventHandler(boardclickOnSpace);
            }
        }

        /// <summary>
        /// Adds an event handler to all tiles in player hand, making it possible to move them around with mouse clicks.
        /// </summary>
        private void AddMouseEventHandlerToHand()
        {
            foreach (PictureBox space in this.handTable.Controls)
            {
                space.MouseClick += new MouseEventHandler(handclickOnSpace);
            }
        }

        /// <summary>
        /// Adds an event handler to all tiles in computer hand, making it possible to move them around with mouse clicks.
        /// </summary>
        private void AddMouseEventHandlerToComputer()
        {
            foreach (PictureBox space in this.computerTable.Controls)
            {
                space.MouseClick += new MouseEventHandler(handclickOnSpace);
            }
        }

        /// <summary>
        /// Adds a blank picture box to all boxes in table making it possible to use control methods
        /// </summary>
        private void fillBoard ()
        {
        
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                   
                    boardTable.Controls.Add(new PictureBox());
                }
            }
        }
        

        /// <summary>
        /// button initilializes player hand and computer hand with good tile cards for a more enjoyable game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initialize_Click(object sender, EventArgs e)
        {
            Tile t0 = new Tile(1, Type.CLUBS);
            Tile t1 = new Tile(2, Type.CLUBS);
            Tile t2 = new Tile(3, Type.CLUBS);

            Tile t3 = new Tile(7, Type.HEARTS);
            Tile t4 = new Tile(8, Type.HEARTS);
            Tile t5 = new Tile(9, Type.HEARTS);
            Tile t6 = new Tile(10, Type.HEARTS);

            Tile t7 = new Tile(4, Type.SPADES);
            Tile t8 = new Tile(5, Type.SPADES);
            Tile t9 = new Tile(6, Type.SPADES);

            Tile t10 = new Tile(7, Type.SPADES);
            Tile t11 = new Tile(8, Type.SPADES);
            Tile t12 = new Tile(9, Type.SPADES);
            Tile t13 = new Tile(10, Type.SPADES);


            List<Tile> perfectcomp = new List<Tile> { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13};
            compLst = new List<Tile>(perfectcomp);

            
            Tile m1 = new Tile(5, Type.CLUBS);
            Tile m2 = new Tile(5, Type.HEARTS);
            Tile m3 = new Tile(6, Type.DIAMONDS);

            Tile m4 = new Tile(2, Type.SPADES);
            Tile m5 = new Tile(3, Type.SPADES);
            Tile m6 = new Tile(6, Type.CLUBS);

            Tile m7 = new Tile(7, Type.CLUBS);
            Tile m8 = new Tile(7, Type.HEARTS);

            Tile m9 = new Tile(4, Type.DIAMONDS);
            Tile m10 = new Tile(4, Type.CLUBS);

            Tile m11 = new Tile(6, Type.HEARTS);
            Tile m12 = new Tile(4, Type.HEARTS);
            Tile m13 = new Tile(5, Type.DIAMONDS);

            List<Tile> perfecthand = new List<Tile> { m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13 };
            handLst = new List<Tile>(perfecthand);
           
            
            boardTB(boardLst,0);
            handTB(handLst);
            compTB(compLst);

            AddMouseEventHandlerToBoard();
            AddMouseEventHandlerToHand();
            AddMouseEventHandlerToComputer();


        }

        
        
        /// <summary>
        /// button that plays turn for computer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Click(object sender, EventArgs e)
        {
            
            //if (right)
            {
                boardTable.Visible = false;
                int current = compLst.Count;
                reserveBoard = new List<List<Tile>>(boardLst);
                reserveComp = new List<Tile>(compLst);
                reserveHand = new List<Tile>(handLst);
                File.AppendAllText(path, Environment.NewLine + "---------------------COMPUTER TURN---------------------" + Environment.NewLine + Environment.NewLine);
                File.AppendAllText(path, "------------------------BEFORE------------------------" + Environment.NewLine);
                UpdateBoard();
                File.AppendAllText(path, "------------------------AFTER------------------------" + Environment.NewLine);

                AddMouseEventHandlerToComputer();

                Algorithm.Easy(boardLst, compLst);
                for (int j = 1; j <= 2; j++)
                {
                    for (int i = 0; i < compLst.Count; i++)
                    {

                        Algorithm.amen(compLst[i], compLst, boardLst);


                    }
                }
                Algorithm.Easy(boardLst, compLst);

                boardTB(boardLst, 0);
               
                if (current == compLst.Count)

                {

                    int r = random.Next(0, game.bowl.Count - 1);
                    Tile take = game.bowl[r];
                    
                    compLst.Insert(0, take);
                    
                    computerTable.Controls.Add(take.picture);
                    AddMouseEventHandlerToComputer();
                    game.bowl.Remove(take);
                }
                
                WinOrLose();

                UpdateBoard();
                
                boardTable.Visible = true;
                right = false;
                
            }
           
        }

        /// <summary>
        /// method that adds tiles to computer table. It connects between the backend to the UI.
        /// </summary>
        /// <param name="comp"></param>       
        public void compTB(List<Tile> comp)
        { 
            for (int i = 0; i < comp.Count; i++)
            {
                computerTable.Controls.Remove(computerTable.GetControlFromPosition(i, 0));
                computerTable.Controls.Add(comp[i].getPicture(), i, 0);
            }

        }

        /// <summary>
        /// method that adds tiles to player table. It connects between the backend to the UI.
        /// </summary>
        /// <param name="hand"></param>
        public void handTB(List<Tile> hand)
        {
           
            for (int i = 0; i < hand.Count; i++)
            {
                handTable.Controls.Remove(handTable.GetControlFromPosition(i, 0));
                handTable.Controls.Add(hand[i].getPicture(), i, 0);

            }
        }

        public void groupTB(List<Tile> group,int row,int column)
        {
            int i;
            for (i = 0; i < group.Count; i++)
            {
                
                boardTable.Controls.Add(group[i].picture, column, row);
                column++;

            }
            
        }

        /// <summary>
        /// method that adds the board list to the board table. It connects between the backend to the UI.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        public void boardTB(List<List<Tile>> board, int row)
        {
          
            
            int column = 0;
            foreach (List<Tile> group in board)
            {
                if (group.Count>=3)
                {
                    groupTB(group, row,column);

                    row++;
                }
                if (row > 7)
                {
                    column = 6;
                    row = 0;
                }
            }
        }

        /// <summary>
        /// special feature. My math teacher asked us to build a 
        /// math game for a better learning experience for students. 
        /// I added this button which askes you a math question in patterns
        /// based on the sum of the tiles on the board.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pattern_Click(object sender, EventArgs e)
        {
            boardsum = UpdateBoard();
            MessageBox.Show(boardsum.ToString());
            

            List<List<int>> boardsum_Lists = new List<List<int>> ();

            foreach (List<int> lst in MathProject.sdarot)
            {
                if (lst.Contains(boardsum))
                {
                    boardsum_Lists.Add(lst);
                }
                    
            }
            if (boardsum_Lists.Count > 2)
            {
                boardsum_Lists.RemoveAt(0);
                boardsum_Lists.RemoveAt(1);
            }

            int r = random.Next(0, boardsum_Lists.Count - 1);
            chosen = boardsum_Lists[r];

            MessageBox.Show("The sum of all the tiles on the board is " + boardsum + " , enter in the text box the placement of " + boardsum + " in the pattern: " + MathProject.ListToString(chosen)+". Click on the Check button ->");
           
            
        }

        /// <summary>
        /// checks if the answer to the pattern test
        /// question is correct and pops a message box with
        /// either "correct" or "wrong"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBtn_Click(object sender, EventArgs e)
        {
            String answer = textBox1.Text;
            if (answer.Equals((chosen.IndexOf(boardsum) + 1).ToString()))
            {
                MessageBox.Show("Correct!");
                right = true;

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
            //sum += t.value;
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
        /// Method that updates the backend of the board by the UI
        /// </summary>
        public int UpdateBoard()
        {
            

            int boardsum = 0;  
            List<Tile> lst = new List<Tile>();
            List<List<Tile>> newboard = new List<List<Tile>>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                   
                    PictureBox pb = (PictureBox)boardTable.GetControlFromPosition(j, i);
                    if (pb.Image!=null)
                    {
                        pb.MouseClick += new MouseEventHandler(handclickOnSpace);
                        Tile tile = new Tile(pb);
                        boardsum += tile.value;
                       
                        lst.Add(tile);
                        File.AppendAllText(path, tile.ToString());
                       
                        handLst.Remove(tile);
                        
                    }
                    else if (lst.Count>=6)
                    {
                        List<Tile> split = new List<Tile> {lst[lst.Count-3],lst[lst.Count - 2],lst[lst.Count - 1] };

                        lst.RemoveAt(lst.Count - 3);
                        lst.RemoveAt(lst.Count - 2);
                        lst.RemoveAt(lst.Count - 1);

                        newboard.Add(lst);
                        
                        newboard.Add(split);
                        File.AppendAllText(path, Environment.NewLine);
                        lst = new List<Tile>();
                       
                    }
                    else
                    {
                       
                        if (lst.Count >= 3)
                        {
                            newboard.Add(lst);
                            File.AppendAllText(path, Environment.NewLine);
                         
                        }
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
            
            return boardsum;
         
        }


        /// <summary>
        /// gives player hand random new card from bowl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeNewBtn_Click(object sender, EventArgs e)
        {
            
            int r = random.Next(0, game.bowl.Count - 1);
            Tile take = game.bowl[r];
            handLst.Insert(0,take);
            handTable.Controls.Add(take.picture);
            AddMouseEventHandlerToHand();
            game.bowl.Remove(take);
            
        }

        /// <summary>
        /// allows you to undo a move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoBtn_Click(object sender, EventArgs e)
        {
            boardTable.Visible = false;
            boardLst = new List<List<Tile>> (reserveBoard);
            compLst = new List<Tile>(reserveComp);
            handLst = new List<Tile>(reserveHand);
            compTB(compLst);
            handTB(handLst);
            boardTB(boardLst,0);
            boardTable.Visible = true;
        }

        /// <summary>
        /// suggestion button for the player. plays the move for him.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void suggestBtn_Click(object sender, EventArgs e)
        {
            reserveBoard = new List<List<Tile>>(boardLst);
            reserveComp = new List<Tile>(compLst);
            reserveHand = new List<Tile>(handLst);

            File.AppendAllText(path, Environment.NewLine + "---------------------PLAYER TURN---------------------" + Environment.NewLine + Environment.NewLine);
            File.AppendAllText(path, "------------------------BEFORE------------------------" + Environment.NewLine);
            UpdateBoard();
            File.AppendAllText(path, "------------------------AFTER------------------------" + Environment.NewLine);

            AddMouseEventHandlerToHand();
            boardTable.Visible = false;


           
            Algorithm.Easy(boardLst, handLst);
            for (int j = 1; j <= 2; j++)
            {
                for (int i = 0; i < handLst.Count; i++)
                {

                    Algorithm.amen(handLst[i], handLst, boardLst);



                }
            }
            Algorithm.Easy(boardLst, handLst);

           
            boardTB(boardLst, 0);
            UpdateBoard();

            WinOrLose();

            boardTable.Visible = true;

        }

        /// <summary>
        /// checks if either the player or the computer wins the game.
        /// if so, pops a message box with whomever won.
        /// </summary>
        private void WinOrLose()
        {
            if (handLst==null)
                MessageBox.Show("You Win!!!");
            if (compLst == null)
                MessageBox.Show("Computer Wins! :(");
        }


       
    }
}


