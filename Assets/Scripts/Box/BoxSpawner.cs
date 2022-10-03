using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private GameObject box;
    private float randomSpawnPoint;
    void Start()
    {
        StartCoroutine(SpawnBox());
    }
    private IEnumerator SpawnBox()
    {
        while (true)
        {
            int randAngle = Random.Range(0, 2);
            yield return new WaitForSeconds(spawnTime);
            float halfLength = GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float spawnPos = transform.position.x;
            randomSpawnPoint = Random.Range(spawnPos - halfLength, spawnPos + halfLength);
            
            Instantiate(box, new Vector2(randomSpawnPoint, transform.position.y), Quaternion.Euler(new Vector3(0, randAngle * 180, 0)));
        }
    }
}
