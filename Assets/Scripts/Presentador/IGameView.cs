using System.Collections.Generic;

public interface IGameView
{
    void RenderWalls(IEnumerable<PosicionTablero> coordenadasParedes);
    void RenderSnake(IReadOnlyList<PosicionTablero> posicionesSerpiente);
    void RenderFruit(PosicionTablero posicionFruta);
    void ClearAll();
}