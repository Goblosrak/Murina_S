using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Lab4.ProducerConsumer
{
    public static class ProducerConsumerWithBlockingCollection
    {
        private static readonly BlockingCollection<int> buffer = new BlockingCollection<int>(boundedCapacity: 5);

        public static void Run()
        {
            var producer = new Thread(Produce);
            var consumer = new Thread(Consume);
            producer.Start();
            consumer.Start();
            producer.Join();
            consumer.Join();
        }

        private static void Produce()
        {
            for (int i = 0; i < 20; i++)
            {
                buffer.Add(i);
                Console.WriteLine($"Произведено: {i}");
                Thread.Sleep(200);
            }
            buffer.CompleteAdding();
        }

        private static void Consume()
        {
            foreach (var item in buffer.GetConsumingEnumerable())
            {
                Console.WriteLine($"Потреблено: {item}");
                Thread.Sleep(300);
            }
        }
    }
}