
public class WallEntity : Entity
{
    public override void OnCollide(IGameContext context)
    {
        context.EndGame();
    }
}
