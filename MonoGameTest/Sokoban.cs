using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class Sokoban : INavigator
    {
        private Cell[,] map;

        public Cell[,] Map => map;

        public int MapWidth => map.GetLength(0);

        public int MapHeight => map.GetLength(1);

        private List<Point> targetCells;

        private Point playerPosition;

        public delegate void MakeTurnEventHandler();

        public event MakeTurnEventHandler OnTurn;

        // TODO event reverse + cvs + leveling

        public bool IsOver
        {
            get
            {
                foreach (var cell in targetCells)
                {
                    if (!(map[cell.X, cell.Y].OnCell is Box))
                        return false;
                }

                return true;
            }
        }

        private void FindGameObjects()
        {
            int boxCounter = 0;

            for (var i = 0; i < MapWidth; i++)
            {
                for (var j = 0; j < MapHeight; j++)
                {
                    var cell = map[i, j];

                    switch (cell.OnCell)
                    {
                        case Player player:
                            playerPosition = new Point(i, j);
                            break;
                        case Box box:
                            boxCounter++;
                            break;
                    }

                    if (cell.IsTarget)
                    {
                        targetCells.Add(new Point(i, j));
                    }
                }
            }

            if (boxCounter != targetCells.Count)
            {
                throw new Exception("Wrong game map!");
            }
        }

        public Sokoban(Cell[,] map)
        {
            this.map = map;
            this.targetCells = new List<Point>();
            FindGameObjects();
        }

        private void MoveObject(Point oldPosition, Point newPosition)
        {
            var obj = map[oldPosition.X, oldPosition.Y].OnCell;
            map[oldPosition.X, oldPosition.Y].OnCell = new Empty();
            map[newPosition.X, newPosition.Y].OnCell = obj;
        }

        public Resulter MakeTurn(Movement move)
        {
            var resulter = map[playerPosition.X, playerPosition.Y].OnCell.Act(this, playerPosition, move);

            if (resulter.IsSuccess)
            {
                if (resulter.InnerRequest != null)
                {
                    MoveObject(resulter.InnerRequest.OldPosition, 
                        resulter.InnerRequest.NewPosition);
                }

                this.playerPosition = resulter.NewPosition;
                MoveObject(resulter.OldPosition, resulter.NewPosition);
                OnTurn?.Invoke();
            }

            return resulter;
        }

        public ICreature GetInfoAboutCell(Point point)
        {
            return map[point.X, point.Y].OnCell;
        }
    }
}
