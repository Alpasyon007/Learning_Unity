using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawn = false;
    [SerializeField]
    private GameObject[] _powerUp;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnRoutine() {
        while (!_stopSpawn) {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerup() {
        while(!_stopSpawn) {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject tripleShot = Instantiate(_powerUp[Random.Range(0, 3)], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath() {
        _stopSpawn = true;
    }
}
