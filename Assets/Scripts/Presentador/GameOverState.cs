public class GameOverState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IUIView _vistaUI;

    public GameOverState(SnakePresenter presenter, IUIView vistaUI)
    {
        _presenter = presenter;
        _vistaUI = vistaUI;
    }

    public void Entrar()
    {
        _vistaUI.ShowGameOver();
    }

    public void ProcesarTick() { }

    public void ProcesarDireccion(KeyDirection direccion) { }

    public void ProcesarPausa() { }

    public void Salir()
    {
        _vistaUI.HideGameOver();
    }
}