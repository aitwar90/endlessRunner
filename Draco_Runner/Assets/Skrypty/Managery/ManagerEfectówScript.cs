using System.Collections.Generic;
using UnityEngine;

public class ManagerEfectówScript : MonoBehaviour
{
    public static ManagerEfectówScript instance = null;
    private PoolingClass[] poolingEffects = null;
    private PoolingObjects[] poolingObjects = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            poolingEffects = new PoolingClass[System.Enum.GetNames(typeof(TypeVisualBase)).Length];
            poolingObjects = new PoolingObjects[poolingEffects.Length];
            for(byte i = 0; i < poolingEffects.Length; i++)
            {
                poolingEffects[i] = new PoolingClass();
                poolingObjects[i] = new PoolingObjects();
            }
        }
        else
        {
            Destroy(this);
        }
    }
    public void AddToStackVisualData(ElementVisual vBase, byte type)
    {
        poolingEffects[type].AddToStack(ref vBase);
    }
    public ElementVisual GetFromStackVisualData(byte type)
    {
        if (poolingEffects == null)
        {
            return null;
        }
        return poolingEffects[type].GetItemFromStack();
    }
    public void AddToStackObject(VisualObjectBase vBase, byte type)
    {
        poolingObjects[type].AddToStack(ref vBase);
    }
    public VisualObjectBase GetFromStackObject(byte type)
    {
        if (poolingObjects == null)
        {
            return null;
        }
        return poolingObjects[type].GetItemFromStack();
    }
    public void ResetData()
    {
        for (byte i = 0; i < poolingEffects.Length; i++)
        {
            poolingEffects[i].ResetData();
        }
    }
    private class PoolingClass
    {
        private Queue<ElementVisual> stos = null;
        public void AddToStack(ref ElementVisual vBase)
        {
            if (stos == null)
            {
                stos = new Queue<ElementVisual>();
            }
            stos.Enqueue(vBase);

        }
        public ElementVisual GetItemFromStack()
        {
            if (stos == null || stos.Count == 0)
            {
                return null;
            }
            else
            {
                return stos.Dequeue();
            }
        }
        public void ResetData()
        {
            stos = null;
        }
    }
    private class PoolingObjects
    {
        private Queue<VisualObjectBase> stos = null;
        public void AddToStack(ref VisualObjectBase vBase)
        {
            if (stos == null)
            {
                stos = new Queue<VisualObjectBase>();
            }
            stos.Enqueue(vBase);

        }
        public VisualObjectBase GetItemFromStack()
        {
            if (stos == null || stos.Count == 0)
            {
                return null;
            }
            else
            {
                return stos.Dequeue();
            }
        }
        public void ResetData()
        {
            stos = null;
        }
    }
}
public enum TypeVisualBase
{
    KulaLawy = 0,
    Błyskawica = 1
}
