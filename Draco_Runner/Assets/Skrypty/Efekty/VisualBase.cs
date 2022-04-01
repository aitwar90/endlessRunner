using UnityEngine;

public abstract class VisualBase
{
    public ElementVisual myElementRoot = null;
    public Vector3 targetPositionBase;
    public Vector3 root;
    protected VisualObjectBase visualObjectBase = null;
    public void SetTargetPosition(float x, float y, float z)
    {
        targetPositionBase.x = x;
        targetPositionBase.y = y;
        targetPositionBase.z = z;
    }
    public void SetRootPosition(float x, float y, float z)
    {
        root.x = x;
        root.y = y;
        root.z = z;
    }
    public abstract void GenerujEfekt();
    public abstract void GenerujEfekt(Vector3 root, Vector3 targetPosition);
}
public class ElementVisual
{
    public bool actualUse = false;
    public Vector3 sPos;
}
