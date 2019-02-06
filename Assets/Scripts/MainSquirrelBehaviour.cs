using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSquirrelBehaviour : MonoBehaviour
{
    public int nutCapacity=5, nutCount=2;
    private bool stealingAlready = false, inStealingRange = false, nearHome = false;
    public float stealTime = 1.0f, timer = 0.0f, depositmultiplier = 0.5f;
    private GameObject Home;
    private OtherTreeBehaviour otherTree;

     void Start()
    {
        
    }
    
    void Update()
    {
        if (inStealingRange && (!stealingAlready)&&(nutCount<nutCapacity))
        {
            bool xx = CheckNuts();
            if (xx)
            {
                stealingAlready = true;
                StealNuts();
            }
        }

        if (nearHome)
        {
            DepositNuts();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HomeBehaviour>())
        {
            nearHome = true;
            Home = other.gameObject;
        }
        else
        {
            otherTree = other.GetComponent<OtherTreeBehaviour>();
            inStealingRange = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HomeBehaviour>())
        {
            nearHome = false;
        }
        else
        {
            inStealingRange = false;
        }
    }

    private void StealNuts()
    {
        timer += Time.deltaTime;
        if (timer >= stealTime)
        {
            stealingAlready = false;
            otherTree.NutCount--;
            nutCount++;
        } 
    }

    private void DepositNuts()
    {
        HomeBehaviour var = Home.GetComponent<HomeBehaviour>();
        var.NutCount += nutCount;
        nutCount = 0;
    }

    public void DropNuts(int x)
    {
        nearHome = false;
    }

    private bool CheckNuts()
    {
        if (otherTree.NutCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
