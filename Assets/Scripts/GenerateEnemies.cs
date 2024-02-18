using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemycount;

    void Start()
    {
        
    }

    public void Spawn()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemycount < 3)
        {
            xPos = Random.Range(-11, 20);
            zPos = Random.Range(-1, 28);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            enemycount += 1;
        }
    }
}
