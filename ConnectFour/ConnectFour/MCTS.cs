﻿using System;
using System.Diagnostics;
using System.Threading;
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
        public long timeallotted;

        public MCTS(ConnectFour board, long timeallotted)
        {
            this.width = board.width;
            this.timeallotted = timeallotted;
            root = new Node(null, board);
        }

        public void update(int move)
        {
            root = root.children[move] != null
                    ? root.children[move]
                    : new Node(null, root.board.getNextState(move));
        }

        public int getOptimalMove()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(stopwatch.Elapsed <= TimeSpan.FromSeconds(timeallotted))
            {
                Node selectedNode = select();
                if (selectedNode == null)
                    continue;
                Node expandedNode = expand(selectedNode);
                double result = simulate(expandedNode);
                backpropagate(expandedNode, result);
            }
            stopwatch.Stop();
            int maxIndex = -1;
            for (int i = 0; i < width; i++)
            {
                if (root.children[i] != null)
                {
                    if (maxIndex == -1 || root.children[i].visit > root.children[maxIndex].visit)
                    {
                        maxIndex = i;
                    }
                }
            }
            return maxIndex;
        }

        private Node select()
        {
            return select(root);
        }

        private Node select(Node parent)
        {
            // if parent has at least child without statistics, select parent
            for (int i = 0; i < width; i++)
            {
                if (parent.children[i] == null && parent.board.canPlace(i))
                {
                    return parent;
                }
            }
            // if all children have statistics, use UCT to select next node to visit
            double maxSelectionVal = -1;
            int maxIndex = -1;
            for (int i = 0; i < width; i++)
            {
                if (!parent.board.canPlace(i))
                    continue;
                Node currentChild = parent.children[i];
                double wins = (parent.board.getNextTurn() == true)
                  ? currentChild.p1Win
                  : (currentChild.visit - currentChild.p1Win);
                double selectionVal = wins / currentChild.visit
                  + Formula_Parameter * Math.Sqrt(Math.Log(parent.visit) / currentChild.visit);// UCT
                if (selectionVal > maxSelectionVal)
                {
                    maxSelectionVal = selectionVal;
                    maxIndex = i;
                }
            }
            if (maxIndex == -1)
                return null;
            return select(parent.children[maxIndex]);
        }

        private Node expand(Node selectedNode)
        {
            // get unvisited child nodes
            List<int> unvisitedChildrenIndices = new List<int>(width);
            for (int i = 0; i < width; i++)
            {
                if (selectedNode.children[i] == null && selectedNode.board.canPlace(i))
                {
                    unvisitedChildrenIndices.Add(i);
                }
            }
            Random rnd = new Random();
            // randomly select unvisited child and create node for it
            int selectedIndex = unvisitedChildrenIndices[(rnd.Next(0, unvisitedChildrenIndices.Count))];
            selectedNode.children[selectedIndex] = new Node(selectedNode, selectedNode.board.getNextState(selectedIndex));
            return selectedNode.children[selectedIndex];
        }

        private double simulate(Node expandedNode)
        {
            Random rand = new Random();
            ConnectFour simulationBoard = expandedNode.board.copy();
            while (simulationBoard.currentGameState() == 0)
            {
                simulationBoard.place((rand.Next(0,width)));
            }
            switch (simulationBoard.currentGameState())
            {
                case 1:
                    return 1;
                case 2:
                    return 0;
                default:
                    return 0.5;
            }
            
        }

        private void backpropagate(Node expandedNode, double simulationResult)
        {
            Node currentNode = expandedNode;
            while (currentNode != null)
            {
                currentNode.incrementVisits();
                currentNode.incrementPlayerWins(simulationResult);
                currentNode = currentNode.parent;
            }
        }

    }
    class Node
    {
        public Node parent;
        public Node[] children;
        public int visit;
        public double p1Win;
        public ConnectFour board;

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