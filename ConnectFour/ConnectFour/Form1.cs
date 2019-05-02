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
        private int rowIndex;
        public int height = 6;
        public int width = 7;
        public int[,] board = new int[6, 7];
        private long timeallotted = 4;
        private int turn;
        private int turnCounter = 0;
        private Boolean next;
        private Boolean gameStart = false;
        int winner = -1;

        public ConnectFour()
        {
            InitializeComponent();
            this.boardColumns = new Rectangle[width];
            this.board = new int[height, width];
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
            return column >= 0 && column < width && board[0, column] == 0;
        }
        public int place(int column)
        {
            //Console.WriteLine(next);
            int disk = (next == true) ? 1 : 2;
            if (!canPlace(column))
                return 0;
            int diskHeight = height - 1;
            while (board[diskHeight, column] != 0)
                diskHeight--;
            board[diskHeight, column] = disk;

            next = !next;
            
            return 1;
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
            else if (WinnerPlayer(2) == 2)
            {
                return 2;
            }
            else if (isFull())
            {
                return 3; 
            }
            else
            {
                return 0;
            }
        }
        private void ConnectFour_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.LimeGreen, 24, 24, 340, 300);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
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
            board = (int[,])contents.Clone();
        }
        public bool isFull()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == 0)
                        return false;
                }
            }
            return true;
        }
        private void ConnectFour_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameStart)
            {
                int colIndex = this.ColumnNumber(e.Location);
                this.turn = 1;
                //Console.WriteLine(e.Location); //Debugging
                if (colIndex != -1)
                {
                    rowIndex = this.EmptyRow(colIndex);
                    if (rowIndex != -1)
                    {
                        //These fill the board with the right color
                        this.board[rowIndex, colIndex] = this.turn;
                        if (this.turn == 1)
                        {
                            Graphics g = this.CreateGraphics();
                            g.FillEllipse(Brushes.SlateGray, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                            Console.WriteLine("Player" + colIndex + ", " + rowIndex);
                            //turnDisplay.Text = "Computer's Turn next";
                            turnCounter++;
                            winner = WinnerPlayer(this.turn);
                        }
                        if (winner != -1)
                        {
                            string winsy = (winner == 1) ? "Player" : "Computer";
                            MessageBox.Show(winsy + " wins");
                            Application.Restart();
                        }
                        computerTurn(2);
                    }
                }
                if (winner != -1)
                {
                    string winsy = (winner == 1) ? "Player" : "Computer";
                    MessageBox.Show(winsy + " wins");
                    Application.Restart();
                }
                if (gameStart)
                {
                    PlayerButton.Enabled = false;
                    PCButton.Enabled = false;
                }
            }
            else
                MessageBox.Show("Please press play when you are ready to begin");
        }
        //Series of checks to see if a win state is found, this could probably be refactored into a better method but we can do that later
        private int WinnerPlayer(int playerToCheck)
        {
            //Vertical Checks
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col], this.board[row + 2, col], this.board[row + 3, col]))
                        return playerToCheck;
                }
            }
            //Horizontal Checks
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row, col + 1], this.board[row, col + 2], this.board[row, col + 3]))
                        return playerToCheck;
                }
            }
            //Top-left to Bottom right
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.AllNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col + 1], this.board[row + 2, col + 2], this.board[row + 3, col + 3]))
                        return playerToCheck;
                }
            }
            //Top-right to Bottom left
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
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
            foreach (int num in numbers)
            {
                if (num != toCheck)
                    return false;
            }
            return true;
        }
        //Sends back the column that we are placing the piece into 0 on far left 6 on far right for our width of 7
        private int ColumnNumber(Point mouse)
        {
            for (int i = 0; i < this.boardColumns.Length; i++)
            {
                if ((mouse.X >= this.boardColumns[i].X) && (mouse.Y >= this.boardColumns[i].Y))
                {
                    if ((mouse.X <= this.boardColumns[i].X + this.boardColumns[i].Width) && (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
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
            for (int i = 5; i >= 0; i--)
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
        private void playStart_Click(object sender, EventArgs e)
        {
            gameStart = true;
            PlayerButton.Enabled = false;
            PCButton.Enabled = false;
        }
        private void computerTurn(int whosTurn)
        {
            this.turn = whosTurn;
            ConnectFour connectboard = new ConnectFour(board, next);
            MCTS ai = new MCTS(connectboard, timeallotted);
            int colIndex = 0;  
            colIndex = ai.getOptimalMove();
            rowIndex = this.EmptyRow(colIndex);
            if (rowIndex != -1)
            {
                this.board[rowIndex, colIndex] = this.turn;
                Graphics g = this.CreateGraphics();
                g.FillEllipse(Brushes.HotPink, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                //turnDisplay.Text = "Player's Turn next";
            }
            else
            {
                while (rowIndex == -1)
                {
                    colIndex = ai.getOptimalMove();
                    ai.update(colIndex);
                    rowIndex = this.EmptyRow(colIndex);
                    if (rowIndex != -1)
                    {
                        this.board[rowIndex, colIndex] = this.turn;
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.HotPink, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        //turnDisplay.Text = "Player's Turn next";
                    }
                }
            }
            ai.update(colIndex);
            turnCounter++;
            winner = WinnerPlayer(this.turn);
        }

        private void restartGame_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void easyButt_Click(object sender, EventArgs e)
        {
            this.timeallotted = 2;
            turnDisplay.Text = "Easy Mode";
        }

        private void medButt_Click(object sender, EventArgs e)
        {
            this.timeallotted = 4;
            turnDisplay.Text = "Medium Mode";
        }

        private void hardButt_Click(object sender, EventArgs e)
        {
            this.timeallotted = 6;
            turnDisplay.Text = "Hard Mode";
        }


        public String toString()
        {
            String result = "|-";
            for (int j = 0; j < width; j++)
            {
                result += "--|-";
            }
            result = result.Substring(0, result.Length - 1) + "\n";
            for (int i = 0; i < height; i++)
            {
                result += "| ";
                for (int j = 0; j < width; j++)
                {
                    result += (board[i,j] == 0 ? " " : (board[i,j] == 1 ? "P" : "A")) + " | ";
                }
                result = result.Substring(0, result.Length - 1);
                result += "\n|-";
                for (int j = 0; j < width; j++)
                {
                    result += "--|-";
                }
                result = result.Substring(0, result.Length - 1);
                result += "\n";
            }
            result += "  0   1   2   3   4   5   6  ";
            return result.Substring(0, result.Length - 1);
        }
    }

}
