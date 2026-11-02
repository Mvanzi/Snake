using System;
using System.Collections.Generic;

public class PlayingState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IGameContext _modelo;
    private readonly IGameView _vistaJuego;
    private readonly IUIView _vistaUI;
    private readonly Dictionary<KeyDirection, Action> _accionesDireccion;

    public PlayingState(SnakePresenter presenter, IGameContext modelo, IGameView vistaJuego, IUIView vistaUI)
    {
        _presenter = presenter;
        _modelo = modelo;
        _vistaJuego = vistaJuego;
        _vistaUI = vistaUI;

        _accionesDireccion = new Dictionary<KeyDirection, Action>()
        {
            { KeyDirection.Up, _modelo.TurnSnakeUp },
            { KeyDirection.Down, _modelo.TurnSnakeDown },
            { KeyDirection.Left, _modelo.TurnSnakeLeft },
            { KeyDirection.Right, _modelo.TurnSnakeRight }
        };
    }

    public void Entrar()
    {
        _vistaUI.HidePause();
        _vistaUI.HideGameOver();
    }

    public void ProcesarTick()
    {
        _modelo.AvanzarTurno();

        if (_modelo.EstaGameOver)
        {
            _presenter.CambiarEstado(new GameOverState(_presenter, _modelo, _vistaJuego, _vistaUI));
            return;
        }

        _vistaJuego.RenderSnake(MapearPosiciones(_modelo.ObtenerPosicionesSerpiente()));
        _vistaJuego.RenderFruit(MapearPosicion(_modelo.ObtenerPosicionFruta()));
    }

    public void ProcesarDireccion(KeyDirection direccion)
    {
        if (_accionesDireccion.TryGetValue(direccion, out Action accion))
        {
            accion.Invoke();
        }
    }

    public void ProcesarPausa()
    {
        _presenter.CambiarEstado(new PausedState(_presenter, this, _modelo, _vistaUI));
    }

    public void Salir() { }

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
}