namespace Exam
{
    public interface ICreature
    {
        Resulter Act(INavigator navigator, Point objectPosition, Movement move);
    }
}