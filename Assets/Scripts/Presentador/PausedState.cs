public class PausedState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IGameState _estadoAnterior;
    private readonly IGameContext _modelo;
    private readonly IUIView _vistaUI;

    public PausedState(SnakePresenter presenter, IGameState estadoAnterior, IGameContext modelo, IUIView vistaUI)
    {
        _presenter = presenter;
        _estadoAnterior = estadoAnterior;
        _modelo = modelo;
        _vistaUI = vistaUI;
    }

    public PausedState(SnakePresenter presenter, IGameState estadoAnterior, IUIView vistaUI)
        : this(presenter, estadoAnterior, null, vistaUI)
    {
    }

    public void Entrar()
    {
        _vistaUI.ShowPause();
        _vistaUI.OnResumeClicked += Despausar;
        _vistaUI.OnSaveClicked += GuardarPartida;
    }

    public void ProcesarTick() { }

    public void ProcesarDireccion(KeyDirection direccion) { }

    public void ProcesarPausa()
    {
        Despausar();
    }

    public void Salir()
    {
        _vistaUI.OnResumeClicked -= Despausar;
        _vistaUI.OnSaveClicked -= GuardarPartida;
        _vistaUI.HidePause();
    }

    private void Despausar()
    {
        _presenter.CambiarEstado(_estadoAnterior);
    }

    private void GuardarPartida()
    {
        if (_presenter.SaveRepository != null && _modelo != null)
        {
            DatosPartidaGuardada datos = new DatosPartidaGuardada(
                _modelo.ObtenerPosicionesParedes(),
                _modelo.ObtenerPosicionesSerpiente(),
                _modelo.ObtenerPosicionFruta(),
                _modelo.SnakeDirection
            );
            _presenter.SaveRepository.Guardar(datos);
            _vistaUI.MostrarMensajeGuardado();
        }
    }
}