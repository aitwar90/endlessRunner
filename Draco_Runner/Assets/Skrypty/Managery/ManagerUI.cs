using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
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
    ///<summary>Metoda wywoływana podczas zmiany języka interfejsu.</summary>
    ///<param name="jakiJęzyk">Indeks języka jaki ma zostać ustawiony (0 - Angielski, 1 - Polski, 2 - Rosyjski, 3 - Ukraiński)
    public void UstawJęzyk(int jakiJęzyk)
    {

    }
}
