import java.util.*;
import java.awt.*;

/** Position.java

    This class represents a Connect 4 game position.  It contains
    the board and information about the current game position 
*/
public class Position
{
    public static final char BLANK='_';  // the character used for empty spaces on the board


    private char board[][];     // the game board
    private int score;          // the score calculated by the computer for the current position 

    private int computerCol;    // the last column that the computer played in 
    private int humanCol;       // the last column the user played in
    private boolean over;       // set to true when the game is over
    private char winner;        // HUMAN_CHARACTER, COMPUTER_CHARACTER or BLANK, depending on who has won

    private Vector winningCells = new Vector();   // a vector of the winning cells
  
    /** Construct an empty board */
    public Position ()
    {
	// initialize the model portion of the class
         int i,j;
         board = new char [6][7];
         for (i=0; i<6; ++i)
             for (j=0; j<7; ++j)
                 board[i][j] = '_';

         score = 0;
         computerCol  = -1;
         humanCol = -1;
         over = false;
         winner = BLANK;
    }
  
    /** Construct a copy of the source board */
    public Position (Position source)
    {
        int i, j;
        board = new char[6][7];
        for (i=0; i<6; i++)
            for (j=0; j<7; j++)
                board[i][j] = source.board[i][j];

        score = source.score;
        computerCol = source.computerCol;
        humanCol = source.humanCol;
        over = source.over;
        winner = source.winner;
    }

    /** Get the board's value at location (row, col) */
    public char getBoardValue(int row, int col)
    {
        return board[row][col];
    }

    /** Set the score */
    public void setScore(int score)
    {
        this.score = score;
    }

    /** get the Score */
    public int getScore() {return score;}

    /** Set the column where the computer played */
    public void setComputerCol(int col)
    {
        this.computerCol = col;
    }

    /** Get the column where the computer played */
    public int getComputerCol () { return computerCol;}

    /** Set the column where the human played */
    public void setHumanCol(int col)
    {
        this.humanCol = col;
    }

    /** Get the column where the human played */
    public int getHumanCol () { return humanCol;}

    /** Get the winner in this position */
    public char getWinner() { return winner;}

    // Get the cells that make 4 in a row
    public Vector getWinningCells() {return winningCells;}


    /* Places the xo character into col of the board */
    public Position makeMove (int col, char xo)
    {
        Position pos = new Position(this);
        int i;

        i=0;
        while (pos.board[i][col] != '_') i++;
        pos.board[i][col] = xo;

        if (xo == HumanPlayer.HUMAN_CHARACTER)
	    pos.humanCol = col;
        else
            pos.computerCol = col;

        pos.setGameOver();
        return pos;
    }


    /** Checks to make sure it is legal to play in column j */
    public boolean legalMove (int j)
    {
        return (board[5][j] == '_');
    }


    /** This method prints a position to the screen */
    public void print ()
    {
        int i, j;
        System.out.println();
        for (j=0; j<7; j++)
	    System.out.print (j + " ");
        System.out.println("\n-------------");

        for (i=5; i>=0; i--)
        {
	    for (j=0; j<7; j++)
		System.out.print(board[i][j] + " ");
	    System.out.println();
        }

        if (humanCol >= 0)
	    System.out.println("Your last move " + humanCol);
        if (computerCol >=0)
	    System.out.println("Computer's last move " + computerCol);
        System.out.println("Score for position is : " + score);
    }


    /** Checks to see if the game is over.
       It loops through the rows, the columns and the diagonals
       to see if anyone has won. */
    public boolean gameOver()
    {
        return over;
    }

    /** Set the game over flag for this positions */
    private void setGameOver()
    {
        int i, j;

        // Assume the game is over
        over = true;
 
        // If the board is full, the game is over
        for (j=0; j<7; ++j)
            if (board[5][j] == BLANK) over = false;

        // Check all the rows for four in a row
        for (i=0; i<6; ++i)
            for (j=0; j<4; ++j)
            {
                if (board[i][j] != BLANK &&
                    board[i][j] == board[i][j+1] &&
                    board[i][j] == board[i][j+2] &&
                    board[i][j] == board[i][j+3])
                {
	            over = true;
                    winner = board[i][j];
                    winningCells.add(new Point(i,j));
                    winningCells.add(new Point(i,j+1));
                    winningCells.add(new Point(i,j+2));
                    winningCells.add(new Point(i,j+3));
                }
            }
  
        // Check all the columns for 4 in a row
        for (j=0; j<7; ++j)
            for (i=0; i<3; ++i)
            {
	        if (board[i][j] != BLANK &&
                    board[i][j] == board[i+1][j] &&
                    board[i][j] == board[i+2][j] &&
                    board[i][j] == board[i+3][j])
                {
	            over = true;
                    winner = board[i][j];
                    winningCells.add(new Point(i,j));
                    winningCells.add(new Point(i+1,j));
                    winningCells.add(new Point(i+2,j));
                    winningCells.add(new Point(i+3,j));
                }
            }

        // Check one set of diagonals for 4 in a row
        for (i=0; i<3; ++i)
            for (j=0; j<4; ++j)
            {
                if (board[i][j] != BLANK &&
                    board[i][j] == board[i+1][j+1] &&
                    board[i][j] == board[i+2][j+2] &&
                    board[i][j] == board[i+3][j+3])
                {
	            over = true;
                    winner = board[i][j];
                    winningCells.add(new Point(i,j));
                    winningCells.add(new Point(i+1,j+1));
                    winningCells.add(new Point(i+2,j+2));
                    winningCells.add(new Point(i+3,j+3));
                }
            }

        // Check the other set of diagonals for 4 in a row
        for (i=5; i>2; --i)
            for (j=0; j<4; ++j)
            {
	        if (board[i][j] != BLANK &&
                    board[i][j] == board[i-1][j+1] &&
                    board[i][j] == board[i-2][j+2] &&
                    board[i][j] == board[i-3][j+3])
                {
                    over = true;
                    winner = board[i][j];
                    winningCells.add(new Point(i,j));
                    winningCells.add(new Point(i-1,j+1));
                    winningCells.add(new Point(i-2,j+2));
                    winningCells.add(new Point(i-3,j+3));
                }
            }
    }


}
