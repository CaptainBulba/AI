public class SedanJailedState : State
{
    Sedan sedan;

    public SedanJailedState(Sedan sedan)
    {
        this.sedan = sedan;
    }

    public override State Execute()
    {
        sedan.HideCar();
        return this;
    }
}
