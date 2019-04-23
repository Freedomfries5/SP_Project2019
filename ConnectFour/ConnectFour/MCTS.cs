using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class MCTS
    {
        private Node root;
        private int width;
        private static double Formula_Parameter = Math.Sqrt(2);
        private long timeallotted;

        public MCTS(ConnectFour board, long timealotted)
        {
            this.timeallotted = timeallotted;
            root = new Node(null, board);
        }
    }
    class Node
    {
        private Node parent;
        private Node[] children;
        private int visit;
        private double p1Win;
        private ConnectFour board;

        public Node(Node parent, ConnectFour board)
        {
            this.parent = parent;
            this.board = board;
            this.visit = 0;
            this.p1Win = 0;
            children = new Node[7];
        }

        public int incrementVisits()
        {
            return ++visit;
        }

        public double incrementPlayerWins(double result)
        {
            p1Win += result;
            return p1Win;
        }
    }
}
