using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    GameObject player;
    public int maxSnowballs=20, snowballCount;
    public List<GameObject> snowballSpawnerList = new List<GameObject>();
    
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();
    public float enemySpawnTimeMin=5, enemySpawnTimeMax=10;
    public float enemySpawnRangeMin=5, enemySpawnRangeMax=10;
    public int maxEnemies=3, playerKills;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(spawningSnowballs());
        StartCoroutine(spawningEnemies());
    }

    IEnumerator spawningSnowballs()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(.5f,1f));

            if(snowballCount<maxSnowballs && snowballSpawnerList.Count>0)
            snowballSpawnerList[Random.Range(0,snowballSpawnerList.Count)].GetComponent<SnowballSpawner>().spawnSnowball();
        }
    }

    IEnumerator spawningEnemies()
    {
        float offsetX, offsetY, offsetZ;

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(enemySpawnTimeMin,enemySpawnTimeMax));

            if(enemyList.Count<maxEnemies)
            {                
                if(Random.Range(1,3)==1) offsetX=Random.Range(enemySpawnRangeMin,enemySpawnRangeMax);
                else offsetX=Random.Range(-enemySpawnRangeMin,-enemySpawnRangeMax);

                offsetY=Random.Range(0,2f);

                if(Random.Range(1,3)==1) offsetZ=Random.Range(enemySpawnRangeMin,enemySpawnRangeMax);
                else offsetZ=Random.Range(-enemySpawnRangeMin,-enemySpawnRangeMax);

                Vector3 spawnPos = new Vector3(player.transform.position.x+offsetX, player.transform.position.y+offsetY, player.transform.position.z+offsetZ);
                
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                enemyList.Add(enemy);
            }
        }
    }
}
