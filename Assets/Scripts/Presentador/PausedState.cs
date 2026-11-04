using System.Collections.Generic;

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
                MapearPosiciones(_modelo.ObtenerPosicionesParedes()),
                MapearPosiciones(_modelo.ObtenerPosicionesSerpiente()),
                MapearPosicion(_modelo.ObtenerPosicionFruta()),
                MapearDireccionACadena(_modelo.SnakeDirection)
            );
            _presenter.SaveRepository.Guardar(datos);
            _vistaUI.MostrarMensajeGuardado();
        }
    }

    private List<PosicionTablero> MapearPosiciones(IReadOnlyList<Coordinate> coordenadas)
    {
        List<PosicionTablero> posiciones = new List<PosicionTablero>(coordenadas.Count);
        for (int i = 0; i < coordenadas.Count; i++)
        {
            posiciones.Add(new PosicionTablero(coordenadas[i].X, coordenadas[i].Z));
        }
        return posiciones;
    }

    private PosicionTablero MapearPosicion(Coordinate coordenada)
    {
        return new PosicionTablero(coordenada.X, coordenada.Z);
    }

    private string MapearDireccionACadena(IDirection direccion)
    {
        if (direccion is UpDirection) return "Up";
        if (direccion is DownDirection) return "Down";
        if (direccion is LeftDirection) return "Left";
        return "Right";
    }
}