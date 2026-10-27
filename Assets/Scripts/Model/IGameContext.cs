using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Snake.Assets.Scripts.Model
{ 

    public interface IGameContext
    {
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
}