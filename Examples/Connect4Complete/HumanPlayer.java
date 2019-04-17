import java.io.*;

/**
   HumanPlayer.java
  
   This class contains the code for a human player to play Connect 4
*/
public class HumanPlayer implements Player
{

    public static final char HUMAN_CHARACTER = 'O';

    /** This method gets a move from the user and makes the play on the board */
    public Position move (Position pos) throws Exception
    {
        String line;
        int col;
        Position tempPos;
        col = 0;

        BufferedReader bufReader = new BufferedReader(new InputStreamReader(System.in));
       
        System.out.print("Enter the column in which to play (0-6) -->");
        line = bufReader.readLine();
        try 
        {
            col = Integer.parseInt(line);
        }
        catch (NumberFormatException e)
        {
	    col = -1;
	}
        while ((col <0) || col>6 || !pos.legalMove(col) )
        {
            System.out.print("Illegal move!" + '\n' + "Enter the column in which to play (0-6) -->");
            line = bufReader.readLine();
            try 
            {
                col = Integer.parseInt(line);
            }
            catch (NumberFormatException e)
            {
	        col = -1;
	    }           
        }

        return pos.makeMove(col, HUMAN_CHARACTER);
    }
}
