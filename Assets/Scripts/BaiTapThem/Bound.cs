using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    Collider2D m_Collider;
    Vector3 m_Size;
    SpriteRenderer spriteRenderer;
    Texture2D texture2D;
    Color[] color;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        var tex = spriteRenderer.sprite.texture;
        texture2D = new Texture2D(tex.width,tex.height,TextureFormat.ARGB32,false);
        texture2D.wrapMode = TextureWrapMode.Clamp;
        texture2D.filterMode = FilterMode.Bilinear;
        color = tex.GetPixels();
        texture2D.SetPixels(color);
        texture2D.Apply();
        //Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;

        //Output to the console the size of the Collider volume
        Debug.Log("Collider Size x: " + m_Size.x);
        Debug.Log("Collider Size y: " + m_Size.y);
        Debug.Log("Texture2D Width: "+texture2D.width);
        Debug.Log("Texture2D Height: " + texture2D.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
