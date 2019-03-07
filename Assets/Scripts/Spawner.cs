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
        foreach (GameObject enemy in enemies)
        {
            string m_ClipName = enemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (m_ClipName == "fall")
            {
                enemies.Remove(enemy);
                StartCoroutine("Despawn", enemy);
            }

        }
    }
        

    IEnumerator Despawn(GameObject enemy)
    {
        
        for (float temp = 3; temp > Time.deltaTime; temp -= Time.deltaTime)
        {
            yield return null;
        }
        Destroy(enemy);
    }
}
