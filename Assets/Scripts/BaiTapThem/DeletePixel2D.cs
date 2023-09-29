using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePixel2D : MonoBehaviour
{
    private Texture2D texture2D;
    private Color[] color;
    private RaycastHit2D hit;
    private SpriteRenderer spriteRenderer;
    private GameObject gameObjectDone;
    private bool Drawing = false;
    private Vector2Int lastPos;
    public int erSize;
    private Color zeroAlpha = Color.clear;
    //private GameObject hoanthanh;
    int dem;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //get texture of sprite;
        var tex = spriteRenderer.sprite.texture as Texture2D;
        texture2D = new Texture2D(tex.width,tex.height,TextureFormat.ARGB32,false);
        texture2D.filterMode = FilterMode.Bilinear;
        texture2D.wrapMode = TextureWrapMode.Clamp;
        //get copy of source color data
        color = tex.GetPixels();
        texture2D.SetPixels(color);
        texture2D.Apply();
        spriteRenderer.sprite = Sprite.Create(texture2D,spriteRenderer.sprite.rect,new Vector2(0.5f,0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            if (hit.collider !=null)
            {
                UpdateTexture();
                Drawing = true;
            }
        }
        else   
            Drawing = false;

        if(Input.GetMouseButtonUp(0))
        {
            for(int i = 0; i < color.Length; i++)
            {
                if(color[i] == Color.clear)
                    dem++;
            }
        }
    }

    public void UpdateTexture()
    {
        int w = texture2D.width;
        int h = texture2D.height;
        var MousePos = hit.point - (Vector2)hit.collider.bounds.min;

        MousePos.x *= w/hit.collider.bounds.size.x;
        MousePos.y *= h/hit.collider.bounds.size.y;

        Vector2Int p = new Vector2Int((int)MousePos.x, (int)MousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();

        if(!Drawing)
        {
            lastPos = p;
        }
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - erSize,0,w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - erSize,0,h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + erSize,0,w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + erSize,0,h);

        Vector2 dir = p - lastPos;

        for(int x = start.x; x < end.x; x++)
        {
            for(int y = start.y; y < end.y; y++)
            {
                Vector2 pixel = new Vector2(x,y);
                Vector2 linePos = p;

                if(Drawing)
                {
                    float d = Vector2.Dot(pixel - lastPos, dir)/dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(lastPos,p,d);
                }

                if((pixel - linePos).sqrMagnitude <= erSize*erSize)
                {
                    color[x+y*w] = zeroAlpha;
                }
            }
        }
        
        lastPos = p;
        texture2D.SetPixels(color);
        texture2D.Apply();
        spriteRenderer.sprite = Sprite.Create(texture2D,spriteRenderer.sprite.rect, new Vector2(0.5f,0.5f));
    }
}
