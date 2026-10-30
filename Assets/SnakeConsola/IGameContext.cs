using System.Collections;
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
        bool DidGrowInLastStep { get; }

        void StartNewGame();
        void ExecuteStep();
        void TurnSnakeUp();
        void TurnSnakeDown();
        void TurnSnakeLeft();
        void TurnSnakeRight();
        void RespawnFruit();
        void GrowSnake();
        void EndGame();
    }
