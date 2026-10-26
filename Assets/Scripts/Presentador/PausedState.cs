public class PausedState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IGameState _estadoAnterior;
    private readonly IUIView _vistaUI;

    public PausedState(SnakePresenter presenter, IGameState estadoAnterior, IUIView vistaUI)
    {
        _presenter = presenter;
        _estadoAnterior = estadoAnterior;
        _vistaUI = vistaUI;
    }

    public void Entrar()
    {
        _vistaUI.ShowPause();
    }

    public void ProcesarTick() { } // Ignora el paso del tiempo

    public void ProcesarDireccion(KeyDirection direccion) { } // No gira en pausa

    public void ProcesarPausa()
    {
        // Al volver a presionar pausa, vuelve al estado anterior
        _presenter.CambiarEstado(_estadoAnterior);
    }

    public void Salir()
    {
        _vistaUI.HidePause();
    }
}