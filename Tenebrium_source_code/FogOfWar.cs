using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [Header("Setari Principale")]
    public Transform player;      
    public LayerMask fogLayer;    
    public float radius = 8f;     
    
    [Header("Calitate")]
    public int resolution = 1024; 
    public Color fogColor = new Color(0.1f, 0.1f, 0.1f, 1f); 

    private Texture2D fogTexture;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
        
        fogTexture = new Texture2D(resolution, resolution);
        
        
        Color[] pixels = new Color[resolution * resolution];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = fogColor;
        }
        fogTexture.SetPixels(pixels);
        fogTexture.Apply();

        
        rend.material.mainTexture = fogTexture;

        
        InvokeRepeating("UpdateFog", 0.05f, 0.05f);
    }

    void UpdateFog()
    {
        if (player == null) return;

        
        
        
        Vector3 skyPosition = new Vector3(player.position.x, 500f, player.position.z);
        Ray ray = new Ray(skyPosition, Vector3.down);
        
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, 1000f, fogLayer))
        {
            
            Vector2 uv = hit.textureCoord;

            int centerX = Mathf.FloorToInt(uv.x * resolution);
            int centerY = Mathf.FloorToInt(uv.y * resolution);

            
            
            float planeSizeX = hit.transform.lossyScale.x * 10f; 
            int pixelRadius = Mathf.RoundToInt((radius / planeSizeX) * resolution);

            
            for (int x = -pixelRadius; x <= pixelRadius; x++)
            {
                for (int y = -pixelRadius; y <= pixelRadius; y++)
                {
                    float dist = Mathf.Sqrt(x*x + y*y);
                    
                    if (dist < pixelRadius)
                    {
                        int drawX = centerX + x;
                        int drawY = centerY + y;

                        
                        if (drawX >= 0 && drawX < resolution && drawY >= 0 && drawY < resolution)
                        {
                            
                            float alpha = dist / pixelRadius;
                            
                            
                            alpha = alpha * alpha * (3f - 2f * alpha);

                            Color current = fogTexture.GetPixel(drawX, drawY);
                            
                            
                            
                            if (alpha < current.a)
                            {
                                fogTexture.SetPixel(drawX, drawY, new Color(fogColor.r, fogColor.g, fogColor.b, alpha));
                            }
                        }
                    }
                }
            }
            
            fogTexture.Apply();
        }
    }
}