using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualObjectBase : MonoBehaviour
{
    protected ElementVisual myElementRoot = null;
    public ElementVisual MyElementRoot
    {
        set
        {
            myElementRoot = value;
        }
    }
}
