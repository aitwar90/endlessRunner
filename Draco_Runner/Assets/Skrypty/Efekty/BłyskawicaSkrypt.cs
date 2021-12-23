using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BłyskawicaSkrypt : VisualBase
{
    public void GenerujBłyskawicę(Vector3 root, Vector3 targetPosition)
    {
        Vector3 temp = targetPosition - root;
        temp = temp.normalized;
        temp *= UnityEngine.Random.Range(0.75f, 1.25f);
        byte count = 0;
        if(błyskawiceRoot == null)
        {
            błyskawiceRoot = new NodeBłyskawica[1];
            NodeBłyskawica nBłyskawica = new NodeBłyskawica(root.x, root.y, root.z, temp.x, temp.y, temp.z);
            błyskawiceRoot[0] = nBłyskawica;
            HelperGenerujBłyskawicę(ref błyskawiceRoot[0], temp.x, temp.y, temp.z, count);
            return;
        }
        else
        {
            for(byte i = 0; i < błyskawiceRoot.Length; i++)
            {
                if(!błyskawiceRoot[i].actualUse)
                {
                    //Użyj tej błyskawicy
                    NodeBłyskawica nBłyskawica = new NodeBłyskawica(root.x, root.y, root.z, temp.x, temp.y, temp.z);
                    błyskawiceRoot[i] = nBłyskawica;
                    HelperZaktualizujBłyskawicę(ref błyskawiceRoot[i], temp.x, temp.y, temp.z);
                    return;
                }
            }
            //Stwórz nową błyskawicę
            NodeBłyskawica[] nBłys = new NodeBłyskawica[błyskawiceRoot.Length+1];
            for(byte i = 0; i < błyskawiceRoot.Length; i++)
            {
                nBłys[i] = błyskawiceRoot[i];
            }
            błyskawiceRoot = nBłys;
            NodeBłyskawica nBłyskawica = new NodeBłyskawica(root.x, root.y, root.z, temp.x, temp.y, temp.z);
            błyskawiceRoot[błyskawiceRoot.Length-1] = nBłyskawica;
            HelperGenerujBłyskawicę(ref błyskawiceRoot[błyskawiceRoot.Length-1], temp.x, temp.y, temp.z, count);
        }
    }
    private void HelperGenerujBłyskawicę(ref NodeBłyskawica root, float dirx, float diry, float dirz, byte cout)
    {
    }
    private void HelperZaktualizujBłyskawicę(ref NodeBłyskawica root, float dirx, float diry, float dirz)
    {

    }
}
public class NodeBłyskawica
{
    public Vector3 sPos;
    public Vector3 ePos;
    public NodeBłyskawica[] odnogiBłyskawicy = null;
    public bool actualUse = false;
    public NodeBłyskawica()
    {

    }
    public NodeBłyskawica(float sPosX, float sPosY, float sPosZ, float baseDirX, float baseDirY, float baseDirZ)
    {
        sPos.x = sPosX;
        sPos.y = sPosY;
        sPos.z = sPosZ;
        actualUse = true;
        DodajEndPos(baseDirX, baseDirY, baseDirZ);
    }
    private void DodajOdnogę(ref NodeBłyskawica _nodeBłyskawica)
    {
        if (odnogiBłyskawicy == null)
        {
            odnogiBłyskawicy = new NodeBłyskawica[1];
            odnogiBłyskawicy[0] = _nodeBłyskawica;
        }
        else
        {
            NodeBłyskawica[] nodeBłyskawicas2 = new NodeBłyskawica[odnogiBłyskawicy.Length+1];
            for(byte i = 0; i < odnogiBłyskawicy.Length; i++)
            {
                nodeBłyskawicas2[i] = odnogiBłyskawicy[i];
            }
            nodeBłyskawicas2[odnogiBłyskawicy.Length] = _nodeBłyskawica;
            odnogiBłyskawicy = nodeBłyskawicas2;
        }
    }
    ///<summary>Metoda tworzy odnogę biorąc pod uwagę kierunek podany w parametrach.</summary>
    ///<param name="x">Kierunek osi X.</param>
    ///<param name="y">Kierunek osi Y.</param>
    ///<param name="z">Kierunek osi Z.</param>
    public void DodajOdnogę(float x, float y, float z)
    {
        float nx = x + UnityEngine.Random.Range(-0.25f*x, 0.25f*x) + ePos.x;
        float ny = y + UnityEngine.Random.Range(-0.15f*y, 0.15f*y) + ePos.y;
        float nz = z + UnityEngine.Random.Range(-0.25f*z, 0.25f*z) + ePos.z;
        NodeBłyskawica nBłyskawica = new NodeBłyskawica(nx, ny, nz, x, y, z);
        DodajOdnogę(ref nBłyskawica);
    }
    ///<summary>Metoda tworzy odnogę biorąc pod uwagę kierunek podany w parametrach.</summary>
    ///<param name="x">Kierunek osi X.</param>
    ///<param name="y">Kierunek osi Y.</param>
    ///<param name="z">Kierunek osi Z.</param>
    private void DodajEndPos(float x, float y, float z)
    {
        ePos.x = x + UnityEngine.Random.Range(-0.15f*x, 0.15f*x) + sPos.x;
        ePos.y = y + UnityEngine.Random.Range(-0.15f*y, 0.15f*y) + sPos.y;
        ePos.z = z + UnityEngine.Random.Range(-0.15f*z, 0.15f*z) + sPos.z;
    }
}
