using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGry : MonoBehaviour
{
    public static ManagerGry managerGry = null;
    // Start is called before the first frame update
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
