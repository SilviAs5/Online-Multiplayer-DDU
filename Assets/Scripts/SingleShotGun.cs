using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] GameObject firePos;

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {

        Debug.DrawRay(firePos.transform.position, firePos.transform.forward * 50, Color.blue);
        //Ray ray = firePos.transform.position, firePos.transform.forward(new Vector2(0f, 0f));
    }
}
