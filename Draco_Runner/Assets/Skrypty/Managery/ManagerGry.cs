using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGry : MonoBehaviour
{
    public static ManagerGry managerGry = null;
    public TextAsset jezykiTekst;
    void Awake()
    {
        if (managerGry == null)
        {
            managerGry = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        Dane.ŁadujDane(0);
        ZaładujScenęOIndeksie(2);
    }
    /**
    <summary>
    Metoda ładuje scene o zadanym indeksie.
    </summary>
    <param name="sceneIndex">Indeks sceny jaka ma zostać załadowana.</param>
    */
    public void ZaładujScenęOIndeksie(byte sceneIndex)
    {
        byte coWykonaćPo = 0;
        bool czyMainUI = true;
        if(sceneIndex > 3)
        {
            coWykonaćPo = 1;
            czyMainUI = false;
        }
        StartCoroutine(CzekajAżZaładujęScenę(coWykonaćPo, sceneIndex));    //Ładuj dane opcji
        StartCoroutine(ŁadujSceny(sceneIndex, czyMainUI));
    }
    ///<summary>Funkcja czeka, aż zostanie załadowana zadana scena.</summary>
    ///<param name="scenaDoZaładowania">Indeks określający czynność jaka ma się wykonać po załadowani zadanej sceny.</param>
    ///<param name="czyMainUI">Indeks sprawdzanej sceny.</param>
    private IEnumerator CzekajAżZaładujęScenę(byte coWykonaćPo, byte scenaDoZaładowania)
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByBuildIndex(scenaDoZaładowania).isLoaded);
        switch (coWykonaćPo)
        {
            case 0: //Po załadowaniu Załaduj dane
                yield return new WaitUntil(()=>ManagerUI.managerUI != null);
                Dane.ŁadujDane(1);
                break;
            case 1:
                yield return new WaitUntil(() => ManagerMainUI.managerMainUI != null);
                Dane.ŁadujDane(2);
                break;
        }
    }
    ///<summary>Funkcja ładuje scenę zadaną w parametrze.</summary>
    ///<param name="scenaDoZaładowania">Indeks sceny jaka ma zostać załadowana z Build Index.</param>
    ///<param name="czyMainUI">Czy scena ma łądować MainUI (UI Gry).</param>
    private IEnumerator ŁadujSceny(byte scenaDoZaładowania, bool czyMainUI = false)
    {
        AsyncOperation asyncOperation = new AsyncOperation();
        //Deaktywuj wszystkie sceny z wyjątkiem Dane (sceny z build index 0)
        for(int i = 1; i < SceneManager.sceneCount; i++)
        {
            asyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            yield return new WaitUntil(()=> asyncOperation.isDone);
        }
        //Załaduj scenę Loading
        asyncOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        yield return new WaitUntil(()=> asyncOperation.isDone);
        //Załaduj właściwą scenę jaką chcesz odpalić
        asyncOperation = SceneManager.LoadSceneAsync(scenaDoZaładowania, LoadSceneMode.Additive);
        yield return new WaitUntil(()=>asyncOperation.isDone);
        //Wyłącz scenę Loading
        asyncOperation = SceneManager.UnloadSceneAsync(1);
        //Załaduj scenę z UI
        if(!czyMainUI)
        { 
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        }
    }
}
