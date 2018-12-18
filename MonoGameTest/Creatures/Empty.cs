namespace Exam
{
    public class Empty : ICreature
    {
        public Resulter Act(INavigator navigator, Point objectPosition, Movement move)
        {
            return new Resulter(objectPosition, false);
        }
    }
}