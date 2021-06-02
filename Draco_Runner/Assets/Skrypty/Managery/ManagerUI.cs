using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI managerUI = null;

    void Awake()
    {
        if(managerUI == null)
        {
            managerUI = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
