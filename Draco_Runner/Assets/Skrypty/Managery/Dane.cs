using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Dane
{
    public static float poziomMuzyki = 1.0f;
    public static float poziomDźwięku = 1.0f;
    public static Język ustalonyJęzyk;
    public static int rekordPkt = 0;
    private static void InicjujDaneGra()
    {
        rekordPkt = 0;
    }
    private static void InicjujDaneOpcje()
    {
        poziomMuzyki = 1.0f;
        poziomDźwięku = 1.0f;
        ustalonyJęzyk = Język.Angielski;
    }
    ///<summary>Funkcja zapisuje dane do PlayerPrefs i zwraca 0 jeśli zapis został udany.</summary>
    ///<param name="coZapisać">Parametr określa co ma zostać zapisane (0-Dane gry, 1-Opcje).</summary>
    public static byte ZapiszDane(byte coZapisać = 0)
    {
        if (coZapisać == 0)  //Zapis danych gry
        {
            PlayerPrefs.SetInt("RekordPKT", rekordPkt);
            return 0;
        }
        else if (coZapisać == 1)
        {
            PlayerPrefs.SetFloat("PoziomMuzyki", poziomMuzyki);
            PlayerPrefs.SetFloat("PoziomDźwięku", poziomDźwięku);
            PlayerPrefs.SetInt("Język", (int)ustalonyJęzyk);

            return 0;
        }
        return 1;
    }
    ///<summary>Funkcja ładuje dane z PlayerPrefs i jeśli nie ma danych to inicjuje domyślne.</summary>
    ///<param name="coZapisać">Parametr określa co ma zostać wczytane (0-Dane gry, 1-Opcje).</summary>
    private static byte WczytajDane(byte coZapisać = 0)
    {
        if (coZapisać == 0)  //Wczytanie danych gry
        {
            if (PlayerPrefs.HasKey("RekordPKT"))
            {
                rekordPkt = PlayerPrefs.GetInt("RekordPKT", 0);
                return 0;
            }
            else
            {
                InicjujDaneGra();
                return 1;
            }
        }
        else if (coZapisać == 1)    //Wczytanie opcji
        {
            if (PlayerPrefs.HasKey("PoziomMuzyki"))
            {
                poziomMuzyki = PlayerPrefs.GetFloat("PoziomMuzyki", 1.0f);
                poziomDźwięku = PlayerPrefs.GetFloat("PoziomDźwięku", 1.0f);
                ustalonyJęzyk = Język.Angielski;
                ustalonyJęzyk += PlayerPrefs.GetInt("Język", 0);
                /*
                PlayerPrefs.SetFloat("PoziomMuzyki", poziomMuzyki);
                PlayerPrefs.SetFloat("PoziomDźwięku", poziomDźwięku);
                PlayerPrefs.SetInt("Język", (int)ustalonyJęzyk);
                */
                return 0;
            }
            else
            {
                InicjujDaneOpcje();
                return 1;
            }
        }
        return 1;
    }
    ///<summary>Metoda ustawia dane zapisane na dysku.</summary>
    ///<param name="coZaładować">Parametr określający co ma zostać ładowane (0-Dane gry, 1-Dane opcji)</param>
    public static void ŁadujDane(byte coZaładować = 0)
    {
        WczytajDane(coZaładować);
        if(coZaładować == 1)    //Ładuj opcje
        {
            ManagerUI.managerUI.UstawGłośnośćDźwięku = poziomDźwięku;
            ManagerUI.managerUI.UstawGłośnośćMuzyki = poziomMuzyki;
            ManagerUI.managerUI.UstawJęzyk((byte)ustalonyJęzyk);
        }
        else if(coZaładować == 2)   //Ładuj grę
        {

        }
    }
}
public enum Język
{
    Angielski = 1,
    Polski = 1,
    Ukraiński = 2,
    Rosyjski = 3
}
