using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDźwięku : MonoBehaviour
{
    public static ManagerDźwięku managerDźwięku = null;

    void Awake()
    {
        if(managerDźwięku == null)
        {
            managerDźwięku = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
