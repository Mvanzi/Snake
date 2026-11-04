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

        _modelo.IniciarJuego();
        _vistaJuego.ClearAll();
        _vistaJuego.RenderWalls(MapearPosiciones(_modelo.ObtenerPosicionesParedes()));
        _vistaJuego.RenderSnake(MapearPosiciones(_modelo.ObtenerPosicionesSerpiente()));
        _vistaJuego.RenderFruit(MapearPosicion(_modelo.ObtenerPosicionFruta()));

        _vistaUI.OnStartClicked += IniciarPartidaPorBoton;
        _vistaUI.OnContinueClicked += ContinuarPartidaGuardada;
    }

    public void ProcesarTick() { }

    public void ProcesarDireccion(KeyDirection direccion)
    {
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
                List<Coordinate> paredes = MapearACoordenadas(datos.Paredes);
                List<Coordinate> cuerpo = MapearACoordenadas(datos.CuerpoSerpiente);
                Coordinate fruta = new Coordinate(datos.Fruta.X, datos.Fruta.Z);
                IDirection direccion = MapearCadenaADireccion(datos.Direccion);

                _modelo.CargarPartida(paredes, cuerpo, fruta, direccion);
                _vistaJuego.ClearAll();
                _vistaJuego.RenderWalls(datos.Paredes);
                _vistaJuego.RenderSnake(datos.CuerpoSerpiente);
                _vistaJuego.RenderFruit(datos.Fruta);

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

    private List<Coordinate> MapearACoordenadas(IReadOnlyList<PosicionTablero> posiciones)
    {
        List<Coordinate> coordenadas = new List<Coordinate>(posiciones.Count);
        for (int i = 0; i < posiciones.Count; i++)
        {
            coordenadas.Add(new Coordinate(posiciones[i].X, posiciones[i].Z));
        }
        return coordenadas;
    }

    private IDirection MapearCadenaADireccion(string nombre)
    {
        switch (nombre)
        {
            case "Up": return new UpDirection();
            case "Down": return new DownDirection();
            case "Left": return new LeftDirection();
            default: return new RightDirection();
        }
    }
}