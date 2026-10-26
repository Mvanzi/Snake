using System.Collections.Generic;

public struct BoardCoordinate
{
    public int X { get; set; }
    public int Z { get; set; }

    public BoardCoordinate(int x, int z)
    {
        X = x;
        Z = z;
    }
}

public interface IGameView
{
    void RenderWalls(IEnumerable<BoardCoordinate> coordenadasParedes);
    void RenderSnake(IReadOnlyList<BoardCoordinate> posicionesSerpiente);
    void RenderFruit(BoardCoordinate posicionFruta);
    void ClearAll();
}