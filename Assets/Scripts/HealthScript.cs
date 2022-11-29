using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthScript : MonoBehaviour, IDamageable
{
    public Text hpbar;
    public SpriteRenderer pSprite;
    public int hp = 100;

    [SerializeField] private int maxHP = 100;
    [SerializeField] private Color deadColor;
    [SerializeField] private Color aliveColor;

    private bool isalive = true;
    private bool run = true;
    private bool disableTxt = false;
    private Behaviour pc;

    PhotonView view;

    private void Start()
    {
       pc = GetComponent<PlayerController>();
       view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!disableTxt)
        {
            if (hp > maxHP)
            {
                hp = maxHP;
            }
            if (hp <= 0)
            { //Dead
                isalive = false;
                hp = 0;
                pSprite.color = deadColor;
            }
            else if (hp > 0)
            { //Alive
                isalive = true;
                pSprite.color = aliveColor;
            }
            hpbar.text = hp + "/" + maxHP.ToString();

            if (!isalive && run)
            {
                pc.enabled = !pc.enabled;
                run = false;
            }
            else if (isalive && !run)
            {
                pc.enabled = !pc.enabled;
                run = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        view.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!view.IsMine)
            return;

        Debug.Log("Took Damage: " + damage);
    }
}
