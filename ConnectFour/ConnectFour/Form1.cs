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
                        g.FillEllipse(Brushes.Orchid, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                    }
                    if (this.turn == 2)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.HotPink, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                    }
                }
            }
            if (this.turn == 1)
                turn = 2;
            else
                turn = 1;
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
    }
}
