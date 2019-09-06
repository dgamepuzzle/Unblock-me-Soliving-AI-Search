using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe
{
    class Broad
    {
        public Broad()
        {
            this.carlist = new List<Car>();
            this.metric = new int[6][];
            //add Target Car
            Car firstCar = new Car(new int[]{ 2, 0 }, true, 2);
            firstCar.id = 9;
            this.carlist.Add(firstCar);

        }
        public int[][] metric = new int[6][];
        public List<Car> carlist;
        public void makeBroad()
        {
            // init array with all 0
            for (int i = 0; i < this.metric.Length; i++)
            {
                this.metric[i] = new int[] { 0, 0, 0, 0, 0, 0 };
            }

            for (int i = 0 ;i < this.carlist.Count; i++)
            {
                for (int CarWidth = 0; CarWidth < this.carlist[i].width; CarWidth++)
                {
                    if (this.carlist[i].alignment)
                    {
                        this.metric[this.carlist[i].position[0]][this.carlist[i].position[1] + CarWidth] = this.carlist[i].id;
                    }else
                    {
                        this.metric[this.carlist[i].position[0]+ CarWidth][this.carlist[i].position[1]] = this.carlist[i].id;
                    }
                }
            }
        }
        public void showBroad()
        {
            for (int i = 0; i < this.metric.Length; i++)
            {
                for (int j = 0; j < this.metric[i].Length; j++)
                {
                    Console.Write(this.metric[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
