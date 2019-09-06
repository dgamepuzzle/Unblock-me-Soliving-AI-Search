using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe
{
    class Tree : ICloneable{
        //constructor
        public Tree()
        {
            this.carlist = new List<Car>();
            this.metric = new int[6][];
            //add Target Car
            Car firstCar = new Car(99,new int[]{ 2, 0 }, true, 2);
            this.carlist.Add(firstCar);

        }
        //property
        public static int count = 0;
        public int[][] metric = new int[6][];
        public List<Car> carlist;
        public int index;
        public int depth;

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
                    if(this.metric[i][j] < 10)
                        Console.Write(" ");
                    Console.Write(this.metric[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }
        public List<int> checkStepAvailable(Car car)
        {
            List<int> returnValue = new List<int>();
            //horizontal
            if (car.alignment)
            {
                //check on the leftside
                for (int i = 1; i <= car.position[1]; i++)
                {
                    if (car.position[1] != 0 && this.metric[car.position[0]][car.position[1]-i] == 0)
                    {
                        returnValue.Add(-i);
                    }
                    else
                    {
                        break;
                    }
                }
                //check on the rightside
                for(int i = car.position[1]+car.width; i <= 5 ; i++)
                {
                    if (car.position[1] + car.width != 5 && this.metric[car.position[0]][i] == 0)
                    {
                        returnValue.Add(i - car.position[1] - car.width + 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                //check on the top
                for (int i = 1; i <= car.position[0]; i++)
                {
                    if (car.position[0] != 0 && this.metric[car.position[0] - i][car.position[1]] == 0)
                    {
                        returnValue.Add(-i);
                    }
                    else
                    {
                        break;
                    }
                }
                //check on the bottom
                for (int i = car.position[0] + car.width; i <= 5; i++)
                {
                    if (car.position[0] + car.width != 5 && this.metric[i][car.position[1]] == 0)
                    {
                        returnValue.Add(i -car.position[0] - car.width +1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return returnValue;
        }

        public List<Tree> createNewTreeWithAvailableAction()
        {
            List<Tree> treeList = new List<Tree>();
            foreach(Car car in this.carlist)
            {

            }
            return treeList;
        }
        public object Clone()
        {

        }
    }
}
