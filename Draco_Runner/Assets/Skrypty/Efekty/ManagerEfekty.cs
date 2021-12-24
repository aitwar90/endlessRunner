using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEfekty : MonoBehaviour
{
    public static BłyskawicaSkrypt bScript = null;
    public Vector3 posStart;
    private bool wygenerowane = true;
    public Vector3 posPlayer;
    private byte idx = 0;
    void Awake()
    {
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
    #endregion
}
