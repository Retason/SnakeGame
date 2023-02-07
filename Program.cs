using System;
using System.Threading;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake("");
            Console.CursorVisible = false;

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    snake.direction = "left";
                    break;
                case ConsoleKey.RightArrow:
                    snake.direction = "right";
                    break;
                case ConsoleKey.UpArrow:
                    snake.direction = "up";
                    break;
                case ConsoleKey.DownArrow:
                    snake.direction = "down";
                    break;
            }

            int score = 0;

            // Создание еды
            Random random = new Random();
            (int, int) food = (random.Next(1, (int)Border.Right - 1), random.Next(1, (int)Border.Top - 1));
            PrintBorders();
            PrintFood(food);

            Thread change = new Thread(snake.ChangeDirection);
            change.Start();
            Thread move = new Thread(snake.Move);
            move.Start();

            while (true)
            {
                // Коллизия еды
                if (snake.snake[0].Item1 == food.Item1 && snake.snake[0].Item2 == food.Item2)
                {
                    score++;
                    snake.snake.Add((0, 0));
                    food = (random.Next(1, (int)Border.Right - 1), random.Next(1, (int)Border.Top - 1));
                    PrintFood(food);
                }

                // Коллизия стен
                if (snake.snake[0].Item1 <= 0 || snake.snake[0].Item1 >= (int)Border.Right ||
                    snake.snake[0].Item2 <= 0 || snake.snake[0].Item2 >= (int)Border.Top)
                {
                    Console.Clear();
                    Console.WriteLine("Вы проиграли! Ваш счет: " + score);
                    snake.end = true;
                    break;
                }

                // Коллизия змейки
                for (int i = 1; i < snake.snake.Count; i++)
                {
                    if (snake.snake[0].Item1 == snake.snake[i].Item1 && snake.snake[0].Item2 == snake.snake[i].Item2)
                    {
                        Console.Clear();
                        Console.WriteLine("Вы проиграли! Ваш счет: " + score);
                        snake.end = true;
                        break;
                    }
                }
                
                Thread.Sleep(250);
            }
        }

        static void PrintBorders()
        {
            Console.Write(new String('-', (int)Border.Right));
            Console.SetCursorPosition(0, (int)Border.Top);
            Console.Write(new String('-', (int)Border.Right));
            Console.SetCursorPosition((int)Border.Right, 0);
            for (int i = 0; i < (int)Border.Top; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }
            for (int i = 0; i < (int)Border.Top; i++)
            {
                Console.SetCursorPosition((int)Border.Right, i);
                Console.Write("|");
            }
        }

        static void PrintFood((int, int) food)
        {
            Console.SetCursorPosition(food.Item1, food.Item2);
            Console.Write("F");
        }
    }
}