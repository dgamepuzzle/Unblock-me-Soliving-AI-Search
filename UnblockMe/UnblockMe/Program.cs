using System;
using System.Collections.Generic;

namespace UnblockMe
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int[]> blueprint = new List<int[]>
            {
                new int[]{99,2,0,1,2},
                new int[]{1,0,2,1,3},
                new int[]{2,0,5,0,3},
                new int[]{3,2,2,0,3},
                new int[]{4,3,4,1,2},
                new int[]{5,4,0,0,2},
                new int[]{6,4,4,0,2},
                new int[]{7,5,1,1,3}
            };
            List<int[]> EasyBlueprint = new List<int[]>
            {
                new int[]{99,2,0,1,2},
                new int[]{1,0,0,0,2},
                new int[]{2,0,2,0,2},
                new int[]{3,0,3,1,2},
                new int[]{4,1,3,1,3},
                new int[]{5,3,0,1,2},
                new int[]{6,3,2,1,2},
                new int[]{7,4,0,0,2},
                new int[]{8,5,1,1,2},
                new int[]{9,4,3,0,2},
                new int[]{10,4,4,0,2},
                new int[]{11,4,5,0,2},
            };

            /*Tree root = new Tree(blueprint);

            root.makeBroad();
            root.showBroad();
            Console.WriteLine();*/
            makeTree(EasyBlueprint);

            Console.WriteLine("Done");



        }

        public static Tree makeTree(List<int[]> blueprint)
        {
            //add first root's child to the queue
            List<Tree> checkingList = new List<Tree>();
            Tree root = new Tree(blueprint);
            root.makeBroad();
            Queue<Tree> BFS_queue = new Queue<Tree>();
            foreach (Tree item in root.createNewTreeWithAvailableAction())
            {
                if(checkingList.Count == 0)
                {
                    checkingList.Add(item);
                    BFS_queue.Enqueue(item);
                }
                else
                {
                    foreach (Tree checkingTree in checkingList)
                    {
                        if (item.isEqualTo(checkingTree))
                        {
                            break;
                        }
                    }
                    checkingList.Add(item);
                    BFS_queue.Enqueue(item);
                }
            }
            while(BFS_queue.Count != 0)
            {
                Tree temp = BFS_queue.Dequeue();
                foreach (Tree item in temp.createNewTreeWithAvailableAction())
                {
                    bool checkSimBool = false;
                    foreach (Tree checkingTree in checkingList)
                    {
                        if (item.isEqualTo(checkingTree))
                        {
                            checkSimBool = true;
                            /*Console.WriteLine("collide !!!");
                            item.showBroad();
                            Console.WriteLine("======================================");
                            checkingTree.showBroad();
                            Console.WriteLine("");*/
                            break;
                        }
                    }
                    if (!checkSimBool)
                    {
                        checkingList.Add(item);
                        BFS_queue.Enqueue(item);
                        item.showBroad();
                        Console.WriteLine("Checking List :"+checkingList.Count);
                        Console.WriteLine();
                    }
                }

            }

            return root;
        }

        public void testIterate(Tree root)
        {
            foreach (Tree item in root.createNewTreeWithAvailableAction())
            {
                item.makeBroad();
                item.showBroad();
                Console.WriteLine();
            }

            Console.WriteLine("=================================================");

            foreach (Tree i in root.childList)
            {
                foreach (Tree j in i.createNewTreeWithAvailableAction())
                {
                    j.makeBroad();
                    j.showBroad();
                    Console.WriteLine();
                }
                Console.WriteLine("=================================================");
            }

            foreach (Tree i in root.childList)
            {
                foreach (Tree j in i.childList)
                {
                    foreach (Tree k in j.createNewTreeWithAvailableAction())
                    {
                        j.makeBroad();
                        j.showBroad();
                        Console.WriteLine();
                    }
                    Console.WriteLine("=================================================");
                }

            }
     
        }
    }
}
