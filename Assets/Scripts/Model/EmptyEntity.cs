public class EmptyEntity : Entity
{
    public override void OnCollide(IGameContext context) { }
    public override bool IsEmpty() => true;
}