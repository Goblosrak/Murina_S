using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab4.SleepingBarber
{
    public class BarberShop
    {
        private readonly SemaphoreSlim clientsWaiting = new SemaphoreSlim(0);
        private readonly Mutex mutex = new Mutex();
        private readonly Queue<Client> waitingClients = new Queue<Client>();
        private const int MaxClients = 5;
        private bool isBarberSleeping = true;
        private bool shouldStop = false;

        public void ClientArrives(Client client)
        {
            mutex.WaitOne();
            
            if (waitingClients.Count >= MaxClients)
            {
                Console.WriteLine($"Клиент {client.Id} ушёл (нет мест).");
                mutex.ReleaseMutex();
                return;
            }
            
            waitingClients.Enqueue(client);
            Console.WriteLine($"Клиент {client.Id} в очереди. Всего: {waitingClients.Count}");
            
            mutex.ReleaseMutex();
            
            if (isBarberSleeping)
            {
                isBarberSleeping = false;
                clientsWaiting.Release();
            }
        }

        public void BarberWork()
        {
            Console.WriteLine("Парикмахер начинает рабочий день.");
            
            while (!shouldStop)
            {
                try
                {
                    if (waitingClients.Count == 0)
                    {
                        isBarberSleeping = true;
                        Console.WriteLine("Парикмахер засыпает...");
                        clientsWaiting.Wait();
                        isBarberSleeping = false;
                        Console.WriteLine("Парикмахер просыпается!");
                    }
                  
                    mutex.WaitOne();
                    
                    if (waitingClients.Count == 0)
                    {
                        mutex.ReleaseMutex();
                        continue;
                    }
                    
                    var client = waitingClients.Dequeue();
                    Console.WriteLine($"Парикмахер взял клиента {client.Id}. Осталось: {waitingClients.Count}");
                    mutex.ReleaseMutex();

                    Console.WriteLine($"Парикмахер стрижёт клиента {client.Id}.");
                    Thread.Sleep(500);
                    Console.WriteLine($"Парикмахер закончил с клиентом {client.Id}.");
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Парикмахер получил сигнал прерывания.");
                    shouldStop = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    shouldStop = true;
                }
            }
            
            Console.WriteLine("Парикмахер заканчивает рабочий день.");
        }
        
        public void Stop()
        {
            shouldStop = true;
        }
    }
}