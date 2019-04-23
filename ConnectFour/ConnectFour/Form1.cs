using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class ConnectFour : Form
    {
        private Rectangle[] boardColumns;
        private int[,] board;
        private int turn;
        private int turnCounter = 0;
        public ConnectFour()
        {
            InitializeComponent();
            this.boardColumns = new Rectangle[7];
            this.board = new int[6, 7];
            //MessageBox.Show(this.board[0, 0].ToString());
            this.turn = 1;
        }
        private void ConnectFour_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LimeGreen, 24, 24, 340, 300);
            for(int i=0; i<6; i++)
            {
                for(int j=0; j<7;j++)
                {
                    if (i == 0)
                    {
                        this.boardColumns[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
                    }
                    e.Graphics.FillEllipse(Brushes.BlanchedAlmond, 32 + 48 * j, 32 + 48 * i, 32, 32);
                }
            }
        }
        private void ConnectFour_MouseClick(object sender, MouseEventArgs e)
        {
            int colIndex = this.ColumnNumber(e.Location);
            //Console.WriteLine(e.Location); //Debugging
            if(colIndex != -1)
            {
                int rowIndex = this.EmptyRow(colIndex);
                if (rowIndex != -1)
                {
                    //These fill the board with the right color
                    this.board[rowIndex, colIndex] = this.turn;
                    if (this.turn == 1)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.SlateGray, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        turnDisplay.Text = "Computer's Turn next";
                        turnCounter++;
                    }
                    if (this.turn == 2)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.HotPink, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        turnDisplay.Text = "Player's Turn next";
                        turnCounter++;
                    }
                }
            }
            int winner = WinnerPlayer(this.turn);
            if (winner != -1)
            {
                string winsy = (winner == 1) ? "Player" : "Computer";
                MessageBox.Show(winsy + " wins");
                Application.Restart();
            }

            if (this.turn == 1)
                turn = 2;
            else
                turn = 1;
            if(turnCounter >0)
            {
                PlayerButton.Enabled = false;
                PCButton.Enabled = false;
            }
        }
        //Series of checks to see if a win state is found, this could probably be refactored into a better method but we can do that later
        private int WinnerPlayer(int playerToCheck)
        {
            //Vertical Checks
            for(int row = 0; row < this.board.GetLength(0)-3; row++)
            {
                for(int col =0; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col], this.board[row + 2, col], this.board[row + 3, col]))
                        return playerToCheck;
                }
            }
            //Horizontal Checks
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1)-3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row, col+1], this.board[row, col+2], this.board[row, col + 3]))
                        return playerToCheck;
                }
            }
            //Top-left to Bottom right
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row+1, col+1], this.board[row+2, col+2], this.board[row+3, col+3]))
                        return playerToCheck;
                }
            }
            //Top-right to Bottom left
            for (int row = 0; row < this.board.GetLength(0)-3; row++)
            {
                for (int col = 3; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col - 1], this.board[row + 2, col - 2], this.board[row + 3, col - 3]))
                        return playerToCheck;
                }
            }
            return -1;
        }
        //Helper method for checking the player has a win condition, specifically if enough pieces are on the board and the pieces are in the right position
        private bool AllNumbersEqual(int toCheck, params int[] numbers)
        {
            foreach(int num in numbers)
            {
                if (num != toCheck)
                    return false;
            }
            return true;
        }
        //Sends back the column that we are placing the piece into 0 on far left 6 on far right for our width of 7
        private int ColumnNumber(Point mouse)
        {
            for(int i=0; i< this.boardColumns.Length; i++)
            {
                if((mouse.X >= this.boardColumns[i].X)&&(mouse.Y >= this.boardColumns[i].Y))
                {
                    if((mouse.X<= this.boardColumns[i].X + this.boardColumns[i].Width) && (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
                    {
                        //MessageBox.Show("" + i);
                        return i;
                    }
                }
            }
            return -1;
        }
        //places piece at lowest spot, goes from 5 to 0 to make 6 our depth
        private int EmptyRow(int col)
        {
            for(int i=5; i>=0;i--)
            {
                if (this.board[i, col] == 0)
                    return i;
            }
            return -1;
        }
        private void PlayerButton_Click(object sender, EventArgs e)
        {
            this.turn = 1;
        }
        private void PCButton_Click(object sender, EventArgs e)
        {
            this.turn = 2;
        }
        
    }
}
