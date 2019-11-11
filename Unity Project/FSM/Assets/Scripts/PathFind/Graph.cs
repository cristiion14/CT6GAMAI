using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    List<GraphNode> nodes;  //list of the existing nodes in the graph


	void DFS(int start, int end)
    {
        List<int> route = new List<int>(nodes.Count);   //a list of possible route between nodes

        List<bool> hasVisited = new List<bool>(nodes.Count);    //keeps track which node was visited

        Stack<GraphEdge> edges = new Stack<GraphEdge>();    //stack containing the graph edges
        
        hasVisited[start] = true;
        //add all the adjacent edges of the source node to the stack and mark the source node as visited
        for(int i = start; i<nodes[start].adjacenyLsit.Count; i++)
        {
            edges.Push(nodes[start].adjacenyLsit[i]);
        }

        while(edges.Count != 0)
        {
            GraphEdge graphEdges = edges.Pop(); //contains adjacency edge betheen nodes
            route[graphEdges.to] = graphEdges.from; //
            hasVisited[graphEdges.to] = true;
            if(graphEdges.to==start)
            {
                Debug.Log("Found node at: "+graphEdges.to);
                return;
            }
            else
            {

            }
        }
                            

    }
}
