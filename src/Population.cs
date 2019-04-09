using System;
using System.Collections.Generic;

namespace Genetic_Algorithm
{
    public class Population
    {
        private Gene[] _population;
        private readonly float _mutationRate;
        private readonly string _target;
        public int Generations { get; set; } = 0;
        public bool IsFinished { get; set; }

        public Population(string target, int populationSize, float mutationRate)
        {
            _mutationRate = mutationRate;
            _target = target;
            _population = new Gene[populationSize];

            for (int i = 0; i < populationSize; i++)
            {
                _population[i] = new Gene(target.Length);
            }
        }


        public void CalculateFitness()
        {
            foreach (var population in _population)
            {
                population.CalculateFitness(_target);
            }
        }

        private Gene Selection()
        {
            double sumFitness = 0;
            foreach (var population in _population)
            {
                sumFitness += Math.Floor(100 * population.Fitness);
            }
            Random random = new Random();
            int generatedNumber = random.Next(0, (int)sumFitness);
            double accumulatedFitness = 0;
            for (int i = 0; i < _population.Length; i++)
            {
                accumulatedFitness += Math.Floor(100 * _population[i].Fitness);
                if (accumulatedFitness >= generatedNumber)
                    return _population[i];
            }
            return null;
        }

        public void GenerateChild()
        {
            for (int i = 0; i < _population.Length; i++)
            {
                var partnerA = Selection();
                var partnerB = Selection();
                var child = partnerA.CrossOver(partnerB);
                // var child = partnerA.CrissCross(partnerB);
                child.Mutate(_mutationRate);
                child.CalculateFitness(_target);
                _population[i] = child;
            }
            Generations++;
        }

        public String GetBestSpecie()
        {
            float maxFitness = 0.0f;
            int index = 0;
            for (int i = 0; i < _population.Length; i++)
            {
                if (_population[i].Fitness > maxFitness)
                {
                    index = i;
                    maxFitness = _population[i].Fitness * 100;
                }
            }
            if (maxFitness == 100)
                IsFinished = true;
            return _population[index].DNA;
        }
    }
}