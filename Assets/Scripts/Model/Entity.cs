public abstract class Entity
{
    public abstract void OnCollide(IGameContext context);
    public virtual bool IsEmpty() => false;
}
