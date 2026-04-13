using UnityEngine;

public class CurePotion : MonoBehaviour
{
    public float speed = 15f; 
    public float curePower = 25f; 
    public GameObject hitEffect; 

    private Transform targetEnemy; 

    
    public void SeekTarget(Transform _target)
    {
        targetEnemy = _target;
        Destroy(gameObject, 5f); 
    }

    void Update()
    {
        
        if (targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        
        Vector3 targetPos = targetEnemy.position + Vector3.up;
        
        
        Vector3 dir = targetPos - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetPos);
    }

    void HitTarget()
    {
        
        EnemyManager enemy = targetEnemy.GetComponent<EnemyManager>();
        if (enemy != null)
        {
            enemy.TakeCure(curePower);
        }

        
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        
        Destroy(gameObject);
    }
}