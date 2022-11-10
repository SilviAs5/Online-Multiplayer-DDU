using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int hp = 100;
    [SerializeField] private int maxHP = 100;
    public bool isalive = true;
    public Text hbar;

    private void Start()
    {
        GameObject healthbar = GameObject.FindGameObjectWithTag("Healthbar");
        hbar = healthbar.GetComponent<Text>();
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
        hp += n;
    }
}
