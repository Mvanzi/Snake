using System.Collections.Generic;

public class GameModel : IGameContext
{
    public IBoard Board { get; private set; }
    public Coordinate SnakeHead => snake.GetHead();
    public Coordinate SnakeTail => snake.GetTail();
    public Coordinate LastClearedTail { get; private set; }
    public Coordinate FruitPosition { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool EstaGameOver => IsGameOver;
    public bool DidGrowInLastStep { get; private set; }
    public int SnakeLength => snake != null ? snake.Length : 0;

    private ISnake snake;
    private readonly BoardFactoria boardFactory;
    private bool pendingGrowth;

    public GameModel()
    {
        boardFactory = new BoardFactoria();
    }

    public void StartNewGame()
    {
        IsGameOver = false;
        DidGrowInLastStep = false;
        pendingGrowth = false;

        Board = boardFactory.CreateBoard();

        Coordinate startPos = Board.GetFreePosition();
        snake = new Snake(startPos, new RightDirection());
        Board.PlaceEntityAt(startPos, new SnakeBodyEntity());

        FruitPosition = Board.GetFreePosition();
        Board.PlaceEntityAt(FruitPosition, new RegularFruit());
    }

    public void IniciarJuego() => StartNewGame();

    public void ExecuteStep()
    {
        DidGrowInLastStep = false;
        LastClearedTail = snake.GetTail();
        Board.FreeCellAt(LastClearedTail);
        Coordinate nextHeadPos = snake.GetNextHeadPosition();
        Entity targetEntity = Board.GetEntityAt(nextHeadPos);
        targetEntity.OnCollide(this);
        snake.MoveTo(nextHeadPos);
        Board.PlaceEntityAt(nextHeadPos, new SnakeBodyEntity());

        if (pendingGrowth)
        {
            pendingGrowth = false;
            DidGrowInLastStep = true;
            snake.Grow();
            Board.PlaceEntityAt(snake.GetTail(), new SnakeBodyEntity());
        }
    }

    public void AvanzarTurno() => ExecuteStep();

    public void RespawnFruit()
    {
        Board.FreeCellAt(FruitPosition);
        FruitPosition = Board.GetFreePosition();
        Board.PlaceEntityAt(FruitPosition, new RegularFruit());
    }

    public void GrowSnake()
    {
        pendingGrowth = true;
    }

    public void EndGame()
    {
        IsGameOver = true;
    }

    public void TurnSnakeUp()
    {
        snake.TurnUp();
    }

    public void TurnSnakeDown()
    {
        snake.TurnDown();
    }

    public void TurnSnakeLeft()
    {
        snake.TurnLeft();
    }

    public void TurnSnakeRight()
    {
        snake.TurnRight();
    }

    public IReadOnlyList<Coordinate> ObtenerCuerpoSerpiente()
    {
        return snake != null ? snake.BodyPositions : new List<Coordinate>().AsReadOnly();
    }

    public IReadOnlyList<Coordinate> ObtenerPosicionesSerpiente() => ObtenerCuerpoSerpiente();

    public IReadOnlyList<Coordinate> ObtenerPosicionesParedes()
    {
        return Board != null ? Board.WallPositions : new List<Coordinate>().AsReadOnly();
    }

    public Coordinate ObtenerPosicionFruta() => FruitPosition;
    public IDirection SnakeDirection => snake != null ? snake.CurrentDirection : new RightDirection();

    public void CargarPartida(IReadOnlyList<Coordinate> paredes, IReadOnlyList<Coordinate> cuerpo, Coordinate fruta, IDirection direccion)
    {
        IsGameOver = false;
        DidGrowInLastStep = false;
        pendingGrowth = false;

        Board = new Board(40, 40);

        for (int i = 0; i < paredes.Count; i++)
        {
            Board.PlaceEntityAt(paredes[i], new WallEntity());
        }

        snake = new Snake(cuerpo, direccion);
        for (int i = 0; i < cuerpo.Count; i++)
        {
            Board.PlaceEntityAt(cuerpo[i], new SnakeBodyEntity());
        }

        FruitPosition = fruta;
        Board.PlaceEntityAt(fruta, new RegularFruit());
    }
}