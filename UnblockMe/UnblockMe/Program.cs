using System;
using System.Collections.Generic;

namespace UnblockMe
{
  class Program
  {
    static void Main(string[] args)
    {

      List<int[]> blueprint = new List<int[]> {
        new int[] {-1, 2, 0, 1, 2 },
        // new int[]{1,0,2,1,3},
        // new int[]{2,0,5,0,3},
        // new int[]{4,2,2,0,3},
        // new int[]{5,3,4,1,2},
        // new int[]{6,4,0,0,2},
        // new int[]{8,4,4,0,2},
        // new int[]{7,5,1,1,3}
      };
      List<int[]> EasyBlueprint = new List<int[]> {
        new int[] {-1, 3, 1, 1, 2 },
        new int[] { 1, 1, 4, 1, 2 },
        new int[] { 2, 1, 1, 0, 2 },
        new int[] { 3, 2, 4, 1, 3 },
        new int[] { 4, 2, 3, 0, 2 },
        new int[] { 5, 4, 1, 1, 2 },
        new int[] { 6, 5, 1, 0, 2 },
        new int[] { 7, 4, 3, 1, 2 },
        new int[] { 8, 5, 4, 0, 2 },
        new int[] { 9, 6, 2, 1, 2 },
        new int[] { 10, 3, 5, 0, 3 },
        new int[] { 12, 3, 6, 0, 2 },
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

      var watch = new System.Diagnostics.Stopwatch();
      long dfs_time = 0;
      long ids_time = 0;
      long compare_time = 0;



      Console.Write("------------------ DEPTH FIRST WITH LIMIT SEARCH --------------------");
      watch.Start();
      long dfs_mem_start = GC.GetTotalMemory(true);
      DFS(EasyBlueprint, 3);
      long dfs_mem_end = GC.GetTotalMemory(true);
      long dfs_mem = (dfs_mem_end - dfs_mem_start) / 1024;
      watch.Stop();
      Console.WriteLine("----------------------- END -------------------------");
      dfs_time = watch.ElapsedMilliseconds / 1000;
      Console.WriteLine("Execution Time: " + dfs_time + " s");


      Console.Write("------------------ ITERATIVE DEPTH FIRST SEARCH --------------------");
      if (!watch.IsRunning)
      {
        watch.Restart();
      }
      long ids_mem_start = GC.GetTotalMemory(true);
      IDS(EasyBlueprint);
      long ids_mem_end = GC.GetTotalMemory(true);
      long ids_mem = (ids_mem_end - ids_mem_start) / 1024;
      watch.Stop();
      Console.WriteLine("----------------------- END -------------------------");
      ids_time = watch.ElapsedMilliseconds / 1000;
      Console.WriteLine("Execution Time: " + ids_time + " s");

      Console.WriteLine("\n\n===============================================================================================================\n\n");
      Console.WriteLine("                      Depth First Search with Limit     |     Iterative Depth First Search     ");
      Console.WriteLine("Execution Time:                   " + dfs_time + " sec                                " + ids_time + " sec");
      Console.WriteLine("Memory Usage (GC):                " + dfs_mem + " KB                              " + ids_mem + " KB");
      compare_time = dfs_time / ids_time;
      Console.WriteLine("\n\nConclusion: IDS is faster than DFS by " + compare_time + "X");


    }

    public static Tree DFS(List<int[]> blueprint, int limit)
    {

      // ....................... Initial Setting ....................................
      Tree root = new Tree(blueprint);
      root.makeBroad();

      Console.Write("-----------------------------------------------------");
      Console.Write("-----------------------------------------------------");

      Stack<Tree> gameStack = new Stack<Tree>();
      Stack<int> path = new Stack<int>();

      //push selected node
      gameStack.Push(root);
      int number_curr = 1;
      int temp_depth = -1;
      path.Clear();
      bool found = false;

      // ....................... loop through stack ....................................
      while (gameStack.Count > 0)
      {

        // 1. pop node from stack and save it as currentNode
        Tree currentNode = gameStack.Pop();

        // path Stack
        if (temp_depth > currentNode.depth)
          for (int i = 0; i < (temp_depth - currentNode.depth + 1); i++)
          {
            path.Pop();
          }
        else if (temp_depth == currentNode.depth)
        {
          path.Pop();
        }
        path.Push(currentNode.action);

        // 2. Check if currentNode is Goal Node or not
        if (currentNode.isReachGoal())
        {

          //currentNode == Goal Node
          Console.Write("\n---------------- REACH GOAL --------------\n");
          Console.WriteLine("Number of current node " + number_curr);
          Console.Write("Current Depth is " + currentNode.depth + "\n");
          Console.Write("Action: " + " (" + currentNode.action + ") " + currentNode.getAction() + "\n");
          List<int> reachPath = new List<int>();
          Console.WriteLine("");
          foreach (int number in path)
          {
            reachPath.Add(number);
          }
          for (int i = reachPath.Count - 1; i >= 0; i--)
          {
            Console.WriteLine("Action (" + reachPath[i] + ") " + actionsToString(reachPath[i]));
          }

          currentNode.makeBroad();
          currentNode.showBroad();
          found = true;
          //end of search
          break;

        }
        else
        {
          // currentNode != Goal node
          Console.WriteLine("Number of current node " + number_curr);
          number_curr++;

          //Expand currentNode
          currentNode.createNewTreeWithAvailableAction();

          Console.Write("Current Depth is " + currentNode.depth + "\n");
          Console.Write("Action: " + " (" + currentNode.action + ") " + currentNode.getAction() + "\n");
          currentNode.makeBroad();
          currentNode.showBroad();
          Console.WriteLine("........................................");
          return root;

          //Push currentNode's children into stack
          if (currentNode.depth < limit)
          {
            for (int i = currentNode.childList.Count - 1; i >= 0; i--)
            {
              gameStack.Push(currentNode.childList[i]);
            }
          }

          temp_depth = currentNode.depth;

        }

      }
      if (!found)
      {
        Console.WriteLine("!!! Not reach goal !!!");
      }
      return root;

    }

    public static Tree IDS(List<int[]> blueprint)
    {
      Tree root = new Tree(blueprint);
      root.makeBroad();
      //root.showBroad();
      Console.Write("-----------------------------------------------------");
      Console.Write("-----------------------------------------------------");
      Stack<Tree> gameStack = new Stack<Tree>();
      Stack<int> path = new Stack<int>();
      bool found = false;
      int limit = 1;

      while (!found && limit != 10)
      {
        //push selected node
        gameStack.Push(root);
        int number_curr = 1;
        int temp_depth = -1;
        path.Clear();
        while (gameStack.Count > 0)
        {

          // 1. check if that node is The goal node
          Tree currentNode = gameStack.Pop();

          // path Stack
          if (temp_depth > currentNode.depth)
            for (int i = 0; i < (temp_depth - currentNode.depth + 1); i++)
            {
              path.Pop();
            }
          else if (temp_depth == currentNode.depth)
          {
            path.Pop();
          }
          path.Push(currentNode.action);

          if (currentNode.isReachGoal())
          {
            Console.Write("\n---------------- REACH GOAL --------------\n");
            Console.WriteLine("Number of current node " + number_curr);
            Console.Write("Current Depth is " + currentNode.depth + "\n");
            Console.Write("Action: " + " (" + currentNode.action + ") " + currentNode.getAction() + "\n");
            Console.WriteLine("\nPath to Goal node are .... ");

            List<int> reachPath = new List<int>();
            foreach (int number in path)
            {
              reachPath.Add(number);
            }
            for (int i = reachPath.Count - 1; i >= 0; i--)
            {
              Console.WriteLine("Action (" + reachPath[i] + ") " + actionsToString(reachPath[i]));
            }
            Console.WriteLine("\n");
            currentNode.makeBroad();
            currentNode.showBroad();
            found = true;
            // end of search
            break;

          }
          else
          {
            // if currentNode is not Goal node
            // expand currentNode
            Console.Write("limit: " + limit + "\n");
            Console.WriteLine("Number of current node " + number_curr);
            number_curr++;

            currentNode.createNewTreeWithAvailableAction();
            Console.Write("Current Depth is " + currentNode.depth + "\n");
            Console.Write("Action: " + " (" + currentNode.action + ") " + currentNode.getAction() + "\n");

            currentNode.makeBroad();
            currentNode.showBroad();
            Console.WriteLine("........................................");
            if (currentNode.depth < limit)
            {
              for (int i = currentNode.childList.Count - 1; i >= 0; i--)
              {
                //child.makeBroad();
                //child.showBroad();
                gameStack.Push(currentNode.childList[i]);
              }
            }
            temp_depth = currentNode.depth;

          }
        }
        if (limit == 10)
        {
          break;
        }
        if (!found)
        {
          limit++;
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

      }

      return root;
    }

    public static Tree Depth_Limited(List<int[]> blueprint, int depth)
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
        if (temp.depth >= depth)
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

    public static string actionsToString(int actionNumber)
    {
      switch (actionNumber)
      {
        case 1:
          return "red car move Right";
        case 2:
          return "red car  move Left";
        case 3:
          return "car 1 move Right";
        case 4:
          return "car 1  move Left";
        case 5:
          return "car 2 move Down";
        case 6:
          return "car 2 move Up";
        case 7:
          return "car 3 move Right";
        case 8:
          return "car 3  move Left";
        case 9:
          return "car 4 move Down";
        case 10:
          return "car 4 move Up";
        case 11:
          return "car 5 move Right";
        case 12:
          return "car 5  move Left";
        case 13:
          return "car 6 move Down";
        case 14:
          return "car 6 move Up";
        case 15:
          return "car 7 move Right";
        case 16:
          return "car 7  move Left";
        case 17:
          return "car 8 move Down";
        case 18:
          return "car 8 move Up";
        case 19:
          return "car 9 move Right";
        case 20:
          return "car 9  move Left";
        case 21:
          return "car 10 move Down";
        case 22:
          return "car 10 move Up";
        case 23:
          return "car 12 move Down";
        case 24:
          return "car 12 move Up";
        default:
          return "";
      }


    }

  }
}