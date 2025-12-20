using System;
using System.Threading;

namespace Lab4.DiningPhilosophers
{
    public static class DiningPhilosophersWithoutDeadlock
    {
        public static void Run()
        {
            var forks = new object[5];
            for (int i = 0; i < 5; i++) forks[i] = new object();

            var philosophers = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                var leftFork = forks[i];
                var rightFork = forks[(i + 1) % 5];
                if (i == 4)
                {
                    leftFork = forks[(i + 1) % 5];
                    rightFork = forks[i];
                }
                var philosopher = new Philosopher(i, leftFork, rightFork);
                philosophers[i] = new Thread(philosopher.ThinkAndEat);
                philosophers[i].Start();
            }

            foreach (var philosopher in philosophers) philosopher.Join();
        }
    }
}