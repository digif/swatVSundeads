using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    int hp;
    private void Start()
    {
        hp = 10;
    }

    public void OnHit()
    {
        hp -= 1;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
