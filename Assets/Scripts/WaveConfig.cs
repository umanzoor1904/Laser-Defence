using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnTime = 0.5f;
    [SerializeField] float spawnRandom = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;


    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWayPoint()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
    public float GetSpawnTime()
    {
        return spawnTime;
    }
    public float GetSpawnRandom()
    {
        return spawnRandom;
    }
    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

}
