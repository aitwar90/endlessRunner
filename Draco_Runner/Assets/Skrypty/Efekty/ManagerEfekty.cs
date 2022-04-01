using System.Collections;
using UnityEngine;

public class ManagerEfekty : MonoBehaviour
{
    public static ManagerEfekty instance = null;
    public Vector3 posStart;
    VisualBase bScript = null;
    private bool wygenerowane = true;
    public Vector3 posPlayer;
    private byte idx = 0;
    private byte typeOfMap = 0;
    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }
    #region Visualize effects
   public void GenerateVisualObject(TypeVisualBase typeObj)
   {
       switch(typeObj)
       {
           case TypeVisualBase.Błyskawica:
           bScript = new BłyskawicaSkrypt();
           bScript.GenerujEfekt(posStart, posPlayer);
           break;
           case TypeVisualBase.KulaLawy:
           bScript = new WulkanicznaKulaScript();
           bScript.GenerujEfekt(posStart, posPlayer);
           break;
           
       }
   }
    #endregion
}
