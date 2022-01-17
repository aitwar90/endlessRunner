using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : UI_Menu_Abs
{
    public static ManagerUI managerUI = null;
    void Awake()
    {
        if(managerUI == null)
        {
            managerUI = this;
        }
        else
        {
            Destroy(this);
        }
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    /*
    ///<summary>Metoda ustawia jezyk dla UI. Kolejne elementy rozdzielone ; a języki | (nazwaObjektu|Angielski|Polski|Rosyjski|Ukraiński;)</summary>
    ///<param name="ustIdx">Indeks języka na który ma zostać ustawiony UI.</param>
    public override void UstawJęzyk(byte ustIdx = 0)
    {
        base.UstawJęzyk(ustIdx);
    }
    */
}
