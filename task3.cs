using System;
using System.Threading;

class Program
{
    static void Main()
    {
        new Thread(() => {
            for (int i = 1; i <= 100; i++) 
            {
                Console.Write(i + " ");
            }
        }).Start();
        
        new Thread(() => {
            for (char c = 'A'; c <= 'Z'; c++) 
            {
                Console.Write(c + " ");
            }
        }).Start();
        
        Console.ReadKey();
    }
}