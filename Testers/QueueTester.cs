using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CollectionsPerformanceLab;

public class QueueTester
{
    private const int Size = 500000;
    private const int Iterations = 5;
    
    public void Test()
    {
        Console.WriteLine("Queue");
        
        var queue = new Queue<int>(Size);
        for (int i = 0; i < Size; i++) queue.Enqueue(i);
        
        Measure("Добавление элементов", () => 
        {
            var newQueue = new Queue<int>(queue);
            newQueue.Enqueue(999999);
        });
        
        Measure("Удаление элементов", () => 
        {
            var newQueue = new Queue<int>(queue);
            if (newQueue.Count > 0)
                newQueue.Dequeue();
        });
        
        Measure("Поиск элемента", () => 
        {
            var newQueue = new Queue<int>(queue);
            newQueue.Contains(Size/2);
        });
    }
    
    private void Measure(string name, Action test)
    {
        long total = 0;
        for (int i = 0; i < Iterations; i++)
        {
            var sw = Stopwatch.StartNew();
            test();
            sw.Stop();
            total += sw.ElapsedMilliseconds;
        }
        Console.WriteLine($"{name}: {total/Iterations} мс");
    }
}