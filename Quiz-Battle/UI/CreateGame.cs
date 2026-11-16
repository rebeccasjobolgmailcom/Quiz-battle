using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Battle.UI
{
    internal class CreateGame
    {
        public List<string> CreatePlayers()
        {
            List<string> playersNames = new  List<string>();

            Console.WriteLine("How many players?");
            int numberOfPlayers = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"Enter name for player {i + 1}:");

                string name = Console.ReadLine();

                

                playersNames.Add(name);
            }

            return playersNames;
            
        }

        public int AskRounds()
        {
            Console.Clear();
            Console.WriteLine("Hur många rundor vill ni spela?");
            int rounds = int.Parse(Console.ReadLine());
            return rounds;
        }



    }
}
