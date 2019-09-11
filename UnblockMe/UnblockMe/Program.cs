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
                new int[]{-1,2,0,1,2},
                // new int[]{1,0,2,1,3},
                // new int[]{2,0,5,0,3},
                // new int[]{4,2,2,0,3},
                // new int[]{5,3,4,1,2},
                // new int[]{6,4,0,0,2},
                // new int[]{8,4,4,0,2},
                // new int[]{7,5,1,1,3}
            };
            List<int[]> EasyBlueprint = new List<int[]>
            {
                new int[]{-1,3,1,1,2},
                new int[]{2,1,1,0,2},
                new int[]{4,2,3,0,2},
                new int[]{1,1,4,1,2},
                new int[]{3,2,4,1,3},
                new int[]{5,4,1,1,2},
                new int[]{7,4,3,1,2},
                new int[]{6,5,1,0,2},
                new int[]{9,6,2,1,2},
                new int[]{8,5,4,0,2},
                new int[]{10,3,5,0,3},
                new int[]{12,3,6,0,2},
            };

        
            // Tree root = new Tree(EasyBlueprint);

            // root.makeBroad();
            // root.showBroad();
            // foreach (int i in root.checkStepAvailable(root.carlist[10]) )
            //     Console.WriteLine("Available : "+i);
            // root.createNewTreeWithAvailableAction();
            // foreach(Tree child in root.childList)
            // {
            //     Console.WriteLine("======================================");
            //     child.makeBroad();
            //     child.showBroad();
            // }

            //BFS(EasyBlueprint);
            //Depth_Limited(blueprint, 8);
            Console.Write("------------------ Start Searching with Depth First Search --------------------");
            DFS(EasyBlueprint);


            Console.WriteLine("--------------- Done ----------------------");



        }

        public static Tree DFS(List<int[]> blueprint ){
            Tree root = new Tree(blueprint);
            root.makeBroad();
            //root.showBroad();
            Console.Write("-----------------------------------------------------");
            Console.Write("-----------------------------------------------------");
            Stack<Tree> gameStack = new Stack<Tree>();

            //push selected node
            gameStack.Push(root);
            int number_curr = 1;
           
            while(gameStack.Count>0){

            // 1. check if that node is The goal node
            Tree currentNode = gameStack.Pop();

            if(currentNode.isReachGoal()){
               Console.Write("\n---------------- REACH GOAL --------------\n"); 
                Console.WriteLine("Number of current node "+number_curr);
                Console.Write("Current Depth is " +currentNode.depth+"\n");


                currentNode.makeBroad();
                currentNode.showBroad();
                // end of search
                break;

            }else{
                // if currentNode is not Goal node
                // expand currentNode
               
                Console.WriteLine("Number of current node "+number_curr);
                number_curr++;
               
                currentNode.createNewTreeWithAvailableAction();
                
                
                

                Console.Write("Current Depth is " +currentNode.depth+"\n");
                currentNode.makeBroad();
                currentNode.showBroad();
                Console.WriteLine("........................................");
                if(currentNode.depth < 10){
                for( int i = currentNode.childList.Count-1; i>=0;i--){
                    //child.makeBroad();
                    //child.showBroad();
                    gameStack.Push(currentNode.childList[i]);
                }
                }
                
                
                
            }
           
            }

            return root;
            
        }

        public static Tree BFS(List<int[]> blueprint)
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
            bool isReachGoal = false;
            while(BFS_queue.Count != 0 && !isReachGoal)
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
                            //Console.WriteLine("collide !!!");
                            break;
                        }
                    }
                    if (!checkSimBool)
                    {
                        checkingList.Add(item);
                        BFS_queue.Enqueue(item);
                        item.showBroad();
                        Console.WriteLine("Checking List :"+checkingList.Count);
                        Console.WriteLine(item.depth);
                        Console.WriteLine();
                    }
                    //Console.WriteLine(item.carlist[0].position[0] + " , "+ item.carlist[0].position[1]);
                    if (item.isReachGoal())
                    {
                        Console.WriteLine("Runnnnnnnnnnnnnnnnnnnnnnnnnnnn");
                        item.showBroad();
                        Console.WriteLine("Checking List :" + checkingList.Count);
                        isReachGoal = true;
                        break;
                    }
                }

            }

            return root;
        }

        public static Tree Depth_Limited(List<int[]> blueprint,int depth)
        {
            //add first root's child to the queue
            List<Tree> checkingList = new List<Tree>();
            Tree root = new Tree(blueprint);
            root.makeBroad();
            Queue<Tree> BFS_queue = new Queue<Tree>();
            foreach (Tree item in root.createNewTreeWithAvailableAction())
            {
                if (checkingList.Count == 0)
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
            bool isReachGoal = false;
            while (BFS_queue.Count != 0 && !isReachGoal)
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
                            //Console.WriteLine("collide !!!");
                            break;
                        }
                    }
                    if (!checkSimBool)
                    {
                        checkingList.Add(item);
                        BFS_queue.Enqueue(item);
                        item.showBroad();
                        Console.WriteLine("Checking List :" + checkingList.Count);
                        Console.WriteLine(item.depth);
                        Console.WriteLine();
                    }
                    //Console.WriteLine(item.carlist[0].position[0] + " , "+ item.carlist[0].position[1]);
                    if (item.isReachGoal())
                    {
                        Console.WriteLine("Runnnnnnnnnnnnnnnnnnnnnnnnnnnn");
                        item.showBroad();
                        Console.WriteLine("Checking List :" + checkingList.Count);
                        isReachGoal = true;
                        break;
                    }
                }
                if(temp.depth >= depth)
                {
                    Console.WriteLine("Reach depth limit!!!");
                    break;
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
