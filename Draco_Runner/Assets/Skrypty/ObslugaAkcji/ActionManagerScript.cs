using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerScript : MonoBehaviour
{
    public static DataOfAction dataOfAction = null;
    void Awake()
    {
        if(dataOfAction == null)
        {
            dataOfAction = new DataOfAction();
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        for(byte i = 0; i < 6; i++)
        {
            StartCoroutine(WaitTimeTime(i));
        }
    }
    private IEnumerator WaitTimeTime(byte idxOfAction)
    {
        float time = 0.0f;
        while (true)
        {
            time = GetTimerByIdxOfAction(idxOfAction);
            yield return new WaitForSeconds(time);
            dataOfAction.ExeciuteTypeOfMethod(idxOfAction);
        }
    }
    private float GetTimerByIdxOfAction(byte idxOfAction)
    {
        switch(idxOfAction)
        {
            case 0: //DeltaTime
            return Time.deltaTime;
            case 1: //Unscaled Delta Time
            return Time.unscaledDeltaTime;
            case 2: //DeltaTime
            return Time.deltaTime*2.0f;
            case 3: //Unscaled Delta Time
            return Time.unscaledDeltaTime*2.0f;
            case 4: //DeltaTime
            return Time.deltaTime*5.0f;
            case 5: //Unscaled Delta Time
            return Time.unscaledDeltaTime*5.0f;
            
        }
        return -1;
    }
}
