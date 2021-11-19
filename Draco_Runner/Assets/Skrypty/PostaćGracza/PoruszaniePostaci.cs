using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoruszaniePostaci : MonoBehaviour
{
    public static PoruszaniePostaci poruszaniePostaci = null;
    public float aktualnyMnożnikPrędkości = 0.005f;
    private float docelowyMnożnikPrędkości = 1.0f;
    public float aktualnaMocSkrętu = 1.0f;
    private float docelowaMocSkrętu = 1.0f;

    private float odchylenieOśZ = 0.0f;
    private float odchylenieOsiX = 0.0f;
    private Vector3 przesunięcie = Vector3.zero;
    private Vector3 obrót = Vector3.zero;
    private bool postaćBok = false;
    void Awake()
    {
        if (poruszaniePostaci == null)
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
        ObsłużObrót();
        ObsłużLogikęLotu();
        ZaktualizujTransform();
    }
    ///<summary>Obsługa strumieni wejścia z klawiszy.</summary>
    private void ObsłużObrót()
    {
        if (Input.anyKey)
        {
            Vector3 temp = Vector3.zero;
            float mocSkrętu = aktualnaMocSkrętu * Time.deltaTime;
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (Mathf.Abs(x) > 0.05f)
            {
                temp.y += x * mocSkrętu;
                temp.z -= temp.y * 2.0f;
                //Debug.Log("Modyfikuje skręt");
            }
            if (Mathf.Abs(y) > 0.05f)
            {
                temp.x += y * mocSkrętu;
                //Debug.Log("Modyfikuje pochył");
            }
            if (temp.x != 0)
            {
                postaćBok = true;
            }
            obrót = temp;
        }
        else
        {
            obrót = Vector3.zero;
        }
    }
    /**
    <summary>
    Metoda obsługuję logikę lotu.
    </summary>
    */
    private void ObsłużLogikęLotu()
    {
        bool zwiekszSpadanie = false;
        WyrównujLot();
        if (Mathf.Abs(odchylenieOśZ) > 45.0f)
        {
            zwiekszSpadanie = true;
        }
        przesunięcie = this.transform.forward * aktualnyMnożnikPrędkości * Time.deltaTime;
        if (zwiekszSpadanie)
        {
            przesunięcie.y -= (odchylenieOśZ / 1000.0f) * Time.deltaTime;
        }
    }
    /**
    <summary>
    Metoda ma za zadanie wyrównać lot jednostki.
    </summary>
    */
    private void WyrównujLot()
    {
        if (!postaćBok)  //Gracz nie trzyma strzałki
        {
            if (odchylenieOśZ < 0.1f)
            {
                obrót.z += Time.deltaTime * PrzemnożPrzezFunkcję(odchylenieOśZ);
            }
            else if (odchylenieOśZ > 0.1f)
            {
                obrót.z -= Time.deltaTime * PrzemnożPrzezFunkcję(odchylenieOśZ);
            }
            else
            {
                obrót.z = -odchylenieOśZ;
            }
            if (odchylenieOsiX < 0.1f)
            {
                obrót.x += Time.deltaTime * PrzemnożPrzezFunkcję(odchylenieOsiX/2.0f);
            }
            else if (odchylenieOsiX > 0.1f)
            {
                obrót.x -= Time.deltaTime * PrzemnożPrzezFunkcję(odchylenieOsiX/2.0f);
            }
            else
            {
                obrót.x = -odchylenieOsiX;
            }
        }
    }
    /**
    <summary>
    Metoda aktualizuje pozycję i rotację obiektu gracza.
    </summary>
    */
    private void ZaktualizujTransform()
    {
        this.transform.Translate(przesunięcie, Space.World);
        this.transform.Rotate(obrót, Space.Self);
        Quaternion quat = this.transform.rotation;
        odchylenieOśZ = NaprawRotację(quat.eulerAngles.z);
        odchylenieOsiX = NaprawRotację(quat.eulerAngles.x);
        postaćBok = false;
    }
    /**
    <summary>
    Funkcja zmienia docelowy mnożnik prędkości dla postaci o wartość podaną w parametrze i zwraca nową wartość zmiennej docelowyMnożnikPrędkości.
    </summary>
    <param name="delta">Wartość o jaką zostanie zmieniony docelowy mnożnik prędkosci postaci.</param>
    */
    public float ZmieńMnożnikPrędkości(float delta)
    {
        docelowyMnożnikPrędkości += delta;
        return docelowyMnożnikPrędkości;
    }
    /**
    <summary>
    Funkcja zmienia docelowy mnożnik skrętu dla postaci o wartość podaną w parametrze i zwraca nową wartość zmiennej docelowyMnożnikSkrętu.
    </summary>
    <param name="delta">Wartość o jaką zostanie zmieniony docelowy mnożnik skrętu postaci.</param>
    */
    public float ZmieńMnożnikSkrętu(float delta)
    {
        docelowaMocSkrętu += delta;
        return docelowaMocSkrętu;
    }
    ///<summary>Kiedy postać ginie ma zostać wywołana ta metoda.</summary>
    public void GameOver()
    {
        //Wyświetl Canvas po śmierci (Obejrzenie reklamy ma wskrzeszać gracza)
    }
    /**
    <summary>
    Funkcja zwraca naprawioną wartość rotacji.
    </summary>
    */
    private float NaprawRotację(float rotacjaDoNaprawy)
    {
        if (rotacjaDoNaprawy > 180.0f)
        {
            rotacjaDoNaprawy = -360.0f + rotacjaDoNaprawy;
        }
        else if (rotacjaDoNaprawy < -180.0f)
        {
            rotacjaDoNaprawy = 360.0f - rotacjaDoNaprawy;
        }
        if (Mathf.Abs(rotacjaDoNaprawy) < 0.05f)
            rotacjaDoNaprawy = 0.0f;
        return rotacjaDoNaprawy;
    }
    private float PrzemnożPrzezFunkcję(float x)
    {
        //if(x < 35f && x > -35f)
        //{
        x /= 2.0f;
        x*=x;
        if(x > 175f)
            x = 175f;
        return x;
        //}
        //else
        //{
        //    return x / 3.0f;
        //}
        /*
        if (x > 30f)
        {
            x = 2 / x;

        }
        else
        {
            x = x/(2*x);
        }
        return x;
        */
    }
}
