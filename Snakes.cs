using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnakeGame
{
    internal class Snake
    {
        public string direction;
        public List<(int, int)> snake = new List<(int, int)>();
        public bool end = false;
        
        public Snake(string direction)
        {
            this.direction = direction;
            snake.Add(((int)Border.Right / 2, (int)Border.Top / 2));
        }

        public void Move()
        {
            while(!end)
            {
                Console.SetCursorPosition(snake[snake.Count - 1].Item1, snake[snake.Count - 1].Item2);
                Console.Write(" ");
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    snake[i] = snake[i - 1];
                }

                if (direction == "up")
                {
                    snake[0] = (snake[0].Item1, snake[0].Item2 - 1);
                }
                else if (direction == "down")
                {
                    snake[0] = (snake[0].Item1, snake[0].Item2 + 1);
                }
                else if (direction == "right")
                {
                    snake[0] = (snake[0].Item1 + 1, snake[0].Item2);
                }
                else if (direction == "left")
                {
                    snake[0] = (snake[0].Item1 - 1, snake[0].Item2);
                }
                Console.SetCursorPosition(snake[0].Item1, snake[0].Item2);
                Console.Write("S");
                Thread.Sleep(252);
            }
        }

        public void ChangeDirection()
        {
            while(true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        direction = "left";
                        break;
                    case ConsoleKey.RightArrow:
                        direction = "right";
                        break;
                    case ConsoleKey.UpArrow:
                        direction = "up";
                        break;
                    case ConsoleKey.DownArrow:
                        direction = "down";
                        break;
                }
                Thread.Sleep(100);
            }
        }
    }
}
