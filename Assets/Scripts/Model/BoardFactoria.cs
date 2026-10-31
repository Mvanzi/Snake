using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model
{ 

    public class BoardFactoria
    {
        private const int DEFAULT_WIDTH = 40;
        private const int DEFAULT_HEIGHT = 40;
        private const int RANDOM_WALL_COUNT = 50;

        public IBoard CreateBoard()
        {
            IBoard board = new Board(DEFAULT_WIDTH, DEFAULT_HEIGHT);

            CreateBorderWalls(board);
            CreateRandomWalls(board);

            return board;
        }

        private void CreateBorderWalls(IBoard board)
        {
            for (int z = 0; z < board.Height; z++)
            {
                board.PlaceEntityAt(new Coordinate(0, z), new WallEntity());
                board.PlaceEntityAt(new Coordinate(board.Width - 1, z), new WallEntity());
            }
            for (int x = 0; x < board.Width; x++)
            {
                board.PlaceEntityAt(new Coordinate(x, 0), new WallEntity());
                board.PlaceEntityAt(new Coordinate(x, board.Height - 1), new WallEntity());
            }
        }

        private void CreateRandomWalls(IBoard board)
        {
            for (int i = 0; i < RANDOM_WALL_COUNT; i++)
            {
                Coordinate freePos = board.GetFreePosition();
                board.PlaceEntityAt(freePos, new WallEntity());
            }
        }
    }
}