using UnityEngine;

public class KulaObjectBase : VisualObjectBase
{
    ///Pozycja końcowa, do której kula ma dotrzeć
    private Vector3 targetIdxPosition;
    ///Aktualna pozycja kuli
    private Vector3 actualMyPosition = Vector3.zero;
    void Update()
    {
        CheckInTarget();
    }
    private void UstawIdxPos()
    {
        targetIdxPosition = myElementRoot.GetNextPoint();
        if(targetIdxPosition == Vector3.negativeInfinity)
        {
            //Koniec, dotarłeś do celu
            DezactivateMe();
        }
    }
    private void CheckInTarget()
    {
        float t = Vector3.Distance(this.transform.position, targetIdxPosition);
        if(t < 0.25f)
        {
            UstawIdxPos();
            this.transform.rotation = Quaternion.LookRotation(targetIdxPosition);
        }
        MoveMe();
    }
    private void MoveMe()
    {
        this.transform.Translate(this.transform.forward * ManagerEfectówScript.instance.dane.prędkośćKuli * Time.deltaTime);
    }
    public override void InitializeMe(float posx, float posy, float posz)
    {
        actualMyPosition.x = posx;
        actualMyPosition.y = posy;
        actualMyPosition.z = posz;
        this.transform.position = actualMyPosition;
        this.transform.rotation = Quaternion.identity;
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
