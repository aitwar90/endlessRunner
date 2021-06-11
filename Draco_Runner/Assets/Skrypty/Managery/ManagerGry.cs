using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGry : MonoBehaviour
{
    public static ManagerGry managerGry = null;

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
        StartCoroutine(CzekajAżZaładujęScenę(0, 2));    //Ładuj dane opcji
        StartCoroutine(ŁadujSceny(2));
    }
    private IEnumerator CzekajAżZaładujęScenę(byte coWykonaćPo, byte scenaDoZaładowania)
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByBuildIndex(scenaDoZaładowania).isLoaded);
        switch (coWykonaćPo)
        {
            case 0: //Po załadowaniu Załaduj dane
                Dane.ŁadujDane(1);
                break;
        }
    }
    private IEnumerator ŁadujSceny(byte scenaDoZaładowania, bool czyMainUI = false)
    {
        AsyncOperation asyncOperation;
        //Deaktywuj wszystkie sceny z wyjątkiem Dane (sceny z build index 0)
        for(byte i = 1; i < SceneManager.sceneCount; i++)
        {
            asyncOperation = SceneManager.UnloadSceneAsync(i);
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
        if(czyMainUI)
        {   //Załaduj scenę MainMenu
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        }
    }
}
