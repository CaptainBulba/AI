public class SedanEscortedState : State
{
    Sedan sedan;

    public SedanEscortedState(Sedan sedan)
    {
        this.sedan = sedan;
    }

    public override State Execute()
    {
        if (sedan.TransportToPrison(sedan))
        {
            return new SedanJailedState(sedan);
        }
        else
            return this;
    }
}
