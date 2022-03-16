using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMainUI : UI_Menu_Abs
{
    public static ManagerMainUI managerMainUI = null;
    public Button sliderMuzyki;
    public Button sliderDźwięku;
    public AudioSource ambientSource;
    public bool UstawGłośnośćMuzyki
    {
        get
        {
            return Dane.poziomMuzyki;
        }
        set
        {
            Dane.poziomMuzyki = value;
            if(sliderMuzyki != null)
            {
                sliderMuzyki.GetComponent<Image>().sprite = ((value) ? sliderMuzyki.GetComponentInParent<KontenerObrazków>().sprites[0] : sliderMuzyki.GetComponentInParent<KontenerObrazków>().sprites[1]);
            }
            ManagerDźwięku.managerDźwięku.SetMixerMusic(value);
        }
    }
    public bool UstawGłośnośćDźwięku
    {
        get
        {
            return Dane.poziomDźwięku;
        }
        set
        {
            Dane.poziomDźwięku = value;
            if(sliderDźwięku != null)
            {
                sliderDźwięku.GetComponent<Image>().sprite = ((value) ? sliderDźwięku.GetComponentInParent<KontenerObrazków>().sprites[0] : sliderDźwięku.GetComponentInParent<KontenerObrazków>().sprites[1]);
            }
            ManagerDźwięku.managerDźwięku.SetMixerSound(value);
        }
    }
    void Awake()
    {
        if (managerMainUI == null)
        {
            managerMainUI = this;
            Dane.ŁadujDane(2);
        }
        else
            Destroy(this);
    }
    protected override void Start()
    {
        base.Start();
        ManagerDźwięku.managerDźwięku.SetAudio(ref ambientSource, 0, true);
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
    public void OdpalUstawienia()
    {
        UstawPanel(1);
    }
    public void OdpalAutorzy()
    {
        
    }
    public void WłWyłMuzykę()
    {
        UstawGłośnośćMuzyki = !Dane.poziomMuzyki;
    }
    public void WłWyłDźwięki()
    {
        UstawGłośnośćDźwięku = !Dane.poziomDźwięku;
    }
    public void OpuśćGrę()
    {
        Application.Quit();
    }
    public void WznówGrę()
    {
         if(Time.timeScale == 0)
         {
             ManagerGry.managerGry.ZaładujIWyładujSceny(new byte[] {3}, new byte[] {2});
         }
    }
    public void WróćDoMenuWMenu()
    {
        UstawPanel(0);
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
