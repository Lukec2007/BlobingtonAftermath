using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    float currentTimeBetweenSpawns;

    Transform enemiesParent;

    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
    }
    void Update()
    {
        currentTimeBetweenSpawns -= Time.deltaTime;

        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }

        Vector2 RandomPosition()
        {
            return new Vector2(Random.Range(-16, 16), Random.Range(-8, 8));
        }

        void SpawnEnemy()
        {
            var e = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
            e.transform.SetParent(enemiesParent);
        }

    }

    private void DestroyAllEnemies()
    {
        foreach (Transform e in enemiesParent)
            Destroy(e.gameObject);
    }

}
