using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CollectionsPerformanceLab;

public class StackTester
{
    private const int Size = 500000;
    private const int Iterations = 5;
    
    public void Test()
    {
        Console.WriteLine("Stack");
        
        var stack = new Stack<int>(Size);
        for (int i = 0; i < Size; i++) stack.Push(i);
        
        Measure("Добавление элементов", () => 
        {
            var newStack = new Stack<int>(stack);
            newStack.Push(999999);
        });
        
        Measure("Удаление элементов", () => 
        {
            var newStack = new Stack<int>(stack);
            if (newStack.Count > 0)
                newStack.Pop();
        });
        
        Measure("Поиск элемента", () => 
        {
            var newStack = new Stack<int>(stack);
            newStack.Contains(Size/2);
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