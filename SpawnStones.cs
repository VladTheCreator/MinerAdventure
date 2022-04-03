using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStones : MonoBehaviour
{
    [SerializeField] private Stone stonePrefab;
    private float timeToSpawn;
    [SerializeField] private float startTimeToSpawn;
    private void Start()
    {
        timeToSpawn = startTimeToSpawn;
    }

    void Update()
    {
        if (timeToSpawn <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(stonePrefab, new Vector3(transform.position.x, 
                    transform.position.y + i), Quaternion.identity);
            }
            timeToSpawn = startTimeToSpawn;
        }
        else
        {
            timeToSpawn -= Time.deltaTime; 
        }
    }
}
