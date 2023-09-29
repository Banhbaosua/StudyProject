using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GiraffeGame : MonoBehaviour
{
    public Transform Anchor;
    public Transform Anchor2;
    GameObject SelectedObject;
    public GameObject GiraffeNeck;
    RaycastHit2D hit;
    bool isDragging = false;
    Vector3 offset;
    private Vector3 Initialscale;
    private Vector3 InitialAnchor;
    float scaleHeight;
    float temp;
    // Start is called before the first frame update
    void Start()
    {
        InitialAnchor=Anchor2.position; 
        Initialscale = GiraffeNeck.transform.localScale;
        scaleHeight = (Initialscale.y/Initialscale.x);
        temp = Initialscale.y/37.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(mousePos(),Vector2.zero);
            if(hit.collider!=null && hit.collider.tag=="Player")
            {
                SelectedObject = hit.collider.gameObject;
                isDragging = true;
                offset = SelectedObject.transform.position - mousePos();
            }
        }

        if(isDragging)
        {
            
            SelectedObject.transform.position = mousePos() + offset;
            ChangeTransform();
        }
    
        if(Input.GetMouseButtonUp(0))
            isDragging = false;
    }

    public Vector3 mousePos()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouse;
    }

    public void ChangeTransform()
    {
        Vector3 pos = mousePos();
        //change position
        Vector3 MiddlePoint = (Anchor.position + Anchor2.position)/2f;
        GiraffeNeck.transform.position = MiddlePoint;
        //rotate
        Vector3 RotationDir = Anchor2.position - Anchor.position;
        GiraffeNeck.transform.up = RotationDir;
        //change scale
        float scaleMulti = Vector3.Distance(Anchor2.position,Anchor.position)/Vector3.Distance(InitialAnchor,Anchor.position);
        Debug.Log(Vector3.Distance(Anchor2.position,Anchor.position));
        //GiraffeNeck.transform.localScale = new Vector3(Initialscale.x,Initialscale.y*scaleMulti,0);
        GiraffeNeck.GetComponent<SpriteRenderer>().size = new Vector3(1,Vector3.Distance(Anchor2.position,Anchor.position)/temp,0);
    }
}
