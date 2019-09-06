using System;

namespace UnblockMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Broad mainBroad = new Broad();
            Car car1 = new Car(new int[]{0,2},true,3);
            Car car2 = new Car(new int[]{0,5},false, 3);
            Car car3 = new Car(new int[]{2,2},true, 3);
            Car car4 = new Car(new int[]{3,4},true, 2);
            Car car5 = new Car(new int[]{4,0},false, 2);
            Car car6 = new Car(new int[]{4,4},false, 2);
            Car car7 = new Car(new int[]{5,1},true, 3);

            mainBroad.carlist.Add(car1);
            mainBroad.carlist.Add(car2);
            mainBroad.carlist.Add(car3);
            mainBroad.carlist.Add(car4);
            mainBroad.carlist.Add(car5);
            mainBroad.carlist.Add(car6);
            mainBroad.carlist.Add(car7);

            mainBroad.makeBroad();
            mainBroad.showBroad();

            Console.WriteLine("");

            car1.move(false,2);
            car2.move(true,1);

            mainBroad.makeBroad();
            mainBroad.showBroad();

        }
    }
}
