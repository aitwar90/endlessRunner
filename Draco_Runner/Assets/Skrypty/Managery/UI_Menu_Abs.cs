using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Menu_Abs : MonoBehaviour
{
    private byte indeksAktJezyka = 0;
    public GameObject[] tablicaPaneli;
    protected byte actualActivePanel;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        actualActivePanel = 255;
        if (tablicaPaneli != null && tablicaPaneli.Length > 0)
        {
            for (byte i = 0; i < tablicaPaneli.Length; i++)
            {
                DisablePanel(i);
            }
            UstawPanel(0);
        }
        UstawJęzyk((byte)Dane.ustalonyJęzyk);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    ///<summary>Metoda ustawia jezyk dla UI. Kolejne elementy rozdzielone ; a języki | (nazwaObjektu|Angielski|Polski|Rosyjski|Ukraiński;)</summary>
    ///<param name="ustIdx">Indeks języka na który ma zostać ustawiony UI.</param>
    public void UstawJęzyk(byte ustIdx = 0)
    {
        if (ustIdx == indeksAktJezyka)   //Jeśli jezyk ustawiony jest taki sam jak wymagany to pomiń
        {
            Debug.Log("Indeks zgodny jest z aktualnym językiem");
            return;
        }
        if (ManagerGry.managerGry.jezykiTekst == null)   //Brak pliku tekstowego
        {
            Debug.Log("Brak pliku tekstowego zawierającego jezyki (ManagerGry na scenie Dane języki");
            return;
        }
        indeksAktJezyka = ustIdx;
        ustIdx++;
        string fs = ManagerGry.managerGry.jezykiTekst.text;
        fs = fs.Replace("\n", "");
        fs = fs.Replace("\r", "");
        string[] fLines = fs.Split(';');
        Text[] wszystkieDane = FindObjectsOfType(typeof(Text)) as Text[];
        for (ushort i = 0; i < fLines.Length; i++)
        {
            string[] pFrazy = fLines[i].Split('|');
            //ustIdx to indeks jaki ma zostać przypisany do poszczególnych elementów textu
            for (ushort j = 0; j < wszystkieDane.Length; j++)
            {
                if (wszystkieDane[j].gameObject.name == pFrazy[0])
                {
                    wszystkieDane[j].text = pFrazy[ustIdx]; //Ustawiam konkretny tekst do konkretnego komponentu Text
                }
            }
        }
    }
    public void UstawPanel(int idxPanel)
    {
        if (actualActivePanel == idxPanel || tablicaPaneli == null || idxPanel >= tablicaPaneli.Length) return;

        if (actualActivePanel < tablicaPaneli.Length)
            tablicaPaneli[actualActivePanel].SetActive(false);
        tablicaPaneli[idxPanel].SetActive(true);
        actualActivePanel = (byte)idxPanel;
    }
    public void DisablePanel(int idxPanel)
    {
        if (tablicaPaneli == null || idxPanel >= tablicaPaneli.Length) return;
        tablicaPaneli[idxPanel].SetActive(false);
    }
}
