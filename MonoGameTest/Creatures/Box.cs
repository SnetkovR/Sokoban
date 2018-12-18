namespace Exam
{
    class Box : ICreature
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

            if (creature is Empty)
            {
                return new Resulter(position, objectPosition, true);

            }

            return new Resulter(objectPosition, false);
        }
    }
}
