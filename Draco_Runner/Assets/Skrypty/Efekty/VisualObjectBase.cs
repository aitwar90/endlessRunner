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
            myElementRoot = value;
        }
    }
    public abstract void ActivateMe();
    public abstract void ResetMe();
    protected abstract void DezactivateMe();
}
