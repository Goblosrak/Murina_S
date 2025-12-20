using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CollectionsPerformanceLab;

public class ListTester
{
    private const int Size = 500000;
    private const int Iterations = 5;
    
    public void Test()
    {
        Console.WriteLine("List");
        
        var list = new List<int>(Size);
        for (int i = 0; i < Size; i++) list.Add(i);
        
        Measure("Добавление в конец", () => 
        {
            var newList = new List<int>(list);
            newList.Add(999999);
        });
        
        Measure("Добавление в начало", () => 
        {
            var newList = new List<int>(list);
            newList.Insert(0, 999999);
        });
        
        Measure("Добавление в середину", () => 
        {
            var newList = new List<int>(list);
            newList.Insert(Size/2, 999999);
        });
        
        Measure("Удаление из конца", () => 
        {
            var newList = new List<int>(list);
            newList.RemoveAt(newList.Count-1);
        });
        
        Measure("Удаление из начала", () => 
        {
            var newList = new List<int>(list);
            newList.RemoveAt(0);
        });
        
        Measure("Удаление из середины", () => 
        {
            var newList = new List<int>(list);
            newList.RemoveAt(Size/2);
        });
        
        Measure("Поиск элемента", () => 
        {
            var newList = new List<int>(list);
            newList.Contains(Size/2);
        });
        
        Measure("Получение по индексу", () => 
        {
            var newList = new List<int>(list);
            var x = newList[Size/2];
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