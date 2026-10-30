using System.Collections;
using System.Collections.Generic;

    public interface IBoard
    {
        int Width { get; }
        int Height { get; }

        Entity GetEntityAt(Coordinate pos);
        void PlaceEntityAt(Coordinate pos, Entity entity);
        void FreeCellAt(Coordinate pos);
        Coordinate GetFreePosition();
    }
