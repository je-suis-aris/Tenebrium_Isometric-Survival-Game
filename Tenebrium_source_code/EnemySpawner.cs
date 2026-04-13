using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        public string name;         
        public GameObject prefab;   
        public int amount;          
    }

    
    public List<EnemyType> enemiesToSpawn;

    [Header("Setari Harta")]
    public Vector2 mapSize = new Vector2(1000, 1000); 
    public LayerMask groundLayer; 

    [Header("Reguli de Plasare")]
    [Tooltip("Inamicii nu se spawneaza pe varful muntelui")]
    public float maxAltitude = 50f; 

    [Tooltip("Inamicii nu apar pe pante abrupte")]
    [Range(0, 45)]
    public float maxSlopeAngle = 20f;

    void Start()
    {
        SpawnAllEnemies();
    }

    void SpawnAllEnemies()
    {
        foreach (var enemy in enemiesToSpawn)
        {
            SpawnBatch(enemy);
        }
    }

    void SpawnBatch(EnemyType enemyData)
    {
        
        GameObject parentFolder = new GameObject(enemyData.name + "_Container");

        int spawnedCount = 0;
        int attempts = 0;

        while (spawnedCount < enemyData.amount)
        {
            attempts++;
            
            if (attempts > 10000) 
            {
                Debug.LogWarning($"Nu am putut spawna toti {enemyData.name}. Doar {spawnedCount} au fost pusi.");
                break; 
            }

            
            Vector3 candidatePosition = GetRandomPointOnMap();

            
            if (IsValidTerrain(candidatePosition, out Vector3 groundHitPoint))
            {
                
                
                if (NavMesh.SamplePosition(groundHitPoint, out NavMeshHit navHit, 5.0f, NavMesh.AllAreas))
                {
                    Vector3 finalPos = navHit.position;

                    
                    Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

                    
                    GameObject newEnemy = Instantiate(enemyData.prefab, finalPos, randomRot, parentFolder.transform);
                    
                    
                    newEnemy.transform.localScale = enemyData.prefab.transform.localScale;

                    spawnedCount++;
                }
            }
        }
        Debug.Log($"Spawnat Inamici: {spawnedCount} x {enemyData.name}");
    }

    Vector3 GetRandomPointOnMap()
    {
        float x = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float z = Random.Range(-mapSize.y / 2, mapSize.y / 2);
        
        
        return transform.position + new Vector3(x, 200f, z); 
    }

    bool IsValidTerrain(Vector3 originHigh, out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;
        Ray ray = new Ray(originHigh, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300f, groundLayer))
        {
            
            if (hit.point.y > maxAltitude) return false;

            
            float slope = Vector3.Angle(hit.normal, Vector3.up);
            if (slope > maxSlopeAngle) return false;

            hitPoint = hit.point;
            return true;
        }
        return false;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, 50, mapSize.y));
    }
}