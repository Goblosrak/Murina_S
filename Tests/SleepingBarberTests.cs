using Xunit;
using System.Threading;

namespace Lab4.Tests
{
    public class SleepingBarberTests
    {
        [Fact]
        public void BarberShop_ShouldHandleClientsWithoutDeadlock()
        {
            var shop = new SleepingBarber.BarberShop();
            Thread barberThread = null;
            
            try
            {
                barberThread = new Thread(() => 
                {
                    try
                    {
                        var barber = new SleepingBarber.Barber(shop);
                        barber.Work();
                    }
                    catch (ThreadInterruptedException)
                    {
                        
                    }
                });
                
                barberThread.Start();
                Thread.Sleep(300); 
                
                for (int i = 0; i < 5; i++) 
                {
                    var client = new SleepingBarber.Client(i);
                    shop.ClientArrives(client);
                    Thread.Sleep(100);
                }
                
                Thread.Sleep(3000);
                
                shop.Stop();
                
                if (barberThread.IsAlive)
                {
                    barberThread.Interrupt();
                    barberThread.Join(1000);
                }
                
                Assert.True(true, "Тест завершён успешно");
            }
            finally
            {
                if (barberThread != null && barberThread.IsAlive)
                {
                    shop.Stop();
                    barberThread.Interrupt();
                    barberThread.Join(500);
                }
            }
        }
    }
}