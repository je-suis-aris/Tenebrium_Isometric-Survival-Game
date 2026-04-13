using UnityEngine;

public class MysticalFloater : MonoBehaviour
{
    [Header("Setări Levitație")]
    [Tooltip("Cât de sus/jos se mișcă obiectul")]
    public float floatAmplitude = 0.3f; 
    
    [Tooltip("Cât de repede face mișcarea sus-jos")]
    public float floatSpeed = 1.5f;     

    [Header("Setări Rotație")]
    [Tooltip("Viteza de rotire (grade pe secundă)")]
    public float rotationSpeed = 50f;   

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        
        startPos = transform.position;

        
        
        
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        
        
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatAmplitude;

        
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}