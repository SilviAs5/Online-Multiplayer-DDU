using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthScript : MonoBehaviour
{
    public int hp = 100;
    [SerializeField] private int maxHP = 100;
    public bool isalive = true;
    public Text hbar;
    PhotonView view;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        if (hp <= 0){
            isalive = false;
        } else if (hp > 0){
            isalive = true;
        }
        hbar.text = hp+"/"+maxHP.ToString();
    }

    public void ModifyHealth(int n)
    {
        hp -= n;
        print(hp);
    }
}
