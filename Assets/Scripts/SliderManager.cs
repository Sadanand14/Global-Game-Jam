using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider sliderN,sliderH,sliderT;
    private HomeBehaviour tree;
    public float levelTime = 1.0f , currentTime = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.Find("HomeTree").GetComponent<HomeBehaviour>() ;
        //if (tree) { Debug.Log("GOT IT"); }
        sliderT.maxValue = levelTime;
        sliderN.maxValue = tree.NutCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        sliderT.value = currentTime;
        sliderN.value = tree.NutCount;
        sliderH.value = tree.treeHealth;
    }
    
}
