using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int maxEnemies = 5;
    public GameObject obj2spawn;
    public float spawnRate;
    float tmp;
    List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        tmp= spawnRate;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp -= Time.deltaTime;
        if (enemies.Count < maxEnemies && tmp < 0f)
        {
            enemies.Add(Instantiate(obj2spawn, transform.position, transform.rotation) as GameObject);
            tmp = spawnRate;
        }
        List<int> fallenEnemies = new List<int>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyController>().hasFallen)
            {
                Debug.Log("beep");
                fallenEnemies.Add(enemies.IndexOf(enemy));
                StartCoroutine("Despawn", enemy);
            }

        }
        foreach(int enemy in fallenEnemies)
        {
            enemies.RemoveAt(enemy);
        }
        fallenEnemies.Clear();
    }
        

    IEnumerator Despawn(GameObject enemy)
    {
        
        for (float temp = 3; temp > Time.deltaTime; temp -= Time.deltaTime)
        {
            yield return null;
        }
        Debug.Log("boop");
        Destroy(enemy);
    }
}
