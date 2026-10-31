using System.Collections;
using System.Collections.Generic;

    public class Board : IBoard
    {
        public int Width { get; }
        public int Height { get; }
        public ICell[,] matrix;

        private System.Random random = new System.Random();
        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            matrix = new ICell[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                matrix[x, z] = new Cell();
            }
        }
    }

        public Entity GetEntityAt(Coordinate pos)
        {
            return matrix[pos.X, pos.Z].CurrentEntity;
        }

        public void PlaceEntityAt(Coordinate pos, Entity entity)
        {
            matrix[pos.X, pos.Z].CurrentEntity = entity;
        }

        public void FreeCellAt(Coordinate pos)
        {
            matrix[pos.X, pos.Z].Clear();
        }

        private bool IsFreeSurroundings(Coordinate pos)
        {
            bool isAvailable = true;
            for (int i = pos.X - 1; i <= pos.X + 1 && isAvailable; i++)
            {
                for (int j = pos.Z - 1; j <= pos.Z + 1 && isAvailable; j++)
                {
                    isAvailable = matrix[i, j].IsEmpty;
                }
            }
            return isAvailable;
        }

        public Coordinate GetFreePosition()
        {
            bool isAvailable = false;
            Coordinate newFreePosition = new Coordinate(0, 0);

            while (!isAvailable)
            {
                newFreePosition = new Coordinate(random.Next(1, Width - 1), random.Next(1, Height - 1));
                isAvailable = IsFreeSurroundings(newFreePosition);
            }
            return newFreePosition;
        }
    }
