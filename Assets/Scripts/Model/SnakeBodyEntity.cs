public class SnakeBodyEntity : Entity
{
    public override void OnCollide(IGameContext context)
    {
        context.EndGame();
    }
}
