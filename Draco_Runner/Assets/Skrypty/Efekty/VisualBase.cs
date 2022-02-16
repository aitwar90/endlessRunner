using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualBase
{
    [SerializeField]public NodeBłyskawica[] błyskawiceRoot;
    public Vector3 targetPositionBase;
    public Vector3 root;
    public void UpdatePlayerPosition(float x, float y, float z)
    {
        targetPositionBase.x = x;
        targetPositionBase.y = y;
        targetPositionBase.z = z;
    }
    public virtual void GenerujBłyskawicę()
    {

    }
    public virtual void GenerujBłyskawicę(Vector3 root, Vector3 targetPosition)
    {

    }
    public virtual void GenerujKulęLawy()
    {

    }
}
