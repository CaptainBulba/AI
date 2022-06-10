public class SedanIdleState : State
{
    Sedan sedan;

    public SedanIdleState(Sedan sedan)
    {
        this.sedan = sedan;
    }

    public override State Execute()
    {
        if (sedan.Captured())
            return new SedanEscortedState(sedan);
        else
            return new SedanRunningState(sedan);

    }
}
