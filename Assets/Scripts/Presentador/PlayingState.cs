using System.Collections.Generic;
using UnityEditor;

public class PlayingState : IGameState
{
    private readonly SnakePresenter _presenter;
    private readonly IGameContext _modelo;
    private readonly IGameView _vistaJuego;
    private readonly IUIView _vistaUI;

    public PlayingState(SnakePresenter presenter, IGameContext modelo, IGameView vistaJuego, IUIView vistaUI)
    {
        _presenter = presenter;
        _modelo = modelo;
        _vistaJuego = vistaJuego;
        _vistaUI = vistaUI;
    }

    public void Entrar()
    {
        _vistaUI.HidePause();
        _vistaUI.HideGameOver();
    }

    public void ProcesarTick()
    {
        // 1. Avanza un paso en la simulación
        _modelo.AvanzarTurno();

        // 2. Consulta si hubo choque o fin de partida
        if (_modelo.EstaGameOver)
        {
            _presenter.CambiarEstado(new GameOverState(_presenter, _vistaUI));
            return;
        }

        // 3. Pasa los datos a la vista para renderizar
        _vistaJuego.RenderSnake(_modelo.ObtenerPosicionesSerpiente());
        _vistaJuego.RenderFruit(_modelo.ObtenerPosicionFruta());
    }

    public void ProcesarDireccion(KeyDirection direccion)
    {
        _modelo.CambiarDireccion(direccion);
    }

    public void ProcesarPausa()
    {
        _presenter.CambiarEstado(new PausedState(_presenter, this, _vistaUI));
    }

    public void Salir() { }
}