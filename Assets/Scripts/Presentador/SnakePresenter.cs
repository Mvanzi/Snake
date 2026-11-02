public class SnakePresenter
{
    private const float VELOCIDAD_NORMAL = 0.2f;
    private const float VELOCIDAD_BOOST = 0.08f;

    private readonly IGameContext _modelo;
    private readonly IGameView _vistaJuego;
    private readonly IUIView _vistaUI;
    private readonly IInputKey _inputKey;
    private readonly ITicks _ticks;
    private readonly ISaveRepository _saveRepository;

    private IGameState _estadoActual;

    public IGameState EstadoActual => _estadoActual;
    public ISaveRepository SaveRepository => _saveRepository;

    public SnakePresenter(IGameContext modelo, IGameView vistaJuego, IUIView vistaUI, IInputKey inputKey, ITicks ticks, ISaveRepository saveRepository)
    {
        _modelo = modelo;
        _vistaJuego = vistaJuego;
        _vistaUI = vistaUI;
        _inputKey = inputKey;
        _ticks = ticks;
        _saveRepository = saveRepository;

        _estadoActual = new NullState();

        SuscribirEventos();
    }

    public SnakePresenter(IGameContext modelo, IGameView vistaJuego, IUIView vistaUI, IInputKey inputKey, ITicks ticks)
        : this(modelo, vistaJuego, vistaUI, inputKey, ticks, null)
    {
    }

    public void Iniciar()
    {
        CambiarEstado(new MenuState(this, _modelo, _vistaJuego, _vistaUI));
    }

    public void CambiarEstado(IGameState nuevoEstado)
    {
        _estadoActual.Salir();
        _estadoActual = nuevoEstado ?? new NullState();
        _estadoActual.Entrar();
    }

    private void SuscribirEventos()
    {
        if (_ticks != null)
        {
            _ticks.OnTick += EnTick;
        }

        if (_inputKey != null)
        {
            _inputKey.OnDirectionPressed += EnDireccionPresionada;
            _inputKey.OnPausePressed += EnPausaPresionada;
            _inputKey.OnSpeedBoostChanged += EnSpeedBoostCambiado;
        }
    }

    public void DesuscribirEventos()
    {
        if (_ticks != null)
        {
            _ticks.OnTick -= EnTick;
        }

        if (_inputKey != null)
        {
            _inputKey.OnDirectionPressed -= EnDireccionPresionada;
            _inputKey.OnPausePressed -= EnPausaPresionada;
            _inputKey.OnSpeedBoostChanged -= EnSpeedBoostCambiado;
        }

        _estadoActual.Salir();
        _estadoActual = new NullState();
    }

    private void EnTick()
    {
        _estadoActual.ProcesarTick();
    }

    private void EnDireccionPresionada(KeyDirection direccion)
    {
        _estadoActual.ProcesarDireccion(direccion);
    }

    private void EnPausaPresionada()
    {
        _estadoActual.ProcesarPausa();
    }

    private void EnSpeedBoostCambiado(bool boostActivado)
    {
        if (_ticks != null)
        {
            _ticks.SetSpeed(boostActivado ? VELOCIDAD_BOOST : VELOCIDAD_NORMAL);
        }
    }
}
