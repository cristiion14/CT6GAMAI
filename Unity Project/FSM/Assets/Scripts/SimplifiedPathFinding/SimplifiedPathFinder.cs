using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SimplifiedPathFinder : MonoBehaviour
{
    SimplifiedGrid GridReference;//For referencing the grid class
    public Transform StartPosition;//Starting position to pathfind from
    public Transform TargetPosition;//Starting position to pathfind to
    public GameObject veorica;
    private void Awake()//When the program starts
    {
        GridReference = GetComponent<SimplifiedGrid>();//Get a reference to the game manager
        veorica = GameObject.Find("Veorica");
      //  veoricaRef = GetComponent<Veorica>();
    }

    private void Update()//Every frame
    {
    //    FindPath(StartPosition.position, TargetPosition.position);//Find a path to the goal
    }

    public  void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        //Vector3[] direction = new Vector3[0];
        SimplifiedNode StartNode = GridReference.NodeFromWorldPoint(a_StartPos);//Gets the node closest to the starting position
        SimplifiedNode TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);//Gets the node closest to the target position

        List<SimplifiedNode> OpenList = new List<SimplifiedNode>();//List of nodes for the open list
        HashSet<SimplifiedNode> ClosedList = new HashSet<SimplifiedNode>();//Hashset of nodes for the closed list

        OpenList.Add(StartNode);//Add the starting node to the open list to begin the program

        while (OpenList.Count > 0)//Whilst there is something in the open list
        {
            SimplifiedNode CurrentNode = OpenList[0];//Create a node and set it to the first item in the open list
            for (int i = 1; i < OpenList.Count; i++)//Loop through the open list starting from the second object
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    CurrentNode = OpenList[i];//Set the current node to that object
                }
            }
            OpenList.Remove(CurrentNode);//Remove that from the open list
            ClosedList.Add(CurrentNode);//And add it to the closed list

            if (CurrentNode == TargetNode)//If the current node is the same as the target node
            {
                  GetFinalPath(StartNode, TargetNode);//Calculate the final path
            //    veorica.GetComponent<Veorica>().direction = RetracePath(StartNode, TargetNode);
                
               
            }

            foreach (SimplifiedNode NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))//Loop through each neighbor of the current node
            {
                if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))//If the neighbor is a wall or has already been checked
                {
                    continue;//Skip it
                }
                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);//Get the F cost of that neighbor

                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))//If the f cost is greater than the g cost or it is not in the open list
                {
                    NeighborNode.igCost = MoveCost;//Set the g cost to the f cost
                    NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode);//Set the h cost
                    NeighborNode.ParentNode = CurrentNode;//Set the parent of the node for retracing steps

                    if (!OpenList.Contains(NeighborNode))//If the neighbor is not in the openlist
                    {
                        OpenList.Add(NeighborNode);//Add it to the list
                    }
                    else
                    {
                      //  ClosedList.Add(NeighborNode);
                    }
                    
                }
            }

        }
    }

    public Vector3[] RetracePath(SimplifiedNode startNode, SimplifiedNode endNode)
    {
        List<SimplifiedNode> path = new List<SimplifiedNode>();
        SimplifiedNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

  public  Vector3[] SimplifyPath(List<SimplifiedNode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].iGridX - path[i].iGridX, path[i - 1].iGridY - path[i].iGridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i-1].vPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }


    void GetFinalPath(SimplifiedNode a_StartingNode, SimplifiedNode a_EndNode)
    {
        List<SimplifiedNode> FinalPath = new List<SimplifiedNode>();//List to hold the path sequentially 
        SimplifiedNode CurrentNode = a_EndNode;//Node to store the current node being checked

        while (CurrentNode != a_StartingNode)//While loop to work through each node going through the parents to the beginning of the path
        {
            FinalPath.Add(CurrentNode);//Add that node to the final path
            CurrentNode = CurrentNode.ParentNode;//Move onto its parent node
        }

        FinalPath.Reverse();//Reverse the path to get the correct order

        veorica.GetComponent<Veorica>().vFinalPath = FinalPath;//Set the final path
          
    }


    int GetManhattenDistance(SimplifiedNode a_nodeA, SimplifiedNode a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);//y1-y2

        return ix + iy;//Return the sum
    }
}
