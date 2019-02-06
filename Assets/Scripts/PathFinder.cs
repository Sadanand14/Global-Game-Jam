using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private GameObject test;
    private TestingObstacles testScript;
    private int a, b;
    private float time = 0.0f;
    private bool testPhase = true;
    public bool isReady = false;
    public class Node
    {
        public int X,Y;
        public bool isUsable;
        public Node(int a, int b)
        {
            X = a;
            Y = b;
            isUsable = true;
        }
    }

    private class Edge
    {
        public Node from, to;
        public float weight;
        public Edge(Node a, Node b,float w)
        {
            weight = w;
            from = a;
            to = b;
        }
    }

    private class Graphs
    {
        public List<Node> nodelist;    
        public List<Edge> edgelist;

    }

    private Graphs graph;

    private class NodeValue
    {
        public Node prevNode;
        public float Cost;

        public NodeValue(Node prevnode, float cost)
        {
            prevNode = prevnode;
            Cost = cost;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        graph = new Graphs();
        CreateNodes();
        testPhase = true;
        a = -12;
        b = -7;
        test = GameObject.Find("Test");
        testScript = test.GetComponent<TestingObstacles>();
    }
    
    void Update()
    {
        
        if (testPhase)
        {
            time += Time.deltaTime;
            test.transform.position = new Vector3(a,b,0);
            DetectingObstacles(a,b);

            if (b < 8)
            {
                b++;
            }
            else if (b == 8 && a < 15)
            {
                a++;
                b = -7;
            }
            else if(a==15&&b==8)
            {
                testPhase = false;
                CreateEdges();
                isReady = true;
                Debug.Log("Object Detection Complete at " + (float)time);
            }
            
        }
    }

    private void DetectingObstacles(int a,int b)
    {
        if (testScript.triggerCount>0)
        {
            foreach (Node node in graph.nodelist)
            {
                if((node.X==a)&&(node.Y==b))
                {
                    node.isUsable = false;
                }
            }
        }
    }

    private void CreateNodes()
    {
        graph.nodelist = new List<Node>();
       
        for (int i=-12; i< 16; i++)
        {
            for(int j=-7; j <9 ;j++)
            {
                graph.nodelist.Add(new Node(i, j));
            }
        }
    }

    private void CreateEdges()
    {
        graph.edgelist = new List<Edge>();

        for (int i = 0; i < graph.nodelist.Count; i++)
        {
            if (graph.nodelist[i].isUsable)
            {
                for (int j = 0; (j != i) && (j < graph.nodelist.Count); j++)
                {
                    if (graph.nodelist[j].isUsable)
                    {
                        bool edgeExists = false;
                        foreach (Edge edge in graph.edgelist)
                        {
                            if ((edge.from == graph.nodelist[i] || edge.from == graph.nodelist[j]) && (edge.to == graph.nodelist[j] || edge.to == graph.nodelist[i]))
                            {
                                edgeExists = true;
                                //Debug.Log("GRAPH EXISTS!!");
                            }
                        }

                        if (!edgeExists)
                        {
                            if ((Mathf.Abs(graph.nodelist[i].X - graph.nodelist[j].X) < 2) && (Mathf.Abs(graph.nodelist[i].Y - graph.nodelist[j].Y) < 2))
                            {
                                if ((Mathf.Abs(graph.nodelist[i].X - graph.nodelist[j].X) + Mathf.Abs(graph.nodelist[i].Y - graph.nodelist[j].Y)) == 2)
                                {
                                    graph.edgelist.Add(new Edge(graph.nodelist[i], graph.nodelist[j], 1.5f));
                                    graph.edgelist.Add(new Edge(graph.nodelist[j], graph.nodelist[i], 1.5f));
                                }
                                else
                                {
                                    graph.edgelist.Add(new Edge(graph.nodelist[i], graph.nodelist[j], 1f));
                                    graph.edgelist.Add(new Edge(graph.nodelist[j], graph.nodelist[i], 1f));
                                }
                            }
                        }
                    }
                }
            }
        }

        //isReady = true;
    }

    public Stack<Node> BellmanFordAlgo(int x1, int y1, int x2, int y2)
    {
        Dictionary<Node, NodeValue> Distance = new Dictionary<Node, NodeValue>();
        for (int i = 0; i < graph.nodelist.Count; i++)
        {
            NodeValue nodeValue = new NodeValue(graph.nodelist[i], float.MaxValue);
            Distance.Add(graph.nodelist[i], nodeValue);
        }

        Node sourceNode = graph.nodelist[0],endNode=graph.nodelist[0];
        foreach (Node node in graph.nodelist)
        {
            if ((node.X == x1) && (node.Y == y1))
            {
                sourceNode = node;
                break;
            }
        }

        Distance[sourceNode].Cost = 0;

        for (int i = 0; i < graph.nodelist.Count; i++)
        {
            for (int j = 0; j < graph.edgelist.Count; j++)
            {
                Node startNode = graph.edgelist[j].from;
                Node endtNode = graph.edgelist[j].to;
                float w = graph.edgelist[j].weight;

                if (Distance[startNode].Cost != float.MaxValue && ((Distance[startNode].Cost + w) < Distance[endtNode].Cost))
                {
                    Distance[endtNode].Cost = Distance[startNode].Cost + w;
                    Distance[endtNode].prevNode = startNode;
                }
            }
        }

        foreach (Node node in graph.nodelist)
        {
            if ((node.X == x2) && (node.Y == y2))
            {
                endNode = node;
                break;
            }
        }
        Debug.Log(endNode.X +"  "+ endNode.Y);
        Stack<Node> path = new Stack<Node>();
        //path.Push(endNode);
        while (Distance[path.Peek()].prevNode != sourceNode)
        {
            path.Push(Distance[path.Peek()].prevNode);
            Debug.Log("LOL");
        }

        return path;
    }
}
