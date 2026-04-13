using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct AnimalType
    {
        public string name;         
        public GameObject prefab;   
        public int amount;          
    }

    public List<AnimalType> animalsToSpawn;

    [Header("Setări Harta")]
    public Vector2 mapSize = new Vector2(1000, 1000); 
    public LayerMask groundLayer; 

    [Header("Reguli de Plasare")]
    [Tooltip("Animalele stau de obicei mai jos, nu pe vârful muntelui")]
    public float maxAltitude = 50f; 
    
    [Tooltip("Animalele preferă zone mai plate")]
    [Range(0, 45)]
    public float maxSlopeAngle = 20f;

    [Header("Variație Mărime")]
    [Tooltip("Cât de mult poate varia mărimea? (0.8 înseamnă între 80% și 100%)")]
    public float minScalePercent = 0.8f; 
    public float maxScalePercent = 1.0f;

    void Start()
    {
        SpawnAllAnimals();
    }

    void SpawnAllAnimals()
    {
        foreach (var animal in animalsToSpawn)
        {
            SpawnBatch(animal);
        }
    }

    void SpawnBatch(AnimalType animal)
    {
        
        GameObject parentFolder = new GameObject(animal.name + "_Container");

        int spawnedCount = 0;
        int attempts = 0;

        while (spawnedCount < animal.amount)
        {
            attempts++;
            if (attempts > 10000) 
            {
                Debug.LogWarning($"Nu am putut spawna toate {animal.name}. Doar {spawnedCount} au fost puse.");
                break; 
            }

            Vector3 candidatePosition = GetRandomPointOnMap();

            if (IsValidTerrain(candidatePosition, out Vector3 groundHitPoint, out Vector3 groundNormal))
            {
                if (NavMesh.SamplePosition(groundHitPoint, out NavMeshHit navHit, 2.0f, NavMesh.AllAreas))
                {
                    Vector3 finalPos = navHit.position;
                    
                    
                    Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

                    
                    GameObject newAnimal = Instantiate(animal.prefab, finalPos, randomRot, parentFolder.transform);
                    
                    
                    
                    float randomScaleFactor = Random.Range(minScalePercent, maxScalePercent);
                    
                    
                    Vector3 originalScale = animal.prefab.transform.localScale;
                    newAnimal.transform.localScale = originalScale * randomScaleFactor;

                    spawnedCount++;
                }
            }
        }
        Debug.Log($"Spawnat Animale: {spawnedCount} x {animal.name}");
    }

    Vector3 GetRandomPointOnMap()
    {
        float x = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float z = Random.Range(-mapSize.y / 2, mapSize.y / 2);
        
        return transform.position + new Vector3(x, 200f, z); 
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
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, 50, mapSize.y));
    }
}