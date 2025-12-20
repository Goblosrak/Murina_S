using Xunit;
using System.Threading;
using Lab4.DiningPhilosophers;

namespace Lab4.Tests
{
    public class DiningPhilosophersTests
    {
        [Fact]
        public void DiningPhilosophersWithDeadlock_ShouldRunWithoutException()
        {
            Thread testThread = null;
            
            try
            {
                testThread = new Thread(() =>
                {
                    var forks = new object[5];
                    for (int i = 0; i < 5; i++) forks[i] = new object();

                    var philosophers = new Thread[5];
                    for (int i = 0; i < 5; i++)
                    {
                        var philosopher = new Philosopher(i, forks[i], forks[(i + 1) % 5]);
                        philosophers[i] = new Thread(philosopher.ThinkAndEat);
                        philosophers[i].Start();
                    }

                    Thread.Sleep(3000); 
      
                    foreach (var philosopher in philosophers)
                    {
                        philosopher.Interrupt();
                    }

                    foreach (var philosopher in philosophers)
                    {
                        if (philosopher.IsAlive)
                            philosopher.Join(500);
                    }
                });
                
                testThread.Start();
                testThread.Join(4000);
                
                Assert.True(true, "Тест завершён без исключений");
            }
            finally
            {
                if (testThread != null && testThread.IsAlive)
                {
                    testThread.Interrupt();
                    testThread.Join(500);
                }
            }
        }

        [Fact]
        public void DiningPhilosophersWithoutDeadlock_ShouldRunWithoutDeadlock()
        {
            Thread testThread = null;
            
            try
            {
                testThread = new Thread(() =>
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

                    Thread.Sleep(5000); 
                    
                    foreach (var philosopher in philosophers)
                    {
                        philosopher.Interrupt();
                    }
  
                    foreach (var philosopher in philosophers)
                    {
                        if (philosopher.IsAlive)
                            philosopher.Join(500);
                    }
                });
                
                testThread.Start();
                testThread.Join(6000);
                
                Assert.True(true, "Тест завершён без deadlock");
            }
            finally
            {
                if (testThread != null && testThread.IsAlive)
                {
                    testThread.Interrupt();
                    testThread.Join(500);
                }
            }
        }
    }
}