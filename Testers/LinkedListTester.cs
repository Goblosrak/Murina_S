using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CollectionsPerformanceLab;

public class LinkedListTester
{
    private const int Size = 500000;
    private const int Iterations = 5;
    
    public void Test()
    {
        Console.WriteLine("LinkedList");
        
        var list = new LinkedList<int>();
        for (int i = 0; i < Size; i++) list.AddLast(i);
        
        Measure("Добавление в конец", () => 
        {
            var newList = new LinkedList<int>(list);
            newList.AddLast(999999);
        });
        
        Measure("Добавление в начало", () => 
        {
            var newList = new LinkedList<int>(list);
            newList.AddFirst(999999);
        });
        
        Measure("Удаление из конца", () => 
        {
            var newList = new LinkedList<int>(list);
            if (newList.Last != null)
                newList.RemoveLast();
        });
        
        Measure("Удаление из начала", () => 
        {
            var newList = new LinkedList<int>(list);
            if (newList.First != null)
                newList.RemoveFirst();
        });
        
        Measure("Поиск элемента", () => 
        {
            var newList = new LinkedList<int>(list);
            newList.Contains(Size/2);
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