using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingObstacles : MonoBehaviour
{
    public int triggerCount = 0;

    private void Start()
    {
        triggerCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerCount++;
        //Debug.Log("Obstacle Found :  "  + triggerCount);
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerCount > 0)
        {
            triggerCount--;
            //Debug.Log("Obstacle Lost :  " + triggerCount);
        }

    }
}
