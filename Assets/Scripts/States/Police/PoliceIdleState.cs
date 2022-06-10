public class PoliceIdleState : State
{
    Police police;

    public PoliceIdleState(Police police)
    {
        this.police = police;
    }

    public override State Execute()
    {
        if (police.FindClosestTarget())
        {
            return new PoliceChasingState(police, police.GetTarget().GetComponent<Sedan>());
        }
        else
        {
            police.Move();
            return this;
        } 
    }
}
