using UnityEngine;
public class BłyskawicaSkrypt : VisualBase
{
    private void GenerujBłyskawicę(Vector3 root, Vector3 targetPosition)
    {
        Vector3 temp = targetPosition - root;
        temp = temp.normalized;
        if (temp.x == 0.0) temp.x = Random.Range(-0.1f, 0.11f);
        if (temp.y == 0.0) temp.y = Random.Range(-0.1f, 0.11f);
        if (temp.z == 0.0) temp.z = Random.Range(-0.1f, 0.11f);
        temp *= UnityEngine.Random.Range(0.85f, 1.45f);
        byte count = 0;
        NodeBłyskawica nBłyskawica = null;
        if (myElementRoot == null)
        {
            myElementRoot = ManagerEfectówScript.instance.GetFromStackVisualData(1);
        }
        if (myElementRoot == null)
        {
            nBłyskawica = new NodeBłyskawica(root.x, root.y, root.z, temp.x, temp.y, temp.z);
            myElementRoot = nBłyskawica;
            HelperGenerujBłyskawicę(nBłyskawica, temp.x, temp.y, temp.z, count);
            PrzypiszVisualObiectBase();
            return;
        }
        else
        {
            if (!myElementRoot.actualUse)
            {
                //Użyj tej błyskawicy
                nBłyskawica = (NodeBłyskawica)myElementRoot;
                HelperZaktualizujBłyskawicę(nBłyskawica, temp.x, temp.y, temp.z, root.x, root.y, root.z);
                PrzypiszVisualObiectBase();
                return;
            }
            else
            {
                BłyskawicaSkrypt bs = new BłyskawicaSkrypt();
                bs.GenerujEfekt(root, targetPosition);
                return;
            }
        }
    }
    private void GenerujBłyskawicę()
    {
        GenerujBłyskawicę(root, targetPositionBase);
    }
    private void PrzypiszVisualObiectBase()
    {
        VisualObjectBase vob = ManagerEfectówScript.instance.GetFromStackObject(1);
        if(vob == null)
        {
            GameObject go = new GameObject("Błyskawica");
            vob = go.AddComponent<BłyskawicaObjectBase>();
        }
        vob.MyElementRoot = myElementRoot;
        vob.ActivateMe();
        visualObjectBase = vob;
    }
    public override void GenerujEfekt()
    {
        GenerujBłyskawicę();
    }
    public override void GenerujEfekt(Vector3 root, Vector3 targetPosition)
    {
        GenerujBłyskawicę(root, targetPosition);
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
    private void HelperZaktualizujBłyskawicę(NodeBłyskawica root, float dirx, float diry, float dirz, float prevEndX, float prevEndY, float prevEndZ, bool first = true)
    {
        if (root == null) return;
        root.AktualizujNode(prevEndX, prevEndY, prevEndZ, dirx, diry, dirz, first);
        if (root.odnogiBłyskawicy == null || root.odnogiBłyskawicy.Length == 0)
        {
            return;
        }
        for (byte i = 0; i < root.odnogiBłyskawicy.Length; i++)
        {
            HelperZaktualizujBłyskawicę(root.odnogiBłyskawicy[i], dirx, diry, dirz, root.ePos.x, root.ePos.y, root.ePos.z, (i == 0) ? true : false);
        }
    }
}
[System.Serializable]
public class NodeBłyskawica : ElementVisual
{
    [SerializeField] public NodeBłyskawica[] odnogiBłyskawicy = null;
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
    public void AktualizujNode(float x, float y, float z, float dirx, float diry, float dirz, bool itFirst = false)
    {
        sPos.x = x;
        sPos.y = y;
        sPos.z = z;
        if (itFirst)
        {
            DodajEndPos(dirx, diry, dirz);
        }
        else
        {
            AktualizujOdnogę(dirx, diry, dirz, itFirst);
        }
    }
    public void AktualizujOdnogę(float x, float y, float z, bool first = true)
    {
        float nx = x + UnityEngine.Random.Range(((first) ? -1.45f : -3.40f) * x, ((first) ? 1.45f : 3.40f) * x);
        float ny = y + UnityEngine.Random.Range(-0.45f * y, 0.45f * y);
        float nz = z + UnityEngine.Random.Range(((first) ? -0.55f : 2.40f) * z, ((first) ? 0.55f : 2.40f) * z);
        DodajEndPos(nx, ny, nz);

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
