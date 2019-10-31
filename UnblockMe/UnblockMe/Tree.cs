using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe
{
    class Tree : ICloneable
    {
        //constructor
        public Tree(List<int[]> carBlueprint)
        {
            this.carlist = new List<Car>();
            this.carBlueprint = carBlueprint;
            this.metric = new int[8][];
            foreach (int[] item in carBlueprint)
            {
                Car carTemp = new Car(
                  item[0],
                  new int[] { item[1], item[2] },
                  Convert.ToBoolean(item[3]),
                  item[4]
                );
                this.carlist.Add(carTemp);
            }
        }
        //property
        public static int count = 0;
        public int[][] metric = new int[6][];
        public List<int[]> carBlueprint;
        public List<Car> carlist;
        public int index = 0;
        public int depth = 0;
        public List<Tree> childList;
        public int action = 0;

        public int cost = 0;

        public int setAction(int car, int move)
        {
            if (car == -1 && move == -1)
            {
                return 1;
            }
            else if (car == -1 && move == 1)
            {
                return 2;
            }
            else if (car == 1 && move == -1)
            {
                return 3;
            }
            else if (car == 1 && move == 1)
            {
                return 4;
            }
            else if (car == 2 && move == -1)
            {
                return 5;
            }
            else if (car == 2 && move == 1)
            {
                return 6;
            }
            else if (car == 3 && move == -1)
            {
                return 7;
            }
            else if (car == 3 && move == 1)
            {
                return 8;
            }
            else if (car == 4 && move == -1)
            {
                return 9;
            }
            else if (car == 4 && move == 1)
            {
                return 10;
            }
            else if (car == 5 && move == -1)
            {
                return 11;
            }
            else if (car == 5 && move == 1)
            {
                return 12;
            }
            else if (car == 6 && move == -1)
            {
                return 13;
            }
            else if (car == 6 && move == 1)
            {
                return 14;
            }
            else if (car == 7 && move == -1)
            {
                return 15;
            }
            else if (car == 7 && move == 1)
            {
                return 16;
            }
            else if (car == 8 && move == -1)
            {
                return 17;
            }
            else if (car == 8 && move == 1)
            {
                return 18;
            }
            else if (car == 9 && move == -1)
            {
                return 19;
            }
            else if (car == 9 && move == 1)
            {
                return 20;
            }
            else if (car == 10 && move == -1)
            {
                return 21;
            }
            else if (car == 10 && move == 1)
            {
                return 22;
            }
            else if (car == 12 && move == -1)
            {
                return 23;
            }
            else if (car == 12 && move == 1)
            {
                return 24;
            }
            return 0;

        }

        public string getAction()
        {
            switch (this.action)
            {
                case 1:
                    return "red car  move Left";
                case 2:
                    return "red car move Right";
                case 3:
                    return "car 1  move Left";
                case 4:
                    return "car 1 move Right";
                case 5:
                    return "car 2 move Up";
                case 6:
                    return "car 2 move Down";
                case 7:
                    return "car 3  move Left";
                case 8:
                    return "car 3 move Right";
                case 9:
                    return "car 4 move Up";
                case 10:
                    return "car 4 move Down";
                case 11:
                    return "car 5  move Left";
                case 12:
                    return "car 5 move Right";
                case 13:
                    return "car 6 move Up";
                case 14:
                    return "car 6 move Down";
                case 15:
                    return "car 7  move Left";
                case 16:
                    return "car 7 move Right";
                case 17:
                    return "car 8 move Up";
                case 18:
                    return "car 8 move Down";
                case 19:
                    return "car 9  move Left";
                case 20:
                    return "car 9 move Right";
                case 21:
                    return "car 10 move Up";
                case 22:
                    return "car 10 move Down";
                case 23:
                    return "car 12 move Up";
                case 24:
                    return "car 12 move Down";
                default:
                    return "";
            }
        }

        public void makeBroad()
        {
            // init array with all 0
            for (int i = 0; i < this.metric.Length; i++)
            {
                if (i == 0 || i == this.metric.Length - 1)
                {
                    this.metric[i] = new int[] { 100, 100, 100, 100, 100, 100, 100, 100 };
                }
                else
                {
                    if (i == 3) this.metric[i] = new int[] { 100, 0, 0, 0, 0, 0, 0, 99 };
                    else this.metric[i] = new int[] { 100, 0, 0, 0, 0, 0, 0, 100 };
                }

            }

            for (int i = 0; i < this.carlist.Count; i++)
            {
                for (int CarWidth = 0; CarWidth < this.carlist[i].width; CarWidth++)
                {
                    if (this.carlist[i].alignment)
                    {
                        this.metric[this.carlist[i].position[0]][this.carlist[i].position[1] + CarWidth] = this.carlist[i].id;
                    }
                    else
                    {
                        this.metric[this.carlist[i].position[0] + CarWidth][this.carlist[i].position[1]] = this.carlist[i].id;
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
                    if (this.metric[i][j] < 10 && this.metric[i][j] >= 0)
                        Console.Write("  ");
                    else if (this.metric[i][j] < 100)
                        Console.Write(" ");
                    Console.Write(this.metric[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }


        public int setCost()
        {
            int car_1_cost = 5 - this.carlist[0].position[1];
            int other_car = 0;

            for (int j = this.carlist[0].position[1] + 1; j < this.metric[6].Length - 1; j++)
            {
                if (this.metric[3][j] >= 1)
                {
                    other_car++;

                }
            }
            this.cost = car_1_cost + other_car;
            return car_1_cost + other_car;
        }

        public List<int> checkStepAvailable(Car car)
        {
            List<int> returnValue = new List<int>();
            //horizontal = 1
            if (car.alignment)
            {
                //check on the leftside
                if (this.metric[car.position[0]][car.position[1] - 1] == 0)
                {
                    //can go to the left
                    returnValue.Add(-1);
                }
                //check on the rightside
                if (this.metric[car.position[0]][car.position[1] + car.width] == 0)
                {
                    //can go to the right
                    returnValue.Add(1);
                }
            }
            else
            {
                //check on the top
                if (this.metric[car.position[0] - 1][car.position[1]] == 0)
                {
                    returnValue.Add(-1);
                }

                //check on the bottom
                if (this.metric[car.position[0] + car.width][car.position[1]] == 0)
                {
                    returnValue.Add(1);
                }

            }
            return returnValue;
        }

        public List<Tree> createNewTreeWithAvailableAction()
        {
            List<Tree> treeList = new List<Tree>();
            for (int i = 0; i < this.carlist.Count; i++)
            {
                if (this.checkStepAvailable(carlist[i]).Count != 0)
                {
                    foreach (int item in this.checkStepAvailable(carlist[i]))
                    {
                        Tree temp = new Tree(this.carBlueprint);
                        int oppositAction = 0;

                        //Console.WriteLine("\n\n...............CHILDREN NODE.........................");
                        //Console.Write("\nCar Number " +this.carlist[i].id);
                        temp.action = setAction(this.carlist[i].id, item);
                        temp.carlist[i].move(item);
                        temp.updateBlueprint();
                        temp.makeBroad();
                        //temp.showBroad();
                        // Console.WriteLine("........................................");
                        temp.setCost();
                        temp.depth = this.depth + 1;
                        if (this.action % 2 == 0)
                        {
                            oppositAction = this.action - 1;
                        }
                        else
                        {
                            oppositAction = this.action + 1;
                        }
                        if (temp.action != oppositAction)
                        {
                            treeList.Add(temp);
                        }
                    }
                }
            }
            this.childList = treeList;
            return treeList;
        }

        public void updateBlueprint()
        {
            List<int[]> blueprint = new List<int[]>();
            foreach (Car car in this.carlist)
            {
                blueprint.Add(new int[] { car.id, car.position[0], car.position[1], car.alignment ? 1 : 0, car.width });
            }
            this.carBlueprint = blueprint;

        }

        private void blueprintToCarlist()
        {
            foreach (int[] item in this.carBlueprint)
            {
                Car carTemp = new Car(
                  item[0],
                  new int[] { item[1], item[2] },
                  Convert.ToBoolean(item[3]),
                  item[4]
                );
                this.carlist.Add(carTemp);
            }
        }

        public bool isEqualTo(Tree checkerTree)
        {
            int countSimilarity = 0;
            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    if (this.metric[i][j] == checkerTree.metric[i][j])
                    {
                        countSimilarity += 1;
                    }
                }
            }
            if (countSimilarity == 36)
                return true;
            return false;
        }

        public bool isReachGoal()
        {
            return metric[this.carlist[0].position[0]][this.carlist[0].position[1] + 2] == 99;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}