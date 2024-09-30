using System.Collections;
using UnityEngine;

public class SpawnMaker : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    public GameObject[] spawnPrefabsGravity;
    public GameObject[] spawnPosition;

    [SerializeField]
    public float minX;
    public float maxX;
    public float minAngle, maxAngle;
    public float SecondSpawn = 0.5f;
    public int maxGenerate = 100;
    public GameObject heartPrefab;
    public GameObject GuardPrefab;
    void Start()
    {
        StartCoroutine(SpawnPrefabs());
    }

    /*public void SpawnGenerate()
    {
        for (int i = 0; i < maxGenerate; i++)
        {
            GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

            float randomX = Random.Range(minX, maxX);
            float randomAngle = Random.Range(minAngle, maxAngle);
            Vector3 randomPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            //Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);

            GameObject spawnedObject = Instantiate(prefab, randomPosition, Quaternion.Euler(0f, 0f, randomAngle));

            //Instantiate(prefab, randomPosition, randomRotation);

        }
    }*/

    IEnumerator SpawnPrefabs()
    {
        while (true)
        {
            var xposition = Random.Range(minX, maxX);
            var position = new Vector3(xposition, transform.position.y);
            float randomAngle = Random.Range(minAngle, maxAngle);
            Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);
            GameObject gameObject = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)], position, randomRotation);
            yield return new WaitForSeconds(SecondSpawn);
            Destroy(gameObject, 5f);
        }
    }

    public void SpawnPrefabsGravity()
    {
        for (int i = 0; i < 1; i++)
        {
            var position = Random.Range(0, spawnPosition.Length);
            Instantiate(spawnPrefabsGravity[Random.Range(0, spawnPrefabsGravity.Length)], spawnPosition[position].transform.position, Quaternion.identity);
        }
    }

    public void SpawnPrefabsHeart()
    {
        for (int i = 0; i < 1; i++)
        {
            var position = Random.Range(0, spawnPosition.Length);
            Instantiate(heartPrefab, spawnPosition[position].transform.position, Quaternion.identity);
        }
    }

    public void SpawnPrefabsGuard()
    {
        for (int i = 0; i < 1; i++)
        {
            var position = Random.Range(0, spawnPosition.Length);
            Instantiate(GuardPrefab, spawnPosition[position].transform.position, Quaternion.identity);
        }
    }
}
