using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthScript : MonoBehaviour
{
    public Text hpbar;
    public SpriteRenderer pSprite;
    public int hp = 100;

    [SerializeField] private int maxHP = 100;
    [SerializeField] private Color deadColor;
    [SerializeField] private Color aliveColor;

    private bool isalive = true;
    private bool run = true;
    private Behaviour pc;

    PhotonView view;

    private void Start()
    {
       pc = GetComponent<PlayerController>();
       if (!view.IsMine)
       {
            Destroy(hpbar.gameObject);
       }
    }

    private void Update()
    {
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        if (hp <= 0){ //Dead
            isalive = false;
            hp = 0;
            pSprite.color = deadColor;
        } else if (hp > 0){ //Alive
            isalive = true;
            pSprite.color = aliveColor;
        }
        hpbar.text = hp+"/"+maxHP.ToString();

        if (!isalive && run)
        {
            pc.enabled =! pc.enabled;
            run = false;
        }
        else if (isalive && !run)
        {
            pc.enabled = !pc.enabled;
            run = true;
        }
    }

    public void ModifyHealth(int n)
    {
        hp -= n;
    }
}
