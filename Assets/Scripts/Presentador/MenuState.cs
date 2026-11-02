using System.Collections.Generic;

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

        bool hayPartida = _presenter.SaveRepository != null && _presenter.SaveRepository.HayPartidaGuardada();
        _vistaUI.SetContinueButtonActive(hayPartida);

        // 1. Inicializa y dibuja el escenario para que el jugador vea la serpiente lista
        _modelo.IniciarJuego();
        _vistaJuego.ClearAll();
        _vistaJuego.RenderWalls(MapearPosiciones(_modelo.ObtenerPosicionesParedes()));
        _vistaJuego.RenderSnake(MapearPosiciones(_modelo.ObtenerPosicionesSerpiente()));
        _vistaJuego.RenderFruit(MapearPosicion(_modelo.ObtenerPosicionFruta()));

        _vistaUI.OnStartClicked += IniciarPartidaPorBoton;
        _vistaUI.OnContinueClicked += ContinuarPartidaGuardada;
    }

    public void ProcesarTick() { } // La serpiente no se mueve hasta que el jugador decida arrancar

    public void ProcesarDireccion(KeyDirection direccion)
    {
        // En cuanto el jugador presiona W/A/S/D o flechas, arranca a jugar en esa direccion
        IniciarPartidaConDireccion(direccion);
    }

    public void ProcesarPausa() { }

    public void Salir()
    {
        _vistaUI.OnStartClicked -= IniciarPartidaPorBoton;
        _vistaUI.OnContinueClicked -= ContinuarPartidaGuardada;
        _vistaUI.HideMenu();
    }

    private void IniciarPartidaPorBoton()
    {
        _presenter.CambiarEstado(new PlayingState(_presenter, _modelo, _vistaJuego, _vistaUI));
    }

    private void ContinuarPartidaGuardada()
    {
        if (_presenter.SaveRepository != null && _presenter.SaveRepository.HayPartidaGuardada())
        {
            DatosPartidaGuardada datos = _presenter.SaveRepository.Cargar();
            if (datos != null)
            {
                _modelo.CargarPartida(datos.Paredes, datos.CuerpoSerpiente, datos.Fruta, datos.Direccion);
                _vistaJuego.ClearAll();
                _vistaJuego.RenderWalls(MapearPosiciones(_modelo.ObtenerPosicionesParedes()));
                _vistaJuego.RenderSnake(MapearPosiciones(_modelo.ObtenerPosicionesSerpiente()));
                _vistaJuego.RenderFruit(MapearPosicion(_modelo.ObtenerPosicionFruta()));

                _presenter.CambiarEstado(new PlayingState(_presenter, _modelo, _vistaJuego, _vistaUI));
            }
        }
    }

    private void IniciarPartidaConDireccion(KeyDirection direccion)
    {
        PlayingState playingState = new PlayingState(_presenter, _modelo, _vistaJuego, _vistaUI);
        _presenter.CambiarEstado(playingState);
        playingState.ProcesarDireccion(direccion);
        playingState.ProcesarTick();
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
}