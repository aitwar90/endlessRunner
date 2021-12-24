using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEfekty : MonoBehaviour
{
    public static ManagerEfekty instance = null;
    public BłyskawicaSkrypt bScript = null;
    public Vector3 posStart;
    private bool wygenerowane = true;
    public Vector3 posPlayer;
    private byte idx = 0;
    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
        bScript = new BłyskawicaSkrypt();
    }
    // Start is called before the first frame update
    void Start()
    {
        bScript.GenerujBłyskawicę(posStart, posPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Visualize effects
    public void StartCorouiteStorms(byte idx)
    {
        StartCoroutine(ObsługaBłyskawiy(idx, 1.25f, 10.75f));
    }
    private IEnumerator ObsługaBłyskawiy(byte idx, float fTime, float sTime)
    {
        yield return new WaitForSeconds(fTime);
        ActivateMe(idx);
        yield return new WaitForSeconds(sTime);
        DesactivateMe(idx);
    }
    private void ActivateMe(byte idx)
    {
        bScript.błyskawiceRoot[idx].actualUse = true;
    }
    private void DesactivateMe(byte idx)
    {
        bScript.błyskawiceRoot[idx].actualUse = false;
    }
    void OnDrawGizmos()
    {
        if(bScript != null)
        {
            for(byte i = 0; i < bScript.błyskawiceRoot.Length; i++)
            {
                if(bScript.błyskawiceRoot[i].actualUse)
                {
                    ShowMeBłyskawicę(i);
                }
            }
        }
    }
    private void ShowMeBłyskawicę(byte i)
    {
        NodeBłyskawica aktBłyskawica = bScript.błyskawiceRoot[i];
        if(!wygenerowane)
        {
            GameObject go = new GameObject(idx.ToString());
            idx++;
            go.transform.position = aktBłyskawica.sPos;
        }
        HelperShowBłyskawicę(aktBłyskawica);
        wygenerowane = true;
    }
    private void HelperShowBłyskawicę(NodeBłyskawica aksCheckNode)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(aksCheckNode.sPos, aksCheckNode.ePos);
        if(!wygenerowane)
        {
            GameObject go = new GameObject(idx.ToString());
            idx++;
            go.transform.position = aksCheckNode.sPos;
        }
        if(aksCheckNode.odnogiBłyskawicy == null || aksCheckNode.odnogiBłyskawicy.Length == 0) return;
        for(byte i = 0; i < aksCheckNode.odnogiBłyskawicy.Length; i++)
        {
            HelperShowBłyskawicę(aksCheckNode.odnogiBłyskawicy[i]);
        }
    }
    public void GenerateAnotherOne()
    {
        posStart = posStart + Vector3.right;
        posPlayer = posPlayer + Vector3.right;
        if(bScript == null) bScript = new BłyskawicaSkrypt();
        bScript.GenerujBłyskawicę(posStart, posPlayer);
    }
    public void KasujBłyskawice()
    {
        posStart = new Vector3(6.5f, 2.0f, 60f);
        posPlayer = new Vector3(6.6f, 3.0f, 61f);
        if(bScript != null)
        {
            bScript.błyskawiceRoot = null;
            bScript = null;
        }
    }
    #endregion
}
