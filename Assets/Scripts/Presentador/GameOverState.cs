public class GameOverState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IGameContext _modelo;
    private readonly IGameView _vistaJuego;
    private readonly IUIView _vistaUI;

    public GameOverState(SnakePresenter presenter, IGameContext modelo, IGameView vistaJuego, IUIView vistaUI)
    {
        _presenter = presenter;
        _modelo = modelo;
        _vistaJuego = vistaJuego;
        _vistaUI = vistaUI;
    }

    public GameOverState(SnakePresenter presenter, IUIView vistaUI)
        : this(presenter, null, null, vistaUI)
    {
    }

    public void Entrar()
    {
        if (_presenter.SaveRepository != null)
        {
            _presenter.SaveRepository.BorrarPartida();
        }

        _vistaUI.ShowGameOver();
        _vistaUI.OnRestartClicked += ReiniciarPartida;
    }

    public void ProcesarTick() { }

    public void ProcesarDireccion(KeyDirection direccion)
    {
        ReiniciarPartida();
    }

    public void ProcesarPausa() { }

    public void Salir()
    {
        _vistaUI.OnRestartClicked -= ReiniciarPartida;
        _vistaUI.HideGameOver();
    }

    private void ReiniciarPartida()
    {
        if (_vistaJuego != null && _modelo != null)
        {
            _vistaJuego.ClearAll();
            _presenter.CambiarEstado(new MenuState(_presenter, _modelo, _vistaJuego, _vistaUI));
        }
    }
}