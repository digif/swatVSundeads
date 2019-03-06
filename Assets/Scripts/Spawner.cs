using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj2spawn;
    public float spawnRate;
    float tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp= spawnRate;
        GameObject zombie = Instantiate(obj2spawn, transform.position, transform.rotation) as GameObject;
        zombie.name = "Zombie";
    }

    // Update is called once per frame
    void Update()
    {/*
        tmp -= Time.deltaTime;
        if (tmp < 0f)
        {
            GameObject zombie = Instantiate(obj2spawn, transform.position, transform.rotation)as GameObject;
            zombie.name = "Zombie";
            tmp = spawnRate;
        }*/
    }
}
