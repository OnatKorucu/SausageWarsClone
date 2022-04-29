using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{

    [SerializeField] private GameObject spawnArea;
    [SerializeField] private Transform spawnOffset;
    [SerializeField] private int startingCollectableCount;
    [SerializeField] private int maximumCollectableCount;
    [SerializeField] private float baseSpawnInterval;
    [SerializeField] private float randomSpawnTimeOffset;
    //[SerializeField] private List<IScoreChanger> collectableTypes;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
