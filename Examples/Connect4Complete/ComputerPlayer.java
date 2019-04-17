import java.util.*;

/** ComputerPlayer.java
   
    This class represents a computer player for the
    game of Connect 4
*/
public class ComputerPlayer implements Player
{

    public static final char COMPUTER_CHARACTER = 'X';   // the character used for the computer
    private int depth;  // The number of moves deep to search

    /** Construct a computer player that will search to depth plys deep */
    public ComputerPlayer (int depth)
    {
        if (depth>0) 
            this.depth = depth;
        else
            this.depth = 1;
    }

    /** Get the number of plys */
    public int getDepth()
    {
        return depth;
    }

    /** Set the number of plys */
    public void setDepth(int depth)
    {
        if (depth>0) this.depth = depth;
    }


    /** Make a move in the given position */ 
    public Position move (Position pos)
    {
	Position p = minimax (pos, depth, Integer.MIN_VALUE+1, Integer.MAX_VALUE, COMPUTER_CHARACTER);
        return p;
    } 

    /* This function generates the next set of possible moves
       and places them into the pQ linked list */
    private LinkedList generateNext(char xo, Position pp)
    {
        int j, i=3;
        LinkedList list = new LinkedList();
        for (j=3; j>0; --j)
        {
            if (pp.legalMove(i+j))
                list.add(pp.makeMove(i+j, xo));
            if (pp.legalMove(i-j)) 
                list.add(pp.makeMove(i-j, xo));
        }
        if (pp.legalMove(i))  list.add(pp.makeMove(i, xo));
        return list;
    }

    
    /** This function searches through potential positions to find the
        best next move. */
    private Position minimax (Position b, int depth, int alpha, int beta, char xo)
    {
	
       Position temppos;   // a temporary position holder
       Position nextpos;  // the next position to be looked at
       Position bestpos;   // the best next position found so far
       
       LinkedList pq = new LinkedList();           // the queue of next positions

       int temp;            // the score of the current move
       int max;             // the score of the current best move
       char ox;             // X of O depending on whose move it is

       temppos = null;
       nextpos = null;
       bestpos = null;


       if (xo == COMPUTER_CHARACTER) 
           ox = HumanPlayer.HUMAN_CHARACTER; 
       else 
           ox = COMPUTER_CHARACTER;

       max = Integer.MIN_VALUE;

       if ((depth == 0) || (b.gameOver()))
       {
           b.setScore(staticEval(xo, b, depth)); 
           return b;
       }
       else
       {
           pq = generateNext(xo, b);
           while (pq.size() > 0)
           {
               temppos = (Position)pq.removeFirst();
               nextpos = (Position)minimax(temppos, depth-1, -1*beta, -1*alpha, ox); 
      	       temp = -1 * nextpos.getScore();
              
	      
               if (temp > max)
               {
	           temppos.setScore(temp);
                   max = temp;
		   if (max > alpha)   alpha = max;
                   
                   bestpos=temppos;	

                   if (max > beta)    return bestpos;	  
               }

           }
           return bestpos;
        }
    }


    /** This method assigns a score to 4 elements, along a row, column or diagonal */
    private int quad (char xo, char w, char x, char y, char z) 
    {
        int xocount = 0;
        int oxcount = 0;

        if (w==xo) xocount++; else if (w!=Position.BLANK) oxcount++;
        if (x==xo) xocount++; else if (x!=Position.BLANK) oxcount++;
        if (y==xo) xocount++; else if (y!=Position.BLANK) oxcount++;
        if (z==xo) xocount++; else if (z!=Position.BLANK) oxcount++;
        if (xocount > 0 && oxcount > 0)
            return 0;
        else
	{
            return (xocount-oxcount);
	}
    }

    /** Evaluate the position from xo's point of view */
    private int staticEval (char xo, Position p, int depth){
        char ox;
        int i, j, total=0;
        if (xo == COMPUTER_CHARACTER) 
            ox = HumanPlayer.HUMAN_CHARACTER; 
        else 
            ox = COMPUTER_CHARACTER;
        if(p.gameOver())
        {
            char go = p.getWinner();
            if (go == xo)
            {
                p.setScore(5000000+depth);
                return p.getScore();
            }
            else if (go == ox)
            {
                p.setScore(-5000000-depth);
                return p.getScore();
            }
        }
  
        // Count some points for the rows
        for (i=0; i<6; ++i)
            for (j=0; j<4; ++j)
            {
                total = total + quad (xo, p.getBoardValue(i, j), p.getBoardValue(i, j+1),
                            p.getBoardValue(i, j+2), p.getBoardValue(i, j+3));
            }

        // Count some points for the columns
        for (j=0; j<7; ++j)
            for (i=0; i<3; ++i)
            {
                total = total + quad (xo, p.getBoardValue(i, j), p.getBoardValue(i+1, j),
                            p.getBoardValue(i+2, j), p.getBoardValue(i+3, j));
            }

      
        // count across the diagonals
        for (i=0; i<3; ++i)
            for (j=0; j<4; ++j)
            {
                total = total + quad (xo, p.getBoardValue(i, j), p.getBoardValue(i+1, j+1),
                            p.getBoardValue(i+2, j+2), p.getBoardValue(i+3, j+3));
            }

        // count across the other diagonals
        for (i=3; i<6; ++i)
            for (j=0; j<4; ++j)
            {
                total = total + quad (xo, p.getBoardValue(i, j), p.getBoardValue(i-1, j+1),
                            p.getBoardValue(i-2, j+2), p.getBoardValue(i-3, j+3));
            }

        return total;
    }

}
