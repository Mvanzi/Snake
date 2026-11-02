using System.Collections.Generic;

public interface IGameContext
{
    int SnakeLength { get; }
    IBoard Board { get; }
    Coordinate SnakeHead { get; }
    Coordinate SnakeTail { get; }
    Coordinate LastClearedTail { get; }
    Coordinate FruitPosition { get; }
    bool IsGameOver { get; }
    bool EstaGameOver { get; }
    bool DidGrowInLastStep { get; }

    void StartNewGame();
    void IniciarJuego();
    void ExecuteStep();
    void AvanzarTurno();
    void TurnSnakeUp();
    void TurnSnakeDown();
    void TurnSnakeLeft();
    void TurnSnakeRight();
    void RespawnFruit();
    void GrowSnake();
    void EndGame();

    IReadOnlyList<Coordinate> ObtenerCuerpoSerpiente();
    IReadOnlyList<Coordinate> ObtenerPosicionesSerpiente();
    IReadOnlyList<Coordinate> ObtenerPosicionesParedes();
    Coordinate ObtenerPosicionFruta();
    IDirection SnakeDirection { get; }

    void CargarPartida(IReadOnlyList<Coordinate> paredes, IReadOnlyList<Coordinate> cuerpo, Coordinate fruta, IDirection direccion);
}