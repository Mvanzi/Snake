public interface IGameState
{
    void Entrar();
    void ProcesarTick();
    void ProcesarDireccion(KeyDirection direccion);
    void ProcesarPausa();
    void Salir();
}