using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMainUI : UI_Menu_Abs
{
    public static ManagerMainUI managerMainUI = null;
    void Awake()
    {
        if (managerMainUI == null)
            managerMainUI = this;
        else
            Destroy(this);
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
