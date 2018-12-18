using System;

namespace Exam
{
    public class Player : ICreature
    {
        public Resulter Act(INavigator navigator, Point objectPosition, Movement move)
        {
            var position = new Point(objectPosition.X, objectPosition.Y);

            switch (move)
            {
                case Movement.Down:
                    position.X += 1;
                    break;

                case Movement.Left:
                    position.Y -= 1;
                    break;

                case Movement.Right:
                    position.Y += 1;
                    break;

                case Movement.Up:
                    position.X -= 1;
                    break;
            }

            var creature = navigator.GetInfoAboutCell(position);

            switch (creature)
            {
                case Box box:
                {
                    var resulter = box.Act(navigator, position, move);
                    if (resulter.IsSuccess)
                    {
                        return new Resulter(position, objectPosition, true, resulter);
                    }
                    return new Resulter(objectPosition, false);
                }
                case Empty empty:
                    return new Resulter(position, objectPosition, true);

                case Wall wall:
                    return new Resulter(objectPosition, false);

                default:
                    throw new Exception("А как ты дошел сюда?");
            }
        }
    }
}
