using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeDrag : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject SelectedObject;
    bool isDragging=false;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(mousePos(),Vector2.zero);
            if(hit.collider!=null && hit.collider.tag=="Player")
            {
                SelectedObject = hit.collider.transform.gameObject;
                offset = hit.collider.gameObject.transform.position - mousePos();
                isDragging = true;
            }
        }

        if(isDragging)
            SelectedObject.transform.position = mousePos() + offset;
        
        if(Input.GetMouseButtonUp(0))
            isDragging = false;
    }

    public Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
