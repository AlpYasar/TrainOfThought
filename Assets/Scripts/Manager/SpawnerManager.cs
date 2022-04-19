using System.Collections;
using PoolSystem;
using UnityAtoms;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using DG.Tweening;
using Train;
using NaughtyAttributes;
using PathCreation;
using PathEnds;

namespace Manager
{
    public class SpawnerManager : MonoBehaviour
    {
        private ObjectsPool pool;
        
        [SerializeField, BoxGroup("Spawner Params")] private GameObject[] prefabs;
        [SerializeField, BoxGroup("Spawner Params")] private IntVariable maxSpawnCount;
        [SerializeField, BoxGroup("Spawner Params")] private IntVariable spawnedCount;  
        [SerializeField, BoxGroup("Spawner Params")] private ClampFloat spawnDelay;
        [SerializeField, BoxGroup("Spawner Params"), NaughtyAttributes.ReadOnly] private float delay;
        
        [SerializeField, BoxGroup("Path Params")] private PathEnd firstFork;
        [SerializeField, BoxGroup("Path Params")] private PathCreator firstPath;
        
        
        private void Start()
        {
            pool = ObjectsPool.Instance;
        } 
        
        [Button("Spawn")]
        public void StartSpawning()
        {
            StartCoroutine(Spawn());
        }
        
        private IEnumerator Spawn()
        {
            while (spawnedCount.Value < maxSpawnCount.Value)
            {
                delay = Random.Range(spawnDelay.Min, spawnDelay.Max);
                var prefab = prefabs[Random.Range(0, prefabs.Length)];
                var obj = pool.GetFromPool(prefab);
                obj.GetComponent<TrainMovementController>().SpawnConstructor(firstFork, firstPath);
                
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                
                
                obj.SetActive(true);
                spawnedCount.Value++;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}