using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        pixel hoofd = new pixel();
        hoofd.xpos = screenwidth / 2;
        hoofd.ypos = screenheight / 2;
        hoofd.schermkleur = ConsoleColor.Red;

        string movement = "RIGHT";
        List<int> telje = new List<int>();
        int score = 0;

        DateTime tijd = DateTime.Now;
        string obstacle = "*";
        int obstacleXpos = randomnummer.Next(1, screenwidth);
        int obstacleYpos = randomnummer.Next(1, screenheight);

        while (true)
        {
            Console.Clear();

            // Draw Obstacle
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(hoofd.xpos, hoofd.ypos);
            Console.Write("■");

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }

            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }

            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score: {score}");

            Console.Write("H");

            for (int i = 0; i < telje.Count; i += 2)
            {
                Console.SetCursorPosition(telje[i], telje[i + 1]);
                Console.Write("■");
            }

            // Draw Snake
            Console.SetCursorPosition(hoofd.xpos, hoofd.ypos);
            Console.Write("■");

            ConsoleKeyInfo info = Console.ReadKey();

            // Game Logic
            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    movement = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    movement = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    movement = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    movement = "RIGHT";
                    break;
            }

            if (movement == "UP")
                hoofd.ypos--;
            if (movement == "DOWN")
                hoofd.ypos++;
            if (movement == "LEFT")
                hoofd.xpos--;
            if (movement == "RIGHT")
                hoofd.xpos++;

            // Obstacle Collision
            if (hoofd.xpos == obstacleXpos && hoofd.ypos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth);
                obstacleYpos = randomnummer.Next(1, screenheight);
            }

            telje.Insert(0, hoofd.xpos);
            telje.Insert(1, hoofd.ypos);
            telje.RemoveAt(telje.Count - 1);
            telje.RemoveAt(telje.Count - 1);

            // Collision with Walls or Itself
            if (hoofd.xpos == 0 || hoofd.xpos == screenwidth - 1 || hoofd.ypos == 0 || hoofd.ypos == screenheight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                Console.WriteLine("Game Over");
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                Console.WriteLine($"Dein Score ist: {score}");
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);
                Environment.Exit(0);
            }

            // Self Collision
            for (int i = 0; i < telje.Count; i += 2)
            {
                if (hoofd.xpos == telje[i] && hoofd.ypos == telje[i + 1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                    Console.WriteLine("Game Over");
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                    Console.WriteLine($"Dein Score ist: {score}");
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(50);
        }
    }
}

public class pixel
{
    public int xpos { get; set; }
    public int ypos { get; set; }
    public ConsoleColor schermkleur { get; set; }
    public string karacter { get; set; }
}