using System;
using System.Collections.Generic;
using System.Linq;

namespace UnblockMe {
    class Program {
        static void Main (string[] args) {

            // Problem 0 
            // List<int[]> EasyBlueprint = new List<int[]> {
            //   new int[] {-1, 3, 1, 1, 2 },
            //   new int[] { 1, 1, 4, 1, 2 },
            //   new int[] { 2, 1, 1, 0, 2 },
            //   new int[] { 3, 2, 4, 1, 3 },
            //   new int[] { 4, 2, 3, 0, 2 },
            //   new int[] { 5, 4, 1, 1, 2 },
            //   new int[] { 6, 5, 1, 0, 2 },
            //   new int[] { 7, 4, 3, 1, 2 },
            //   new int[] { 8, 5, 4, 0, 2 },
            //   new int[] { 9, 6, 2, 1, 2 },
            //   new int[] { 10, 3, 5, 0, 3 },
            //   new int[] { 12, 3, 6, 0, 2 },
            // };

            // Problem 1
            // List<int[]> EasyBlueprint = new List<int[]> {
            //     new int[] {-1, 3, 1, 1, 2 }
            // };

            // Problem 2
            // List<int[]> EasyBlueprint = new List<int[]> {
            //     new int[] {-1, 3, 1, 1, 2 },
            //     new int[] { 2, 1, 4, 0, 2 },
            //     new int[] { 3, 1, 2, 1, 2 },
            //     new int[] { 4, 3, 4, 0, 2 },
            //     new int[] { 5, 2, 5, 1, 2 },
            //     new int[] { 6, 3, 6, 0, 3 },
            //     new int[] { 7, 6, 1, 1, 2 },
            //     new int[] { 8, 4, 3, 0, 3 },
            //     new int[] { 9, 6, 4, 1, 3 },
            // };

            // Problem 3
            // List<int[]> EasyBlueprint = new List<int[]> {
            //     new int[] {-1, 3, 1, 1, 2 },
            //     new int[] { 2, 1, 6, 0, 6 },
            // };

            // Problem 4
            List<int[]> EasyBlueprint = new List<int[]> {
                new int[] {-1, 3, 1, 1, 2 },

                new int[] { 1, 2, 1, 1, 2 },
                new int[] { 2, 1, 6, 0, 3 },
                new int[] { 3, 2, 4, 1, 2 },
                new int[] { 4, 5, 6, 0, 2 },
                new int[] { 5, 5, 3, 1, 3 },
                new int[] { 6, 4, 2, 0, 3 },
                new int[] { 8, 2, 3, 0, 3 },
                new int[] { 10, 3, 5, 0, 2 },
            };

            // Problem 5
            //   List<int[]> EasyBlueprint = new List<int[]> {
            //         new int[] {-1, 3, 2, 1, 2 },
            //         new int[] { 5, 6, 5, 1, 2 },
            //         new int[] { 2, 3, 6, 0, 3 },
            //         new int[] { 4, 4, 4, 0, 3 },

            //         };

            var watch = new System.Diagnostics.Stopwatch ();
            long dfs_time = 0;
            long ids_time = 0;
            long dfs_ms_time = 0;
            long ids_ms_time = 0;
            long compare_time = 0;

            Console.Write ("------------------ DEPTH FIRST WITH LIMIT SEARCH --------------------");
            watch.Start ();
            List<long> returnDFS = new List<long> ();

            // Greedy (EasyBlueprint, 10);
            DFS (EasyBlueprint, 10);
            // Console.WriteLine(returnDFS[1]);
            watch.Stop ();
            Console.WriteLine ("----------------------- END -------------------------");
            dfs_time = watch.ElapsedMilliseconds / 1000;
            dfs_ms_time = watch.ElapsedMilliseconds;
            Console.WriteLine ("Execution Time: " + (dfs_time == 0 ? dfs_ms_time : dfs_time) + (dfs_time == 0 ? " ms" : " sec"));

            // Console.Write("------------------ ITERATIVE DEPTH FIRST SEARCH --------------------");
            // if (!watch.IsRunning)
            // {
            //   watch.Restart();
            // }
            // IDS(EasyBlueprint, 10);
            // watch.Stop();
            // Console.WriteLine("----------------------- END -------------------------");
            // ids_time = watch.ElapsedMilliseconds / 1000;
            // ids_ms_time = watch.ElapsedMilliseconds;
            // Console.WriteLine("Execution Time: " + (ids_time == 0 ? ids_ms_time : ids_time) + (ids_time == 0 ? " ms" : " sec"));

            // Console.WriteLine("\n\n===============================================================================================================\n\n");
            // Console.WriteLine("                      Depth First Search with Limit     |     Iterative Depth First Search     ");
            // Console.WriteLine("Execution Time:                   " + (dfs_time == 0 ? dfs_ms_time : dfs_time) + (dfs_time == 0 ? " ms" : " sec") + "                                " + (ids_time == 0 ? ids_ms_time : ids_time) + (ids_time == 0 ? " ms" : " sec"));
            // Console.WriteLine("Memory Usage (GC):                " + dfs_mem + " KB                              " + ids_mem + " KB");
            // if (dfs_time == 0 || ids_time == 0)
            // {
            //   compare_time = dfs_ms_time / ids_ms_time;
            // }
            // else
            // {
            //   compare_time = dfs_time / ids_time;
            // }
            // Console.WriteLine("\n\nConclusion: IDS is faster than DFS by " + compare_time + "X");

        }

        public static Tree Greedy (List<int[]> blueprint, int limit) {

            long max_nodes = 0;
            long start_mem = GC.GetTotalMemory (true);
            long maxi_mem = 0;
            long mem_used = 0;
            // ....................... Initial Setting ....................................
            Tree root = new Tree (blueprint);

            root.makeBroad ();

            Console.Write ("-----------------------------------------------------");
            Console.Write ("-----------------------------------------------------");

            List<Tree> gameList = new List<Tree> ();

            //push selected node
            gameList.Add (root);

            int maxStack = 0;
            int number_curr = 1;
            int temp_depth = -1;
            bool found = false;

            // ....................... loop through stack ....................................
            while (gameList.Count > 0) {
                Console.WriteLine ("gameList Count : " + gameList.Count);
                // 1. pop node from stack and save it as currentNode
                Tree currentNode = gameList[0];
                gameList.RemoveAt (0);
                // 2. Check if currentNode is Goal Node or not
                if (currentNode.isReachGoal ()) {

                    //currentNode == Goal Node
                    Console.Write ("\n---------------- REACH GOAL --------------\n");
                    Console.WriteLine ("Number of current node " + number_curr);
                    Console.Write ("Current Depth is " + currentNode.depth + "\n");
                    Console.Write ("Action: " + " (" + currentNode.action + ") " + currentNode.getAction () + "\n");
                    Console.WriteLine ("");
                    List<int> reachPath = new List<int> ();

                    foreach (int number in currentNode.path) {
                        reachPath.Add (number);
                    }
                    reachPath.Insert (0, 0);
                    for (int i = 0; i < reachPath.Count; i++) {
                        Console.WriteLine ("Action (" + reachPath[i] + ") " + actionsToString (reachPath[i]));
                    }

                    currentNode.makeBroad ();
                    currentNode.showBroad ();

                    // Console.Write ("maxStack is " + maxStack + "\n");
                    Console.Write ("max Stack is " + max_nodes + "\n");

                    // Console.Write ("mem 1 node is " + mem_used + "\n");
                    // Console.Write ("mem used is " + mem_used * max_nodes + "\n");
                    // Console.Write ("max mem " + maxi_mem + "\n");

                    // long end_mem = (max_mem - start_mem);

                    // Console.Write ("Memory is " + end_mem + " Bytes \n");
                    found = true;
                    //end of search
                    break;

                } else {
                    // currentNode != Goal node
                    Console.WriteLine ("Number of current node " + number_curr);
                    number_curr++;

                    //Expand currentNode
                    currentNode.createNewTreeWithAvailableAction ();

                    Console.Write ("Current Depth is " + currentNode.depth + "\n");
                    Console.Write ("Action: " + " (" + currentNode.action + ") " + currentNode.getAction () + "\n");
                    currentNode.makeBroad ();
                    currentNode.showBroad ();

                    Console.WriteLine ("........................................");

                    // return root;

                    //Push currentNode's children into stack
                    if (currentNode.depth < limit) {
                        for (int i = currentNode.childList.Count - 1; i >= 0; i--) {
                            gameList.Add (currentNode.childList[i]);
                            if (maxStack < gameList.Count)
                                maxStack = gameList.Count;
                            // long temp_mem = GC.GetTotalMemory (true);
                            // if (max_mem < temp_mem) {
                            //     max_mem = temp_mem;
                            // }
                            if (max_nodes < gameList.Count) {
                                max_nodes = gameList.Count;
                                long temp_mem = GC.GetTotalMemory (true);
                                maxi_mem = (temp_mem - start_mem);
                            }

                        }
                    }

                    temp_depth = currentNode.depth;

                }
                List<Tree> sortedGameList = gameList.OrderBy (o => o.cost).ToList ();
                gameList = sortedGameList;
            }
            if (!found) {
                Console.WriteLine ("!!! Not reach goal !!!");
                Console.Write ("max Stack is " + max_nodes + "\n");
                // Console.Write ("max mem " + maxi_mem + "\n");

            }
            return root;

        }

        public static List<long> DFS (List<int[]> blueprint, int limit) {

            //   long start_mem = GC.GetTotalMemory(true);
            //   long max_mem = 0;
            //   long end_mem = 0;
            // ....................... Initial Setting ....................................
            Tree root = new Tree (blueprint);
            root.makeBroad ();

            Console.Write ("-----------------------------------------------------");
            Console.Write ("-----------------------------------------------------");

            Stack<Tree> gameStack = new Stack<Tree> ();
            Stack<int> path = new Stack<int> ();

            //push selected node
            gameStack.Push (root);
            int maxStack = 0;
            int number_curr = 1;
            int temp_depth = -1;
            path.Clear ();
            bool found = false;
            List<long> returnDFS = new List<long> ();

            // ....................... loop through stack ....................................
            while (gameStack.Count > 0) {
                // 1. pop node from stack and save it as currentNode
                Tree currentNode = gameStack.Pop ();

                // path Stack
                if (temp_depth > currentNode.depth)
                    for (int i = 0; i < (temp_depth - currentNode.depth + 1); i++) {
                        path.Pop ();
                    }
                else if (temp_depth == currentNode.depth) {
                    path.Pop ();
                }
                path.Push (currentNode.action);

                // 2. Check if currentNode is Goal Node or not
                if (currentNode.isReachGoal ()) {

                    //currentNode == Goal Node
                    Console.Write ("\n---------------- REACH GOAL --------------\n");
                    Console.WriteLine ("Number of current node " + number_curr);
                    Console.Write ("Current Depth is " + currentNode.depth + "\n");
                    Console.Write ("Action: " + " (" + currentNode.action + ") " + currentNode.getAction () + "\n");
                    List<int> reachPath = new List<int> ();
                    Console.WriteLine ("");
                    foreach (int number in path) {
                        reachPath.Add (number);
                    }
                    for (int i = reachPath.Count - 1; i >= 0; i--) {
                        Console.WriteLine ("Action (" + reachPath[i] + ") " + actionsToString (reachPath[i]));
                    }

                    currentNode.makeBroad ();
                    currentNode.showBroad ();
                    found = true;
                    returnDFS.Add (1);

                    Console.Write ("maxStack DFS is " + maxStack + "\n");
                    //   end_mem = (max_mem - start_mem);
                    //   Console.Write("Memory is " + end_mem + " Bytes \n");

                    //end of search
                    break;

                } else {
                    // currentNode != Goal node
                    Console.WriteLine ("Number of current node " + number_curr);
                    number_curr++;

                    //Expand currentNode
                    currentNode.createNewTreeWithAvailableAction ();

                    Console.Write ("Current Depth is " + currentNode.depth + "\n");
                    Console.Write ("Action: " + " (" + currentNode.action + ") " + currentNode.getAction () + "\n");
                    currentNode.makeBroad ();
                    currentNode.showBroad ();
                    Console.WriteLine ("........................................");

                    // return root;

                    //Push currentNode's children into stack
                    if (currentNode.depth < limit) {
                        for (int i = currentNode.childList.Count - 1; i >= 0; i--) {
                            gameStack.Push (currentNode.childList[i]);
                            if (maxStack < gameStack.Count) {
                                maxStack = gameStack.Count;
                            }
                            //   long temp_mem = GC.GetTotalMemory(true);
                            //   if (max_mem < temp_mem)
                            //   {
                            //     max_mem = temp_mem;
                            //   }
                        }
                    }

                    temp_depth = currentNode.depth;

                }

            }
            if (!found) {
                Console.WriteLine ("!!! Not reach goal !!!");
                Console.Write ("maxStack DFS is " + maxStack + "\n");

                // end_mem = (max_mem - start_mem);
                returnDFS.Add (0);
            }

            returnDFS.Add (maxStack);
            return returnDFS;

        }

        public static bool IDS (List<int[]> blueprint, int limit) {
            // bool found = false;
            List<long> returnDFS = new List<long> ();
            long mem_limit = 0;

            for (int i = 1; i <= limit; i++) {
                returnDFS = DFS (blueprint, i);
                if (mem_limit < returnDFS[1]) {
                    mem_limit = returnDFS[1];
                }
                if (returnDFS[0] == 1) {
                    Console.WriteLine ("Max stack IDS is " + mem_limit);
                    return true;
                }
                Console.WriteLine ("=================== limit :" + i + " =======================");
            }
            Console.WriteLine ("!!! Not reach goal !!!");
            Console.WriteLine ("Max stack IDS is " + mem_limit);

            return false;
        }
        public static string actionsToString (int actionNumber) {
            switch (actionNumber) {
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

    }
}