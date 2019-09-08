using System;
using System.Collections.Generic;
using System.Text;

namespace UnblockMe
{
    class Tree : ICloneable{
        //constructor
        public Tree(List<int[]> carBlueprint)
        {
            this.carlist = new List<Car>();
            this.carBlueprint = carBlueprint;
            this.metric = new int[6][];
            foreach(int[] item in carBlueprint)
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
            for(int i = 0; i < this.carlist.Count; i++)
            {
                if(this.checkStepAvailable(carlist[i]).Count != 0)
                {
                    foreach(int item in this.checkStepAvailable(carlist[i])){
                        Tree temp = new Tree(this.carBlueprint);
                        temp.carlist[i].move(item);
                        temp.updateBlueprint();
                        temp.makeBroad();
                        treeList.Add(temp);
                    }
                }
            }
            this.childList = treeList;
            return treeList;
        }

        public void updateBlueprint()
        {
            List<int[]> blueprint = new List<int[]>();
            foreach(Car car in this.carlist)
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool isEqualTo(Tree checkerTree)
        {
            int countSimilarity=0;
            for(int i=0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if(this.metric[i][j] == checkerTree.metric[i][j])
                    {
                        countSimilarity += 1;
                    }
                }
            }
            if (countSimilarity == 36)
                return true;
            return false;
        }
    }
}
