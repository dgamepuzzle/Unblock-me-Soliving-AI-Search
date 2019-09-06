using System;

namespace UnblockMe
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree mainBroad = new Tree();
            Car car1 = new Car(1,new int[]{0,2},true,3);
            Car car2 = new Car(2,new int[]{0,5},false, 3);
            Car car3 = new Car(3,new int[]{2,2},false, 3);
            Car car4 = new Car(4,new int[]{3,4},true, 2);
            Car car5 = new Car(5,new int[]{4,0},false, 2);
            Car car6 = new Car(6,new int[]{4,4},false, 2);
            Car car7 = new Car(7,new int[]{5,1},true, 3);

            mainBroad.carlist.Add(car1);
            mainBroad.carlist.Add(car2);
            mainBroad.carlist.Add(car3);
            mainBroad.carlist.Add(car4);
            mainBroad.carlist.Add(car5);
            mainBroad.carlist.Add(car6);
            mainBroad.carlist.Add(car7);

            mainBroad.makeBroad();
            mainBroad.showBroad();
            
            foreach (Car item in mainBroad.carlist)
            {
                foreach (int step in mainBroad.checkStepAvailable(item))
                {
                    Console.WriteLine(item.id+" : "+step);
                }
            }

            
            /*Console.WriteLine("");
            foreach(int item in mainBroad.checkStepAvailable(car1))
            {
                Console.WriteLine(item);
            }
            car1.move(-1);
            mainBroad.makeBroad();
            mainBroad.showBroad();
            foreach (int item in mainBroad.checkStepAvailable(car1))
            {
                Console.WriteLine(item);
            }*/


            /*car1.move(false,2);
            car2.move(true,1);

            mainBroad.makeBroad();
            mainBroad.showBroad();*/

        }
    }
}
