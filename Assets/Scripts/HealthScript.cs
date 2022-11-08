using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp = 100;

    private void Update()
    {
        if (hp > 100)
        {
            hp--;
        }
    }

    public void ModifyHealth(int n)
    {
        hp -= n;
    }
}
