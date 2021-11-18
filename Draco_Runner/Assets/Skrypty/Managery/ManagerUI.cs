using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : UI_Menu_Abs
{
    public static ManagerUI managerUI = null;
    public Slider sliderMuzyki;
    public Slider sliderDźwięku;
    
    public float UstawGłośnośćMuzyki
    {
        get
        {
            return Dane.poziomMuzyki;
        }
        set
        {
            Dane.poziomMuzyki = value;
            if(sliderMuzyki != null && value != sliderMuzyki.value)
            {
                sliderMuzyki.value = value;
            }
        }
    }
    public float UstawGłośnośćDźwięku
    {
        get
        {
            return Dane.poziomDźwięku;
        }
        set
        {
            Dane.poziomDźwięku = value;
            if(sliderDźwięku != null && value != sliderDźwięku.value)
            {
                sliderDźwięku.value = value;
            }
        }
    }
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
    public void OdpalGrę()
    {
        ManagerGry.managerGry.ZaładujScenęOIndeksie(4); //Poziomy gry 4+
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
