using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dane", menuName = "Utwórz/Dane", order = 1)]
public class ValueSerialized : ScriptableObject
{
    public float prędkośćKuli;
    public GameObject prefabKuli;
    public float czasDoPojawieniaSięBłyskawicy;
    public float czasTrwaniaBłyskawicy;
    public GameObject prefabBłyskawicy;

}
