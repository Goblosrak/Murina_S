using System;
using System.Threading;

namespace Lab4.DiningPhilosophers
{
    public class Philosopher
    {
        private readonly int id;
        private readonly object leftFork;
        private readonly object rightFork;
        private bool shouldStop = false;

        public Philosopher(int id, object leftFork, object rightFork)
        {
            this.id = id;
            this.leftFork = leftFork;
            this.rightFork = rightFork;
        }

        public void ThinkAndEat()
        {
            while (!shouldStop)
            {
                try
                {
                    Think();
                    Eat();
                }
                catch (ThreadInterruptedException)
                {
                    shouldStop = true;
                    Console.WriteLine($"Философ {id} прекращает трапезу.");
                }
            }
        }

        public void Stop()
        {
            shouldStop = true;
        }

        private void Think()
        {
            if (shouldStop) return;
            Console.WriteLine($"Философ {id} думает.");
            Thread.Sleep(new Random().Next(100, 300)); 
        }

        private void Eat()
        {
            if (shouldStop) return;
            
            lock (leftFork)
            {
                if (shouldStop) return;
                Console.WriteLine($"Философ {id} взял левую вилку.");
                lock (rightFork)
                {
                    if (shouldStop) return;
                    Console.WriteLine($"Философ {id} взял правую вилку и ест.");
                    Thread.Sleep(new Random().Next(100, 300)); 
                    Console.WriteLine($"Философ {id} положил правую вилку.");
                }
                Console.WriteLine($"Философ {id} положил левую вилку.");
            }
        }
    }
}