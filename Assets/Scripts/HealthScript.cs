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

    [SerializeField] private int  maxHP = 100;
    [SerializeField] private Color deadColor;
    [SerializeField] private Color aliveColor;
    [SerializeField] private GameObject itemHolder;

    public bool isalive = true;
    private bool run = true;
    private bool disableTxt = false;
    private Behaviour pc;

    PhotonView view;
    PlayerManager playerManager;

    private void Start()
    {
       pc = GetComponent<PlayerController>();
       view = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)view.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Update()
    {
        UpdateHealth();
        IsDead();
    }

    private void UpdateHealth()
    {
        if (!disableTxt)
        {
            if (hp > maxHP)
            {
                hp = maxHP;
            }
            if (hp <= 0) //Dead
            { 
                isalive = false;
            }
            else if (hp > 0) //Alive
            { 
                isalive = true;
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

    private void IsDead()
    {
        if (!isalive) //Dead
        {
            hp = 0;
            pSprite.color = deadColor;
            itemHolder.SetActive(false);
        }
        else if (isalive) //Alive
        {
            pSprite.color = aliveColor;
            itemHolder.SetActive(true);
        }

    }

    public void TakeDamage(int damage)
    {
        view.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
        if (!view.IsMine)
            return;

        if (hp > 0)
        {
            hp -= damage;
        }
        else if (hp <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        playerManager.Die();
    }
}
