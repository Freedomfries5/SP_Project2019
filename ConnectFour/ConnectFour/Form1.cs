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
        public int height = 6;
        public int width = 7;
        private int turn;
        private int turnCounter = 0;
        private Boolean next;



        public ConnectFour()
        {
            InitializeComponent();
            this.boardColumns = new Rectangle[7];
            this.board = new int[6, 7];
            //MessageBox.Show(this.board[0, 0].ToString());
            this.turn = 1;
        }
        public ConnectFour(int height, int width)
        {
            this.width = width;
            this.height = height;
            board = new int[height, width];
        }
        public ConnectFour(int[,] content, Boolean next)
        {
            loadContents(content);
            this.next = next;
        }
        public ConnectFour getNextState(int column)
        {
            ConnectFour next = this.copy();
            next.place(column);
            return next;
        }

        public Boolean canPlace(int column)
        {
            return column >= 0 && column < width && board[0,column] == 0;
        }
        public Boolean place(int column)
        {
            int disk = (next == true) ? 1 : 2;
            if (!canPlace(column))
                return false;
            int diskHeight = height - 1;
            while (board[diskHeight,column] != 0)
                diskHeight--;
            board[diskHeight,column] = disk;
            next = !next;
            return true;
        }

        public Boolean getNextTurn()
        {
            return next;
        }

        public int currentGameState()
        {
            if (WinnerPlayer(1) == 1)
            {
                return 1;
            }
            if (WinnerPlayer(2) == 2)
            {
                return 2;
            }
            if()
            return this.WinnerPlayer(PLAYER_1_DISK) ? PLAYER_1_WON
              : this.didPlayerWin(PLAYER_2_DISK) ? PLAYER_2_WON
              : this.isFull() ? TIE
              : ONGOING;
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
        public void loadContents(int[,] contents)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    board[i,j] = contents[i,j];
        }
        public bool isFull()
        {
            for (int i = 0; i < board.Length; i++)
                for (int j = 0; j < board.GetLength(i); j++)
                    if (board[i,j] == 0)
                        return false;
            return true;
        }

        private void ConnectFour_MouseClick(object sender, MouseEventArgs e)
        {
            int colIndex = this.ColumnNumber(e.Location);
            Console.WriteLine(e.Location);
            if(colIndex != -1)
            {
                int rowIndex = this.EmptyRow(colIndex);
                if (rowIndex != -1)
                {
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
        private bool AllNumbersEqual(int toCheck, params int[] numbers)
        {
            foreach(int num in numbers)
            {
                if (num != toCheck)
                    return false;
            }
            return true;
        }
        private int ColumnNumber(Point mouse)
        {
            for(int i=0; i< this.boardColumns.Length; i++)
            {
                if((mouse.X >= this.boardColumns[i].X)&&(mouse.Y >= this.boardColumns[i].Y))
                {
                    if((mouse.X<= this.boardColumns[i].X + this.boardColumns[i].Width) && (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
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
        public ConnectFour copy()
        {
            return new ConnectFour(board, this.next);
        }
    }
}
