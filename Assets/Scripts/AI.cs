using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    int X1, Y1, X2, Y2,X,Y;
    private PathFinder pathFinder;
    private bool findPath = false, startMoving = false;

    // Start is called before the first frame update
    private void Awake()
    {
        pathFinder = GameObject.FindObjectOfType<PathFinder>();
    }
    void Start()
    {
        X1 = 15;
        Y1 = 7;
        X = X1;
        Y = Y1;
        X2 = -12;
        Y2 = 8;
        findPath = true;
        //pathFinder.BellmanFordAlgo(X1, Y1, X2, Y2);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pathFinder.isReady);
        if (pathFinder.isReady)
        {
            if (findPath)
            {
                TracePath();
                findPath = false;
            }
        }
        transform.position = new Vector3(X, Y, 0);
    }

    public void TracePath()
    {
        Stack<PathFinder.Node> path = new Stack<PathFinder.Node>();
        path = pathFinder.BellmanFordAlgo(X1, Y1, X2, Y2);
        //StartCoroutine(MyCoroutine(path));
    }

    IEnumerator MyCoroutine(Stack<PathFinder.Node> path)
    {
        while (path.Count != 0)
        {
            yield return new WaitForSeconds(0.5f);
            X = path.Peek().X;
            Y = path.Peek().Y;
            path.Pop();
        }
    }
}
