namespace Exam
{
    public class Wall : ICreature
    {
        public Resulter Act(INavigator navigator, Point objectPosition, Movement move)
        {
            return new Resulter(objectPosition, false);
        }
    }
}