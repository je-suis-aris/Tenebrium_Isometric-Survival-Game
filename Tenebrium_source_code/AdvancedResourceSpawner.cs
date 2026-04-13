using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedResourceSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct ResourceType
    {
        public string name;
        public GameObject prefab;
        public int amount;
        public float yOffset; 
    }

    public List<ResourceType> resourcesToSpawn;

    [Header("Setari Harta")]
    public Vector2 mapSize = new Vector2(1000, 1000); 
    public LayerMask groundLayer; 

    [Header("Reguli de Plasare")]
    public float maxAltitude = 60f; 
    [Range(0, 45)]
    public float maxSlopeAngle = 20f;

    void Start()
    {
        SpawnAllResources();
    }

    void SpawnAllResources()
    {
        foreach (var resource in resourcesToSpawn)
        {
            SpawnBatch(resource);
        }
    }

    void SpawnBatch(ResourceType resource)
    {
        GameObject parentFolder = new GameObject(resource.name + "_Container");

        int spawnedCount = 0;
        int attempts = 0;

        while (spawnedCount < resource.amount)
        {
            attempts++;
            if (attempts > 10000) break; 

            Vector3 candidatePosition = GetRandomPointOnMap();

            
            if (IsValidTerrain(candidatePosition, out Vector3 groundHitPoint, out Vector3 groundNormal))
            {
                
                if (NavMesh.SamplePosition(groundHitPoint, out NavMeshHit navHit, 2.0f, NavMesh.AllAreas))
                {
                    Vector3 finalPos = navHit.position;
                    
                    
                    finalPos.y += resource.yOffset;

                    Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

                    
                    GameObject newObj = Instantiate(resource.prefab, finalPos, randomRot, parentFolder.transform);
                    
                    
                    
                    newObj.transform.localScale = resource.prefab.transform.localScale;
                    

                    spawnedCount++;
                }
            }
        }
        Debug.Log($"Spawnat: {spawnedCount} x {resource.name}");
    }

    Vector3 GetRandomPointOnMap()
    {
        float x = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float z = Random.Range(-mapSize.y / 2, mapSize.y / 2);
        Vector3 spawnOrigin = transform.position + new Vector3(x, 200f, z);
        return spawnOrigin;
    }

    bool IsValidTerrain(Vector3 originHigh, out Vector3 hitPoint, out Vector3 normal)
    {
        hitPoint = Vector3.zero;
        normal = Vector3.up;

        Ray ray = new Ray(originHigh, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300f, groundLayer))
        {
            if (hit.point.y > maxAltitude) return false;
            
            float slope = Vector3.Angle(hit.normal, Vector3.up);
            if (slope > maxSlopeAngle) return false;

            hitPoint = hit.point;
            normal = hit.normal;
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, 50, mapSize.y));
    }
}