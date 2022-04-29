using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualObjectBase : MonoBehaviour
{
    protected ElementVisual myElementRoot = null;
    public ElementVisual MyElementRoot
    {
        set
        {
            if(myElementRoot == null) myElementRoot = new ElementVisual();
            myElementRoot = value;
        }
    }
    public abstract void InitializeMe(float posx, float posy, float posz);
    public abstract void ActivateMe();
    public abstract void ResetMe();
    protected abstract void DezactivateMe();
}
