public class PoliceEscortingState : State
{
    private Sedan sedan = null;
    private Police police;

    public PoliceEscortingState(Police police, Sedan sedan)
    {
        this.police = police;
        this.sedan = sedan;
    }

    public override State Execute()
    {
        if (police.TransportToPrison(sedan))
        { 
            return new PoliceIdleState(police);
        }
        else
            return this;
    }
}
