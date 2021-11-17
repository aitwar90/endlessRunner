using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerBaseScript
{
    protected delegate void methodToExeciuteNoneParametr();
    protected IEnumerator WaitRealTime(float time, sbyte typeActionRoExeciute = -1)
    {
        yield return new WaitForSecondsRealtime(time);
        
    }
}
