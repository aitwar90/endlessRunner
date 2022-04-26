using UnityEngine;
using System.Collections.Generic;
public class WulkanicznaKulaScript : VisualBase
{
    public override void GenerujEfekt()
    {
        GenerujPunktyKuli();
    }
    public override void GenerujEfekt(Vector3 root, Vector3 targetPosition)
    {
        GenerujPunktyKuli(root, targetPosition);
    }
    private void GenerujPunktyKuli()
    {
        GenerujPunktyKuli(root, targetPositionBase);
    }
    private void GenerujPunktyKuli(Vector3 root, Vector3 targetPosition)
    {
        Vector3 temp = targetPosition - root;
        temp = temp.normalized;
        if (temp.x == 0.0) temp.x = Random.Range(-0.05f, 0.05f);
        if (temp.y == 0.0) temp.y = Random.Range(-0.05f, 0.05f);
        if (temp.z == 0.0) temp.z = Random.Range(-0.05f, 0.05f);

        NodeKula kula = null;
        if(myElementRoot == null)
        {
            myElementRoot = ManagerEfectówScript.instance.GetFromStackVisualData(0);
        }
        if(myElementRoot == null)
        {
            kula = new NodeKula();
            kula.AddToStack(HelperGenerujDaneKuli(ref targetPosition), true);
            myElementRoot = kula;
            PrzypiszVisualObiectBase();
        }
        else
        {
            if(!myElementRoot.actualUse)
            {
                kula = (NodeKula)myElementRoot;
                kula.OverrideStack(AktualizujDaneKuli(ref targetPosition));
                PrzypiszVisualObiectBase();
                return;
            }
            else
            {
                WulkanicznaKulaScript wks = new WulkanicznaKulaScript();
                wks.GenerujEfekt(root, targetPosition);
                return;
            }
        }
    }
    private Vector3[] HelperGenerujDaneKuli(ref Vector3 targetPosition)
    {
        float dist = Vector3.Distance(root, targetPosition);
        float actDist = 0.0f;
        float multiperDist = dist*0.15f;
        Vector3 interpolationPosition = root;
        List<Vector3> list = new List<Vector3>();
        list.Add(interpolationPosition);
        while(actDist < dist)
        {
            actDist += 1.25f;
            float procDist = actDist / dist;
            interpolationPosition = Vector3.Lerp(root, targetPosition, procDist);
            float tmpOffset = (procDist - 0.5f);   //Ustalam dziedzinę funkcji od -0.5 do 0.5
            interpolationPosition.y -= (tmpOffset*tmpOffset)*multiperDist;
            list.Add(interpolationPosition);
        }
        list.Add(targetPosition);
        return list.ToArray();
    }
    private Vector3[] AktualizujDaneKuli(ref Vector3 targetPosition)
    {
        return HelperGenerujDaneKuli(ref targetPosition);
    }
    private void PrzypiszVisualObiectBase()
    {
        VisualObjectBase vob = ManagerEfectówScript.instance.GetFromStackObject(0);
        if (vob == null)
        {
            GameObject go = null;
            if (ManagerEfectówScript.instance.dane.prefabKuli != null)
            {
                go = GameObject.Instantiate(ManagerEfectówScript.instance.dane.prefabKuli, root, Quaternion.identity);
            }
            else
            {
                go = new GameObject("Kula");
                vob = go.AddComponent<KulaObjectBase>();
            }
        }
        vob.MyElementRoot = myElementRoot;
        vob.InitializeMe(root.x, root.y, root.z);
        vob.ActivateMe();
        visualObjectBase = vob;
    }
}
[System.Serializable]
public class NodeKula : ElementVisual
{
    [SerializeField] public Stack<Vector3> points = null;
    public float length = 0;
    public NodeKula()
    {

    }
    public void AddToStack(float px, float py, float pz)
    {
        CheckPoints();
        points.Push(new Vector3(px, py, pz));
    }
    public void AddToStack(List<Vector3> pointsToAdd, bool reverse = true)
    {
        CheckPoints();
        for (ushort i = 0; i < pointsToAdd.Count; i++)
        {
            points.Push(pointsToAdd[i]);
        }
        if (reverse)
            ReverseStack();
    }
    public void AddToStack(Vector3[] pointsToAdd, bool reverse = true)
    {
        CheckPoints();
        for (ushort i = 0; i < pointsToAdd.Length; i++)
        {
            points.Push(pointsToAdd[i]);
        }
        if (reverse)
            ReverseStack();
    }
    public void OverrideStack(Vector3[] pointsToAdd)
    {
        points = null;
        CheckPoints();
        AddToStack(pointsToAdd, true);
    }
    private void CheckPoints()
    {
        if (points == null)
            points = new Stack<Vector3>();
    }
    public void ReverseStack()
    {
        if (points == null || points.Count == 0)
            return;
        Stack<Vector3> afterReverse = new Stack<Vector3>();
        length = 0;
        Vector3 lPosition = Vector3.zero;
        do
        {
            if (length == 0)
            {
                lPosition = points.Peek();
            }
            else
            {
                Vector3 actPos = points.Peek();
                length += Vector3.Distance(actPos, lPosition);
                lPosition = actPos;
            }
            afterReverse.Push(points.Pop());
        } while (points.Count > 0);
        points = afterReverse;
    }
    public override Vector3 GetNextPoint()
    {
        if (points == null || points.Count == 0)
        {
            return Vector3.negativeInfinity;
        }
        return points.Pop();
    }
}

