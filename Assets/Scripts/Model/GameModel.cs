using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model
{ 

    public class GameModel : IGameContext
    {
        public IBoard Board { get; private set; }
        public Coordinate SnakeHead => snake.GetHead();
        public Coordinate SnakeTail => snake.GetTail();
        public Coordinate LastClearedTail { get; private set; }
        public Coordinate FruitPosition { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool DidGrowInLastStep { get; private set; }

        private ISnake snake;
        private BoardFactoria boardFactory;
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

            boardFactory.CreateBoard();

            Coordinate startPos = Board.GetFreePosition();
            snake = new Snake(startPos, new RightDirection());
            Board.PlaceEntityAt(startPos, new SnakeBodyEntity());

            FruitPosition = Board.GetFreePosition();
            Board.PlaceEntityAt(FruitPosition, new RegularFruit());
        }

        public void ExecuteStep()
        {
            DidGrowInLastStep = false;
            LastClearedTail = snake.GetTail();
            Board.FreeCellAt(LastClearedTail);
            Coordinate nextHeadPos = snake.GetNextHeadPosition();
        }
    }
}