using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolingSystem
{
    public static NodeOfPool rootOfPooling = null;
    private static byte allNodIdxTemp = 0;
    public static void AddToPooling(GameObject obj, int type)
    {
        AddToTree(type, obj);
    }
    public static GameObject GetObjectFromPooling(int type)
    {
        NodeOfPool nop = FindNodeInTree(type);
        if(nop != null)
        {   
            return nop.GetIdxFromQueue();
        }
        return null;
    }
    private static bool AddToTree(int type, GameObject objId)
    {
        if(type < 0)
        {
            Debug.LogError("Type nie może być < 0");
            return false;
        }
        if(rootOfPooling == null)
        {
            NodeOfPool nop = new NodeOfPool((ushort)type, objId);
            rootOfPooling = nop;
            return true;
        }
        else
        {
            NodeOfPool actPool = FindNodeInTree(type);
            if(actPool == null)
            {
                //Brak tego noda
                actPool = new NodeOfPool((ushort)type, objId);
                AddToTree(actPool);
                allNodIdxTemp++;
                if(allNodIdxTemp > 6)
                {
                    RecalculateTree();
                    allNodIdxTemp = 0;
                }
                return true;
            }
            else
            {
                actPool.AddToQueue(objId);
                return true;
            }
        }
    }
    private static void RecalculateTree()
    {
        NodeOfPool[] allNodes = GetAllNodesInTree();
        if(allNodes == null)
        {
            return;
        }
        int tmpVal = 0;
        for(ushort i = 0; i < allNodes.Length; i++)
        {
            tmpVal += allNodes[i].type;
        }
        tmpVal = Mathf.RoundToInt(tmpVal/allNodes.Length);
        ushort bIdx = 0;
        ushort smallerIdx = 10000;
        for(ushort i = 0; i < allNodes.Length; i++)
        {
            ushort valueIdx = (ushort)Mathf.Abs(tmpVal - allNodes[i].type);
            if(valueIdx < smallerIdx)
            {
                smallerIdx = valueIdx;
                bIdx = i;
            }
        }
        rootOfPooling = null;
        allNodes[bIdx].lewyPool = null;
        allNodes[bIdx].prawyPool = null;
        rootOfPooling = allNodes[bIdx];
        for(ushort i = 0; i < allNodes.Length; i++)
        {
            if(i != bIdx)
            {
                allNodes[i].lewyPool = null;
                allNodes[i].prawyPool = null;
                AddToTree(allNodes[i]);
            }
        }
    }
    private static bool AddToTree(NodeOfPool gPool)
    {
        NodeOfPool actPool = rootOfPooling;
        while(true)
        {
            if(gPool.type < actPool.type)
            {
                if(actPool.lewyPool == null)
                {
                    actPool.lewyPool = gPool;
                    return true;
                }
                else
                {
                    actPool = actPool.lewyPool;
                }
            }
            else
            {
                if(actPool.prawyPool == null)
                {
                    actPool.prawyPool = gPool;
                    return true;
                }
                else
                {
                    actPool = actPool.prawyPool;
                }
            }
        }
    }
    private static NodeOfPool[] GetAllNodesInTree()
    {
        if(rootOfPooling == null)
            return null;
        else
        {
            List<NodeOfPool> nop = new List<NodeOfPool>();
            NodeOfPool actNW = rootOfPooling;
            HelperToGetAllNodes(ref nop, actNW);
            return nop.ToArray();
        }
    }
    private static void HelperToGetAllNodes(ref List<NodeOfPool> nodeOfPools, NodeOfPool actNode)
    {
        nodeOfPools.Add(actNode);
        if(actNode.lewyPool != null)
        {
            HelperToGetAllNodes(ref nodeOfPools, actNode.lewyPool);
        }
        if(actNode.prawyPool != null)
        {
            HelperToGetAllNodes(ref nodeOfPools, actNode.prawyPool);
        }
    }
    private static NodeOfPool FindNodeInTree(int idx)
    {
        if(rootOfPooling == null)
        {
            Debug.LogError("Drzewo poolingu jest puste");
            return null;
        }
        NodeOfPool actNode = rootOfPooling;
        do
        {
            if(actNode.type == idx)
            {
                return actNode;
            }
            else
            {
                if(actNode.type < idx)
                {
                    if(actNode.lewyPool != null)
                    {
                        actNode = actNode.lewyPool;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if(actNode.prawyPool != null)
                    {
                        actNode = actNode.prawyPool;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        } while(true);
    }
}
public class NodeOfPool
{
    public ushort type;
    public Queue<GameObject> hashCode;
    public NodeOfPool lewyPool;
    public NodeOfPool prawyPool;
    public NodeOfPool(ushort _type, GameObject _objId)
    {
        type = _type;
        AddToQueue(_objId);
    }
    public void AddToQueue(GameObject _objId)
    {
        if(hashCode == null)
        {
            hashCode = new Queue<GameObject>();
        }
        if(hashCode.Count == 0)
        {
            hashCode.Enqueue(_objId);
        }
        else
        {
            Queue<GameObject> tQ = new Queue<GameObject>();
            bool foundIt = false;
            while(hashCode.Count > 0)
            {
                GameObject actD = hashCode.Dequeue();
                tQ.Enqueue(actD);
                if(actD == _objId)
                {   
                    foundIt = true;
                }
            }
            if(!foundIt)
            {
                tQ.Enqueue(_objId);
            }
            hashCode = tQ;
        }
    }
    public GameObject GetIdxFromQueue()
    {
        if(hashCode == null || hashCode.Count == 0)
            return null;
        return hashCode.Dequeue();
    }

}

