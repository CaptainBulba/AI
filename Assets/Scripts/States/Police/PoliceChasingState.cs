public class PoliceChasingState : State
{
    private Sedan sedan = null;
    private Police police;

    public PoliceChasingState(Police police, Sedan sedan)
    {
        this.police = police;
        this.sedan = sedan;
    }

    public override State Execute()
    {
        if (police.Chase(sedan))
        {
            return new PoliceEscortingState(police, sedan);
        }
        else
            return this;
    }
}
