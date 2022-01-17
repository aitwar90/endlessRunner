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
            Dane.ŁadujDane(1);
        }
        else
        {
            Destroy(this);
        }
    }
    protected override void Start()
    {
        base.Start();
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 0;
        ManagerGry.managerGry.ZaładujIWyładujSceny(new byte[] {2}, new byte[] {3});
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
