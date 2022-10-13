using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentUtil
{
    public static T GetComponent<T>(GameObject gameObject){

        if(gameObject != null){
            return gameObject.GetComponent<T>();
        }

        return default(T);
    }
}
