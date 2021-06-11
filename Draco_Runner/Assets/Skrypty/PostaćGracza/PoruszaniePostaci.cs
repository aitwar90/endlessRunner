using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoruszaniePostaci : MonoBehaviour
{
    public static PoruszaniePostaci poruszaniePostaci = null;
    private byte tor = 1;
    public float offsetToru = 1.0f;
    void Awake()
    {
        if(poruszaniePostaci == null)
        {
            poruszaniePostaci = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        ObsłużInput();
    }
    ///<summary>Obsługa strumieni wejścia z klawiszy.</summary>
    private void ObsłużInput()
    {
        if(Input.anyKeyDown)
        {
            float x = Input.GetAxisRaw("Horizontal");
            if(x < 0 && tor > 0)
            {
                tor--;
                this.transform.Translate(-Vector3.right);
            }
            else if(tor < 2 && x > 0)
            {
                tor++;
                this.transform.Translate(Vector3.right);
            }
        }
    }
    ///<summary>Kiedy postać ginie ma zostać wywołana ta metoda.</summary>
    public void GameOver()
    {
        //Wyświetl Canvas po śmierci (Obejrzenie reklamy ma wskrzeszać gracza)
    }
}
