import java.io.*;
import javax.swing.*;
import javax.swing.plaf.*;
import java.awt.*;
import java.awt.event.*;
import java.util.*;

/**
   Connect4.java
 
   This file contains a program that plays the game Connect 4.
*/
public class Connect4 extends JFrame
{

    ComputerPlayer computer = new ComputerPlayer(5);  // the computer
    Position p = new Position();                      // the model of the board
    Board b = new Board();                            // the GUI of the board

    Stack stack = new Stack();                        // a stack of positions to implement undo

    JLabel messages = new JLabel("");                 // a place for messages

    JButton first = new JButton ("Computer Plays First"); // a button to let the computer go first

    boolean computerThinking = false;

    // the constructor wires everything together.
    public Connect4 ()
    {
        setSize(250, 350);
        setTitle("Connect 4");
     
        setJMenuBar(new Connect4MenuBar());

        Container c = getContentPane();
        c.setLayout(null);

        b.setLocation(15, 15);
        c.add(b);

        first.setLocation (15, 210);
        first.setSize(210, 25);
        first.addActionListener(new ComputerFirstHandler());
        c.add(first);

        messages.setLocation (15, 240);
        messages.setSize(210, 25);
        c.add(messages);

        
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        show();
    }       

    // Set thinking
    public synchronized void setThinking (boolean thinking)
    {
        computerThinking = thinking;
    }

    // Get thinking
    public synchronized boolean getThinking ()
    {
        return computerThinking;
    } 

    // A menu class just for the Connect4 game
    public class Connect4MenuBar extends JMenuBar
    {
        // Two menus
        JMenu level = new JMenu("Difficulty");
        JMenu file = new JMenu("Options");

        // A bunch of levels as menu items.
        JMenuItem level1 = new JMenuItem("1");
        JMenuItem level2 = new JMenuItem("2");
        JMenuItem level3 = new JMenuItem("3");
        JMenuItem level4 = new JMenuItem("4");
        JMenuItem level5 = new JMenuItem("5");
        JMenuItem level6 = new JMenuItem("6");
        JMenuItem level7 = new JMenuItem("7");
        JMenuItem level8 = new JMenuItem("8");

        JMenuItem reset = new JMenuItem("Reset");
        JMenuItem undo = new JMenuItem("Undo");
    
        // The constructor wires everything together
        public Connect4MenuBar()
        {
	    add(file);
            file.add(reset);
            reset.addActionListener (new ResetHandler());
            file.add(undo);
            undo.addActionListener (new UndoHandler());

            add(level);

            level1.addActionListener(new LevelHandler(1));
            level2.addActionListener(new LevelHandler(2));
            level3.addActionListener(new LevelHandler(3));
            level4.addActionListener(new LevelHandler(4));
            level5.addActionListener(new LevelHandler(5));
            level6.addActionListener(new LevelHandler(6));
            level7.addActionListener(new LevelHandler(7));
            level8.addActionListener(new LevelHandler(8));

            level.add(level1);
            level.add(level2);
            level.add(level3);
            level.add(level4);
            level.add(level5);
            level.add(level6);
            level.add(level7);
            level.add(level8);
        }
    }

    // This handler class sets the depth to which the computer searches
    public class LevelHandler implements ActionListener
    {
        int level;

        public LevelHandler (int level)
        {
            this.level = level;
        }

        public void actionPerformed (ActionEvent e)
        {
	    computer.setDepth(level);
        }
    }

    // Handle undo events
    public class UndoHandler implements ActionListener
    {
        public void actionPerformed (ActionEvent e)
        {
            if (p.gameOver() || getThinking()) return;
            if (!stack.empty()){
	        p = (Position) stack.pop();
                b.updateBoard();
                if (stack.empty()) first.setEnabled(true);
            }
            else {
                messages.setText("Cannot undo an empty board");
            }
	}
    }
    
    // Reset Handler
    public class ResetHandler implements ActionListener
    {
        public void actionPerformed(ActionEvent e)
        {
            p = new Position();
            b.resetBoard();
            first.setEnabled(true);
            stack = new Stack();
        }
    }

    // Computer First Handler
    public class ComputerFirstHandler implements ActionListener
    {
        public  void actionPerformed (ActionEvent e)
        {
            try{
                stack.push(p);
                p = computer.move(p);
                b.updateBoard();
                first.setEnabled(false);
            }
            catch (Exception ex) {
                messages.setText("Computer cannot go first!!!");
            }
	}
    }
            

    // We make the Board class an inner class, since it has handlers
    // that need access to the application data
    public class Board extends JPanel
    {
        // We use an array of circles to quickly get to the correct circle for 
        // changing colors
        Circle [][] circleArray = new Circle [6][7];
    
        // Fill the array of circles and add them to the JPanel
        public Board ()
        {
             // initialize the view portion of the class
             setLayout(null);
             setSize(211, 181);
             setBackground(Color.yellow);
             setBorder(BorderFactory.createLineBorder(Color.black));
             for (int i=0; i<6; ++i)
                 for (int j=0; j<7; ++j) {
		     circleArray[i][j] = new Circle(5-i, j, Color.white);
                     add(circleArray[i][j]);
                 }             
        }
 
        // Walks through the position, updating the board as needed
        public void updateBoard()
        {
            int row, col;
            for (row =0; row<6; row++)
                for (col=0; col<7; col++)
                    if (p.getBoardValue(row, col) == '_')
                        circleArray[row][col].setColor(Color.white);
                    else if (p.getBoardValue(row, col) == 'O')
                        circleArray[row][col].setColor(Color.red);
                    else
                        circleArray[row][col].setColor(Color.black);

            Vector winners = p.getWinningCells();
            for (int i=0; i<winners.size(); i++) {
                Point cell = (Point) winners.elementAt(i);

                // Note: reversing the X and Y is a kludge to keep from 
                // rehacking the Position code that creates points in
                // in the form (row, col).
                row = (int) cell.getX();      
                col = (int) cell.getY();
                circleArray[row][col].setWinningUI();
	    }
 
        }

        public void resetBoard()
        {
            int row, col;
            for (row =0; row<6; row++)
                for (col=0; col<7; col++) {
                    circleArray[row][col].setColor(Color.white);
                    circleArray[row][col].setNormalUI();
	        }
 
        }

        // The circle class is used to draw checkers on the board
        public class Circle extends JComponent {
            Color color;
            int row, col;
        
            // The constructor initializes the instance variables.
            public Circle (int row, int colArg, Color color)
            {
                this.row = row;
                this.col = colArg;
                this.color = color;
                setSize(30,30);
                setLocation(30*col, 30*row);
                setUI (new CircleUI());
	        addMouseListener (new MoveHandler());
            }	        
        
            // CircleUI paints circles
            public class CircleUI extends ComponentUI {
                public void paint (Graphics g, JComponent c) {
                    g.setColor(color);
                    g.fillOval(2, 2, 26, 26);
                    g.setColor(Color.black);
                    g.drawOval(2, 2, 26, 26);         
                }
            }

            // Winning CircleUI highlights the winning circles
            public class WinningCircleUI extends ComponentUI {
                public void paint (Graphics g, JComponent c) {
                    Graphics2D g2d = (Graphics2D) g;
                    g2d.setColor(color);
                    g2d.fillOval (2, 2, 26, 26);
                    g2d.setColor(Color.blue);
                    Stroke oldStroke = g2d.getStroke();
                    g2d.setStroke(new BasicStroke(3.0f));
                    g2d.drawOval (2, 2, 26, 26);
                    g2d.setStroke(oldStroke);
                }
            }

            // Since setUI is protected, we need a wrapper method.
            public void setWinningUI  ()
            {
                setUI(new WinningCircleUI());
            }    

            // Likewise we need a wrapper for resetting the UI
            public void setNormalUI ()
            {
                setUI(new CircleUI());
            }

            // set the color of this circle
            public void setColor (Color color)
            {
                this.color = color;
                repaint();
            }
    
            // Make a move for the user and for the computer
	    // when the user clicks on the board.
            private class MoveHandler extends MouseAdapter
            {

                class moveThread extends Thread
                {
                    public void run() {
                        p = computer.move(p);
                        updateBoard();
                        setThinking(false);
                    }
                }

                public void mouseClicked (MouseEvent e)
                {
                    if (p.gameOver() || getThinking()) return;
                    if (p.legalMove(col))
                    {
                        stack.push(p);
                        p = p.makeMove(col, 'O');
                        updateBoard();
                        //paintImmediately(0,0,211,181);
                        messages.setText("");
                        first.setEnabled(false);
 
                        setThinking(true);
                        Thread t = new moveThread();
                        t.start();
                    }
                    else {
                        messages.setText("Illegal move!");
                    }
                }
            }
        }  
    }


    /** The main program drives the game */
    public static void main (String [] args) throws Exception
    {
        Connect4 c4 = new Connect4();
    }
}
