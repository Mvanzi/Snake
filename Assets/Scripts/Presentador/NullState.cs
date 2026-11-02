public class NullState : IGameState
{
    public void Entrar() { }
    public void ProcesarTick() { }
    public void ProcesarDireccion(KeyDirection direccion) { }
    public void ProcesarPausa() { }
    public void Salir() { }
}

