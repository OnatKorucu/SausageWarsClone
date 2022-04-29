using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject spawnArea;
    //[SerializeField] private Vector3 spawnOffset;//TODO: Clean
    [SerializeField, Range(1, 10)] private int startingCollectableCount;
    [SerializeField, Range(1, 20)] private int maximumCollectableCount;
    private volatile int collectableCountInGame;
    
    [SerializeField, Range(0, Mathf.Infinity)] private float baseSpawnInterval;
    [SerializeField, Range(0, Mathf.Infinity)] private float randomSpawnTimeOffset;
    private float nextSpawnTime;
    private float spawnTimeCounter;
    
    [SerializeField] private List<GameObject> collectableTypes;
    
    private Vector3 center;
    private Vector3 size;
    #endregion

    #region LifecycleMethods
    private void OnEnable()
    {
        BaseCollectable.OnCollected += HandleOnCollected;
    }

    private void OnDisable()
    {
        BaseCollectable.OnCollected -= HandleOnCollected;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ClampRandomSpawnTimeOffset();

        InitializeCenterAndSize();

        SpawnInitialCollectables();

        CalculateNextSpawnTime();
    }

    private void LateUpdate()
    {
        spawnTimeCounter += Time.deltaTime;

        if (collectableCountInGame >= maximumCollectableCount) { return; }

        if (spawnTimeCounter < nextSpawnTime) { return; }
        
        GameObject collectableToSpawn = PickRandomCollectable();
        SpawnCollectable(collectableToSpawn);
        spawnTimeCounter = 0;
        CalculateNextSpawnTime();
    }
    #endregion

    #region OtherMethods
    private void HandleOnCollected()
    {
        Debug.Log("xxx HandleOnCollected");

        collectableCountInGame--;
        
        Debug.Log("xxx collectableCountInGame is: " + collectableCountInGame);

    }

    private void ClampRandomSpawnTimeOffset()
    {
        if (randomSpawnTimeOffset > baseSpawnInterval)
        {
            randomSpawnTimeOffset = baseSpawnInterval / 2;
        }
    }
    
    private void InitializeCenterAndSize()
    {
        center = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, spawnArea.transform.position.z);
        size = spawnArea.GetComponent<Renderer>().bounds.size;
    }
    
    private void SpawnInitialCollectables()
    {
        for (int i = 0; i < startingCollectableCount; i++)
        {
            GameObject collectableToSpawn = PickRandomCollectable();
            SpawnCollectable(collectableToSpawn);
        }
    }
    
    private void CalculateNextSpawnTime()
    {
        nextSpawnTime = baseSpawnInterval + (Random.value - 0.5f) * randomSpawnTimeOffset;
        Debug.Log("NextSpawnTime: " + nextSpawnTime);
    }

    private GameObject PickRandomCollectable()
    {
        return collectableTypes.RandomElement();
    }

    private void SpawnCollectable(GameObject collectableToSpawn)
    {
        if (collectableCountInGame >= maximumCollectableCount) { return; }

        GameObject collectableGameObject = Instantiate(collectableToSpawn);
        Debug.Log("xxx SPAWN");

        collectableGameObject.transform.position = PickSpawnPosition(center, size);;
        collectableGameObject.transform.rotation = Quaternion.identity;

        collectableCountInGame++;
        Debug.Log("xxx collectableCountInGame is: " + collectableCountInGame);

    }

    private Vector3 PickSpawnPosition(Vector3 center, Vector3 size)//TODO: Clean
    {
        Vector3 response = center + new Vector3(
            (Random.value - 0.5f) * size.x,
            1,
            (Random.value - 0.5f) * size.z
        );
        Debug.Log("POSITION IS + " + response.x + response.y + response.z);

        return response;
    }
    #endregion

}
