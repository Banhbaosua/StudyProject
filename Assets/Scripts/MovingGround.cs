using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    [SerializeField] Transform[] point;
    bool forward;
    float speed = 5f;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,point[i].position) < 0.01f)
        {
            if(i==0)
            {
                forward=true;
            }

            if(i==point.Length-1)
            {
                forward = false;
            }

            if(forward)
            {
                i++;
            }
            
            if(!forward)
            {
                i--;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position,point[i].position,speed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        other.transform.SetParent(null);
    }
}
