public class SedanRunningState : State
{
    Sedan sedan;

    public SedanRunningState(Sedan sedan)
    {
        this.sedan = sedan;
    }

    public override State Execute()
    {
        if (sedan.Captured())
            return new SedanEscortedState(sedan);
        else
        {
            sedan.Move();
            return this;
        }
    }
}
