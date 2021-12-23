using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEfekty : MonoBehaviour
{
    public static BłyskawicaSkrypt bScript = null;
    public Vector3 posStart;
    public Vector3 posPlayer;
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
    
}
