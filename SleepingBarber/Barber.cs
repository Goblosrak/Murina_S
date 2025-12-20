namespace Lab4.SleepingBarber
{
    public class Barber
    {
        private readonly BarberShop shop;

        public Barber(BarberShop shop)
        {
            this.shop = shop;
        }

        public void Work()
        {
            shop.BarberWork();
        }
    }
}