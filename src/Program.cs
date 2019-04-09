using System;

namespace Genetic_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Population population = new Population("Sean Ervinson", 1000, .01f);

            while (true)
            {
                population.CalculateFitness();
                population.GenerateChild();
                System.Console.WriteLine($"Best specie:\t\t{population.GetBestSpecie()}");
                System.Console.WriteLine($"Current Generation:\t{population.Generations}");
                if (population.IsFinished)
                    break;
            }
            System.Console.WriteLine("");
            System.Console.WriteLine($"Total number of Generations: {population.Generations}");

            Console.ReadLine();
        }
    }
}
