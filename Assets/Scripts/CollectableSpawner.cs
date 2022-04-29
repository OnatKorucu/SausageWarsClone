using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<GameObject> collectableTypes;

    [SerializeField] private GameObject spawnArea;
    private Vector3 center;
    private Vector3 size;
    
    [SerializeField, Range(1, 10)] private int startingCollectableCount;
    [SerializeField, Range(1, 20)] private int maximumCollectableCount;
    private volatile int collectableCountInGame;
    
    [SerializeField, Range(0, Mathf.Infinity)] private float baseSpawnInterval;
    [SerializeField, Range(0, Mathf.Infinity)] private float randomSpawnTimeOffset;
    private float nextSpawnTime;
    private float spawnTimeCounter;
    #endregion

    #region LifecycleMethods
    // Start is called before the first frame update
    void Start()
    {
        ClampRandomSpawnTimeOffset();

        InitializeCenterAndSize();
        
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
    public void HandleOnCollected()
    {
        collectableCountInGame--;
        
        /*if (collectableCountInGame < 0)
        {
            collectableCountInGame = 0;
        }*/
        
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

    private void CalculateNextSpawnTime()
    {
        nextSpawnTime = baseSpawnInterval + (Random.value - 0.5f) * randomSpawnTimeOffset; //TODO
    }

    private void SpawnCollectable(GameObject collectableToSpawn)
    {
        if (collectableCountInGame >= maximumCollectableCount)
        {
            //collectableCountInGame = maximumCollectableCount;
            return;
        }

        GameObject collectableGameObject = Instantiate(collectableToSpawn);
        Debug.Log("xxx SPAWN");

        collectableGameObject.transform.position = PickSpawnPosition(center, size);;
        collectableGameObject.transform.rotation = Quaternion.identity;

        collectableCountInGame++;

        if (collectableCountInGame >= maximumCollectableCount)
        {
            //collectableCountInGame = maximumCollectableCount;
        }

        Debug.Log("xxx collectableCountInGame is: " + collectableCountInGame);
    }
    
    private GameObject PickRandomCollectable()
    {
        return collectableTypes.RandomElement();
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