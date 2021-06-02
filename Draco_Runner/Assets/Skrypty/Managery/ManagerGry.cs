using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGry : MonoBehaviour
{
    public static ManagerGry managerGry = null;
    
    void Awake()
    {
        if(managerGry == null)
        {
            managerGry = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
