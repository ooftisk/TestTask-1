using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private int _enemyCount = 3;            // Сколько врагов спавнить
    [SerializeField] private float _spawnRadius = 5f; 

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned");
            return;
        }

        for (int i = 0; i < _enemyCount; i++)
        {
            // Случайная позиция внутри круга вокруг спавнера
            Vector2 randomOffset = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.name = $"Enemy_{i + 1}";
        }
    }
}
