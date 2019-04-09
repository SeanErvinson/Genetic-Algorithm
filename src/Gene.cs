using System;

namespace Genetic_Algorithm
{
    public class Gene
    {
        public char[] Strands { get; set; }
        public float Fitness { get; set; }
        public String DNA { get => new string(Strands); }
        private Random random;
        public Gene(int targetLength)
        {
            Strands = new char[targetLength];
            random = new Random();
            for (int i = 0; i < targetLength; i++)
            {
                Strands[i] = GenerateStrand(random.Next(63, 122));
            }
        }

        private char GenerateStrand(int number)
        {
            char character = (char)number;
            if (number == 63) character = (char)32;
            if (number == 64) character = (char)46;
            return character;
        }

        public void CalculateFitness(string target)
        {
            int score = 0;
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == Strands[i])
                    score++;
            }
            Fitness = score / (float)target.Length;
        }

        public Gene CrossOver(Gene parent)
        {
            int midPoint = random.Next(0, Strands.Length);
            Gene newGene = new Gene(Strands.Length);
            for (int i = 0; i < Strands.Length; i++)
            {
                if (i > midPoint) newGene.Strands[i] = parent.Strands[i];
                else newGene.Strands[i] = Strands[i];
            }
            return newGene;
        }

        public Gene CrissCross(Gene parent)
        {
            Gene newGene = new Gene(Strands.Length);
            bool alter = false;
            for (int i = 0; i < Strands.Length; i++, alter = !alter)
            {
                if(alter) newGene.Strands[i] = parent.Strands[i];
                else newGene.Strands[i] = Strands[i];
            }
            return newGene;
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < Strands.Length; i++)
            {
                var x = random.NextDouble();
                if (mutationRate > x)
                    Strands[i] = GenerateStrand(random.Next(63, 122));
            }
        }
    }
}