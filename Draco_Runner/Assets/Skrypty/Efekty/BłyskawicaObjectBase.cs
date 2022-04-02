using System.Collections;
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
    public override void InitializeMe(float posx, float posy, float posz)
    {
        this.transform.position = new Vector3(posx, posy, posz);
        this.transform.rotation = Quaternion.identity;
    }
    public override void ActivateMe()
    {
        StartCoroutine(ObsługaBłyskawiy(1.25f, 10.75f));
    }
    protected override void DezactivateMe()
    {
        myElementRoot.actualUse = false;
        ManagerEfectówScript.instance.AddToStackVisualData(myElementRoot, 1);
        ManagerEfectówScript.instance.AddToStackObject(this, 1);
    }
    public override void ResetMe()
    {
        StopCoroutine("ObsługaBłyskawiy");
        myElementRoot.actualUse = false;
    }
    private IEnumerator ObsługaBłyskawiy(float fTime, float sTime)
    {
        yield return new WaitForSeconds(fTime);
        myElementRoot.actualUse = true;
        yield return new WaitForSeconds(sTime);
        DezactivateMe();
    }
}
