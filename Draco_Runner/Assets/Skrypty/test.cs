using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public bool odwróć = false;
    public int maxVal = 100000;
    public int pTestowe = 1000;
    void Start()
    {
        Stopwatch timer = new Stopwatch();

        int a = 0;
        long milisecipp = 0;
        long milisecppi = 0;
        for (ushort j = 0; j < pTestowe; j++)
        {
            if (odwróć)
            {
                timer.Start();
                for (int i = 0; i < maxVal; ++i)
                {
                    a = i;
                }
                timer.Stop();
                milisecppi += timer.ElapsedMilliseconds;
                timer.Reset();
                timer.Start();
                for (int i = 0; i < maxVal; i++)
                {
                    a = i;
                }
                timer.Stop();
                milisecipp += timer.ElapsedMilliseconds;
            }
            else
            {
                timer.Start();
                for (int i = 0; i < maxVal; i++)
                {
                    a = i;
                }
                timer.Stop();
                milisecipp += timer.ElapsedMilliseconds;
                timer.Reset();
                timer.Start();
                for (int i = 0; i < maxVal; ++i)
                {
                    a = i;
                }
                timer.Stop();
                milisecppi += timer.ElapsedMilliseconds;
                timer.Reset();
            }
        }
        UnityEngine.Debug.Log("śr IPP = " + milisecipp / pTestowe);
        UnityEngine.Debug.Log("śr PPI = " + milisecppi / pTestowe);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
