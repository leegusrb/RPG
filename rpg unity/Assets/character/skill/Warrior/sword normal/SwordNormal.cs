using UnityEngine;
using System.Collections;
using Photon.Pun;

public class SwordNormal : MonoBehaviourPunCallbacks
{
    private float Deal;
    //private float Heal;
    //private float Shield;
    //private float Power;    
    private float DealSync;
    public GameObject target;
    //public Vector3 targetPos;
    public PhotonView PV;

    public void giveDeal()
    {
        if (PV.IsMine)
        {
            PhotonView MonsterPV = target.transform.GetComponent<PhotonView>();
            MonsterPV.RPC("MonsterDamage", RpcTarget.All, 5, Deal, 0f);
            PV.RPC("destroySelf", RpcTarget.AllBuffered);
        }        
    }
    IEnumerator Excute()
    {
        if (!PV.IsMine)
            yield break;
        float _timer = 0f;
        while (_timer < DealSync)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        giveDeal();
    }
    [PunRPC]
    void destroySelf()
    {
        /*try
        {
            GetComponent<Animator>().SetTrigger("vanish");
        }
        catch { }*/
        Destroy(gameObject);
    }

    [PunRPC]
    void initSkill(float deal, float heal, float sheild, float power, float sync, float duration, string target_name, Vector2 target_pos)
    {
        Deal = deal;
        //Heal = heal;
        //Shield = sheild;
        //Power = power
        if (target_name != "")
        {
            target = GameObject.Find(target_name);
            if (target == null)
            {
                PV.RPC("destroySelf", RpcTarget.AllBuffered, 0f);
                return;
            }
            transform.parent = target.transform;
        }
        if (target_pos != default(Vector2))
        {
            //targetPos = target_pos;
        }
        DealSync = sync;
        StartCoroutine(Excute());        
    }
}
