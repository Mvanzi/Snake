public class MenuState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IGameContext _modelo;
    private readonly IGameView _vistaJuego;
    private readonly IUIView _vistaUI;

    public MenuState(SnakePresenter presenter, IGameContext modelo, IGameView vistaJuego, IUIView vistaUI)
    {
        _presenter = presenter;
        _modelo = modelo;
        _vistaJuego = vistaJuego;
        _vistaUI = vistaUI;
    }

    public void Entrar()
    {
        _vistaUI.ShowMenu();
        _vistaUI.HidePause();
        _vistaUI.HideGameOver();

        // Escucha el clic en el botón "Start" de la UI
        _vistaUI.OnStartClicked += IniciarPartida;
    }

    public void ProcesarTick() { } // En el menú no corre el juego

    public void ProcesarDireccion(KeyDirection direccion)
    {
        // Opcional: si presionan cualquier flecha/WASD en el menú, también arranca a jugar
        IniciarPartida();
    }

    public void ProcesarPausa() { } // No hay pausa en el menú principal

    public void Salir()
    {
        _vistaUI.OnStartClicked -= IniciarPartida;
        _vistaUI.HideMenu();
    }

    private void IniciarPartida()
    {
        // 1. Inicializa el tablero, paredes, serpiente y fruta en el modelo
        _modelo.IniciarJuego();

        // 2. Renderiza las paredes y la posición inicial
        _vistaJuego.RenderWalls(_modelo.ObtenerPosicionesParedes());
        _vistaJuego.RenderSnake(_modelo.ObtenerPosicionesSerpiente());
        _vistaJuego.RenderFruit(_modelo.ObtenerPosicionFruta());

        // 3. Transiciona al estado de juego activo
        _presenter.CambiarEstado(new PlayingState(_presenter, _modelo, _vistaJuego, _vistaUI));
    }
}