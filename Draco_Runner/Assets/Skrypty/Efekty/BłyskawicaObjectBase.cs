using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BłyskawicaObjectBase : VisualObjectBase
{
    void OnDrawGizmos()
    {
        if (myElementRoot.actualUse)
        {
            ShowMeBłyskawicę();
        }
    }
    private void ShowMeBłyskawicę()
    {
        NodeBłyskawica aktBłyskawica = (NodeBłyskawica)myElementRoot;
        HelperShowBłyskawicę(aktBłyskawica);
    }
    private void HelperShowBłyskawicę(NodeBłyskawica aksCheckNode)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(aksCheckNode.sPos, aksCheckNode.ePos);
        if(aksCheckNode.odnogiBłyskawicy == null || aksCheckNode.odnogiBłyskawicy.Length == 0) return;
        for(byte i = 0; i < aksCheckNode.odnogiBłyskawicy.Length; i++)
        {
            HelperShowBłyskawicę(aksCheckNode.odnogiBłyskawicy[i]);
        }
    }
    private IEnumerator ObsługaBłyskawiy(float fTime, float sTime)
    {
        yield return new WaitForSeconds(fTime);
        //myElementRoot.ActivateMe();
        yield return new WaitForSeconds(sTime);
        //myElementRoot.DezactivateMe();
    }
}
