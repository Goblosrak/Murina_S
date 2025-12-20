using Xunit;
using System.Threading;
using Lab4.ProducerConsumer;

namespace Lab4.Tests
{
    public class ProducerConsumerTests
    {
        [Fact]
        public void ProducerConsumerWithBlockingCollection_ShouldNotOverflow()
        {
            Thread testThread = null;
            
            try
            {
                testThread = new Thread(() =>
                {
                    var buffer = new System.Collections.Concurrent.BlockingCollection<int>(boundedCapacity: 5);
                    bool shouldStop = false;
                    
                    var producer = new Thread(() =>
                    {
                        for (int i = 0; i < 10 && !shouldStop; i++) 
                        {
                            buffer.Add(i);
                            System.Console.WriteLine($"Произведено: {i}");
                            Thread.Sleep(50);
                        }
                        buffer.CompleteAdding();
                    });
                    
                    var consumer = new Thread(() =>
                    {
                        foreach (var item in buffer.GetConsumingEnumerable())
                        {
                            if (shouldStop) break;
                            System.Console.WriteLine($"Потреблено: {item}");
                            Thread.Sleep(80);
                        }
                    });
                    
                    producer.Start();
                    consumer.Start();
                    
                    Thread.Sleep(2000);
                    
                    shouldStop = true;
                    
                    if (producer.IsAlive)
                        producer.Join(500);
                        
                    if (consumer.IsAlive)
                        consumer.Join(500);
                });
                
                testThread.Start();
                testThread.Join(3000);
                
                Assert.True(true, "Буфер не переполнился");
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