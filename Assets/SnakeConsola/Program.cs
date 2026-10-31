using System;
// El namespace de tu modelo

class Program
{
    static void Main(string[] args)
    {
        // 1. Instanciamos el modelo del juego
        IGameContext game = new GameModel();
        game.StartNewGame();

        Console.WriteLine("¡Juego iniciado en la consola!");
        Console.WriteLine($"Cabezas en: X={game.SnakeHead.X}, Z={game.SnakeHead.Z}");
        Console.WriteLine($"Fruta en: X={game.FruitPosition.X}, Z={game.FruitPosition.Z}");

        // 2. Simulamos algunos movimientos automáticos o por teclado de consola
        bool jugar = true;
        while (jugar && !game.IsGameOver)
        {
            Console.WriteLine("\nPresioná W (Arriba), A (Izquierda), S (Abajo), D (Derecha) o Q (Salir):");
            char tecla = Console.ReadKey().KeyChar;

            if (tecla == 'q') { jugar = false; break; }
            else if (tecla == 'w') game.TurnSnakeUp();
            else if (tecla == 's') game.TurnSnakeDown();
            else if (tecla == 'a') game.TurnSnakeLeft();
            else if (tecla == 'd') game.TurnSnakeRight();

            // Ejecutamos un tick del modelo
            game.ExecuteStep();

            Console.Clear();
            Console.WriteLine($"--- ESTADO DEL JUEGO ---");
            Console.WriteLine($"Cabeza: ({game.SnakeHead.X}, {game.SnakeHead.Z})");
            Console.WriteLine($"Cola: ({game.SnakeTail.X}, {game.SnakeTail.Z})");
            Console.WriteLine($"Fruta: ({game.FruitPosition.X}, {game.FruitPosition.Z})");

            Console.WriteLine($"Largo actual: {game.SnakeLength}");

            if (game.IsGameOver)
            {
                Console.WriteLine("\n¡GAME OVER! Chocaste contra una pared o tu propio cuerpo.");
            }
        }
    }
}
