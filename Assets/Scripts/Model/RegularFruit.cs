public class RegularFruit : FruitEntity
{
    protected override void ApplyFruitEffect(IGameContext context)
    {
        context.GrowSnake();
    }
}