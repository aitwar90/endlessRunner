using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BłyskawicaSkrypt : VisualBase
{
    public void GenerujBłyskawicę(Vector3 root, Vector3 targetPosition)
    {
        Vector3 temp = targetPosition - root;
        temp = temp.normalized;
        if(temp.x == 0.0) temp.x = Random.Range(-0.1f, 0.11f);
        if(temp.y == 0.0) temp.y = Random.Range(-0.1f, 0.11f);
        if(temp.z == 0.0) temp.z = Random.Range(-0.1f, 0.11f);
        temp *= UnityEngine.Random.Range(0.85f, 1.45f);
        byte count = 0;
        NodeBłyskawica nBłyskawica = null;
        if (błyskawiceRoot == null)
        {
            błyskawiceRoot = new NodeBłyskawica[1];
            nBłyskawica = new NodeBłyskawica(root.x, root.y, root.z, temp.x, temp.y, temp.z);
            błyskawiceRoot[0] = nBłyskawica;
            HelperGenerujBłyskawicę(błyskawiceRoot[0], temp.x, temp.y, temp.z, count);
            ManagerEfekty.instance.StartCorouiteStorms(0);
            return;
        }
        else
        {
            for (byte i = 0; i < błyskawiceRoot.Length; i++)
            {
                if (!błyskawiceRoot[i].actualUse)
                {
                    //Użyj tej błyskawicy
                    nBłyskawica = błyskawiceRoot[i];
                    HelperZaktualizujBłyskawicę(nBłyskawica, temp.x, temp.y, temp.z, root.x, root.y, root.z);
                    ManagerEfekty.instance.StartCorouiteStorms(i);
                    return;
                }
            }
            //Stwórz nową błyskawicę
            NodeBłyskawica[] nBłys = new NodeBłyskawica[błyskawiceRoot.Length + 1];
            for (byte i = 0; i < błyskawiceRoot.Length; i++)
            {
                nBłys[i] = błyskawiceRoot[i];
            }
            błyskawiceRoot = nBłys;
            nBłyskawica = new NodeBłyskawica(root.x, root.y, root.z, temp.x, temp.y, temp.z);
            błyskawiceRoot[błyskawiceRoot.Length - 1] = nBłyskawica;
            HelperGenerujBłyskawicę(błyskawiceRoot[błyskawiceRoot.Length - 1], temp.x, temp.y, temp.z, count);
            ManagerEfekty.instance.StartCorouiteStorms((byte)(błyskawiceRoot.Length - 1));
        }
    }
    private void HelperGenerujBłyskawicę(NodeBłyskawica aktSprawdzany, float dirx, float diry, float dirz, byte cout)
    {
        if (cout >= 26)
        {
            return;
        }
        byte liczbaOdnóg = (byte)Random.Range((cout < 20) ? 1 : 0, (cout < 20) ? Random.Range(1, 4) : (cout > 24) ? 1 : 3);
        for (byte i = 0; i < liczbaOdnóg; i++)
        {
            aktSprawdzany.DodajOdnogę(dirx, diry, dirz, (i == 0) ? true : false);
        }
        cout++;
        for (byte i = 0; i < liczbaOdnóg; i++)
        {
            HelperGenerujBłyskawicę(aktSprawdzany.odnogiBłyskawicy[i], dirx, diry, dirz, cout);
        }
    }
    private void HelperZaktualizujBłyskawicę(NodeBłyskawica root, float dirx, float diry, float dirz, float prevEndX, float prevEndY, float prevEndZ)
    {
        if(root == null) return;
        root.AktualizujNode(prevEndX, prevEndY, prevEndZ, dirx, diry, dirz);
        if(root.odnogiBłyskawicy == null || root.odnogiBłyskawicy.Length == 0)
        {
            return;
        }
        for(byte i = 0; i < root.odnogiBłyskawicy.Length; i++)
        {
            HelperZaktualizujBłyskawicę(root.odnogiBłyskawicy[i], dirx, diry, dirz, root.ePos.x, root.ePos.y, root.ePos.z);
        }
    }
}
[System.Serializable]
public class NodeBłyskawica
{
    public Vector3 sPos;
    public Vector3 ePos;
    [SerializeField]public NodeBłyskawica[] odnogiBłyskawicy = null;
    public bool actualUse = false;
    public NodeBłyskawica()
    {

    }
    public NodeBłyskawica(float sPosX, float sPosY, float sPosZ, float baseDirX, float baseDirY, float baseDirZ)
    {
        sPos.x = sPosX;
        sPos.y = sPosY;
        sPos.z = sPosZ;
        //actualUse = true;
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
            NodeBłyskawica[] nodeBłyskawicas2 = new NodeBłyskawica[odnogiBłyskawicy.Length + 1];
            for (byte i = 0; i < odnogiBłyskawicy.Length; i++)
            {
                nodeBłyskawicas2[i] = odnogiBłyskawicy[i];
            }
            nodeBłyskawicas2[odnogiBłyskawicy.Length] = _nodeBłyskawica;
            odnogiBłyskawicy = nodeBłyskawicas2;
        }
    }
    public void AktualizujNode(float x, float y, float z, float dirx, float diry, float dirz)
    {
        sPos.x = x;
        sPos.y = y;
        sPos.z = z;
        DodajEndPos(dirx, diry, dirz);
    }
    ///<summary>Metoda tworzy odnogę biorąc pod uwagę kierunek podany w parametrach.</summary>
    ///<param name="x">Kierunek osi X.</param>
    ///<param name="y">Kierunek osi Y.</param>
    ///<param name="z">Kierunek osi Z.</param>
    public void DodajOdnogę(float x, float y, float z, bool first = true)
    {
        float nx = x + UnityEngine.Random.Range(((first) ? -1.45f : -3.40f) * x, ((first) ? 1.45f : 3.40f) * x);
        float ny = y + UnityEngine.Random.Range(-0.45f * y, 0.45f * y);
        float nz = z + UnityEngine.Random.Range(((first) ? -0.55f : 2.40f) * z, ((first) ? 0.55f : 2.40f) * z);
        NodeBłyskawica nBłyskawica = new NodeBłyskawica(ePos.x, ePos.y, ePos.z, nx, ny, nz);
        DodajOdnogę(ref nBłyskawica);
    }
    ///<summary>Metoda tworzy odnogę biorąc pod uwagę kierunek podany w parametrach.</summary>
    ///<param name="x">Kierunek osi X.</param>
    ///<param name="y">Kierunek osi Y.</param>
    ///<param name="z">Kierunek osi Z.</param>
    private void DodajEndPos(float x, float y, float z)
    {
        //float randomD = Random.Range(0.95f, 1.1f);
        ePos.x = (x + UnityEngine.Random.Range(-0.35f * x, 0.35f * x) + sPos.x);// * randomD;
        ePos.y = (y + UnityEngine.Random.Range(-0.25f * y, 0.25f * y) + sPos.y);// * randomD;
        ePos.z = (z + UnityEngine.Random.Range(-0.35f * z, 0.35f * z) + sPos.z);// * randomD;
    }
}
