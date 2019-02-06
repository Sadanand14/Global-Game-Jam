using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement 
{


   public static void Move(Transform transform,Vector2 direction,float speed)
    { 
        transform.Translate(direction * speed * Time.deltaTime);
    }

 
}
