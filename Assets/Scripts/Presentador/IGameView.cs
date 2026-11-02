using System.Collections.Generic;

public readonly struct PosicionTablero
{
    public int X { get; }
    public int Z { get; }

    public PosicionTablero(int x, int z)
    {
        X = x;
        Z = z;
    }
}

public interface IGameView
{
    void RenderWalls(IEnumerable<PosicionTablero> coordenadasParedes);
    void RenderSnake(IReadOnlyList<PosicionTablero> posicionesSerpiente);
    void RenderFruit(PosicionTablero posicionFruta);
    void ClearAll();
}