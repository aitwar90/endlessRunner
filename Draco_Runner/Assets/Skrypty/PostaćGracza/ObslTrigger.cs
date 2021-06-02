using UnityEngine;

public class ObslTrigger : MonoBehaviour
{
    public TypTriggera typTriggera;
    private bool jestemWTriggerze = false;
    void OnTriggerEnter(Collider other)
    {
        if(!jestemWTriggerze && other.CompareTag("Player"))
        {
            jestemWTriggerze = true;
            //Wpadła postać w trigger
            switch((byte)typTriggera)
            {
                case 0: //Śmierć

                break;
                case 1: //Bonus

                break;
                case 2: //Punkty

                break;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(jestemWTriggerze && other.CompareTag("Player"))
        {
            jestemWTriggerze = false;
            //Wpadła postać w trigger
            switch((byte)typTriggera)
            {
                case 1: //Bonus

                break;
            }
        }
    }
}
public enum TypTriggera
{
    Śmierć = 0,
    Bonus = 1,
    Punkty = 2
}
