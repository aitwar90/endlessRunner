using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulaObjectBase : VisualObjectBase
{
    private Vector3 actIdxPosition;
    private Vector3 lastIdxPosition;
    private void UstawIdxPos()
    {
        lastIdxPosition = actIdxPosition;
        actIdxPosition = myElementRoot.GetNextPoint();
        if(actIdxPosition == Vector3.negativeInfinity)
        {
            //Koniec, dotarłeś do celu
            DezactivateMe();
        }
    }
    public override void ActivateMe()
    {
        myElementRoot.actualUse = true;
    }
    protected override void DezactivateMe()
    {
        myElementRoot.actualUse = false;
        ManagerEfectówScript.instance.AddToStackVisualData(myElementRoot, 1);
        ManagerEfectówScript.instance.AddToStackObject(this, 1);
    }
    public override void ResetMe()
    {
        //StopCoroutine("ObsługaBłyskawiy");
        myElementRoot.actualUse = false;
    }
}
