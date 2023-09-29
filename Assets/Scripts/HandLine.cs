using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandLine : MonoBehaviour
{
    [SerializeField] GameObject PlayerAnchor;
    [SerializeField] GameObject PlayerHandAnchor;
//[SerializeField] GameObject Hand;
    [SerializeField] LineRenderer lineRenderer;
    List<Vector2> linePoint = new List<Vector2>();
    Vector2 DirectionToPlayer,DirectionToLastPoint,originalHand;
    Rigidbody2D rb2D;
    int check;
    float speed=10f;
    bool dragCheck;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        originalHand = PlayerHandAnchor.transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        AddPointToLine(PlayerAnchor.transform.position);
        rb2D = PlayerHandAnchor.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLinePoint();
        PutHandToLastPoint();
        RotateHand(PlayerHandAnchor.transform.position);
        Drag();
        //LineHit();
        Debug.Log(lineRenderer.positionCount);
    }

    //Line hit point
    private void LineHit()
    {
        RaycastHit2D hit=Physics2D.Linecast(PlayerHandAnchor.transform.position, lineRenderer.GetPosition(lineRenderer.positionCount-2));
        if(hit.collider!=null && hit.collider.tag=="Object")
            {
                if(Vector2.Distance(hit.point,PlayerHandAnchor.transform.position)!= Vector2.Distance(PlayerHandAnchor.transform.position,lineRenderer.GetPosition(linePoint.Count-2)))
                {
                    linePoint.RemoveAt(linePoint.Count-1);
                    AddPointToLine(hit.point);
                }
            }
    }

    void AddPointToLine(Vector3 point)
    {
        linePoint.Add(point);
        linePoint.Add(PlayerHandAnchor.transform.position);
    }

    private void UnWrapLine()
    {
        Vector2 Player = PlayerHandAnchor.transform.position;
        Vector2 LastPoint = lineRenderer.GetPosition(lineRenderer.positionCount-2);
        Vector2 FixedPoint = lineRenderer.GetPosition(lineRenderer.positionCount-3);//chỉ dùng fixedpoint
        //DirectionToPlayer = Player - FixedPoint;
        //DirectionToLastPoint = LastPoint - FixedPoint;
        //RaycastHit2D hitLast = Physics2D.Linecast(Player,LastPoint);//dont use
        RaycastHit2D hitFixed = Physics2D.Linecast(Player,FixedPoint);
        if(lineRenderer.positionCount==3)
        {
            if(hitFixed.collider==null)
            {
                    linePoint.RemoveAt(linePoint.Count-2);
            }
        }

        if(lineRenderer.positionCount>3)
        {
            RaycastHit2D hit = Physics2D.Raycast(Player,FixedPoint-Player);
            if(hit.point==FixedPoint)
            {
                linePoint.RemoveAt(linePoint.Count-2);
            }
        }
    }
    //vị trí tay luôn ở cuối
    void PutHandToLastPoint()
    {
        lineRenderer.SetPosition(lineRenderer.positionCount-1,PlayerHandAnchor.transform.position);
    }

    void UpdateLinePoint()
    {
        lineRenderer.positionCount = linePoint.Count;
        for(int i = linePoint.Count-1;i>=0;i--)
        {
            lineRenderer.SetPosition(i,linePoint[i]);
        }
    }

    public Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void RotateHand(Vector3 p)
    {
        Vector3 dir = p-lineRenderer.GetPosition(linePoint.Count-2);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        PlayerHandAnchor.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }

    void Drag()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(MousePos(),Vector2.zero);
            if(hit.collider!=null && hit.collider.tag=="Hand")
            {
                dragCheck = true;
                PlayerHandAnchor.GetComponent<PolygonCollider2D>().isTrigger = false;
                //offset = hit.collider.gameObject.transform.position - MousePos();//tính lại
                ////offset.z = 0;
            }
        }

        if(dragCheck)
        {
            LineHit();
            if(lineRenderer.positionCount>2)
                UnWrapLine();
            
            var pos = MousePos();
            pos.z =0;
            var posPlayer = PlayerHandAnchor.transform.position;
            posPlayer.z = 0;
            rb2D.velocity = (pos - posPlayer)*3000*Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(0))
            dragCheck=false;
        
        if(!dragCheck)
        {
            rb2D.velocity = (lineRenderer.GetPosition(linePoint.Count-2)-PlayerHandAnchor.transform.position)*0;
            PlayerHandAnchor.GetComponent<PolygonCollider2D>().isTrigger = true;
            if(linePoint.Count>2)
            {
                var pos = PlayerHandAnchor.transform.position;
                pos.z =0;
                for(int i = linePoint.Count;i>2;i--)
                {
                    PlayerHandAnchor.transform.position = Vector2.MoveTowards(PlayerHandAnchor.transform.position,lineRenderer.GetPosition(linePoint.Count-2),speed*Time.deltaTime);
                    if(PlayerHandAnchor.transform.position == lineRenderer.GetPosition(linePoint.Count-2))
                        linePoint.RemoveAt(linePoint.Count-2);
                }
            }
            if(linePoint.Count ==2)
            {
                var pos = PlayerHandAnchor.transform.position;
                pos.z =0;
                PlayerHandAnchor.transform.position = Vector2.MoveTowards(PlayerHandAnchor.transform.position,originalHand,speed*Time.deltaTime);
            }
        }
    }
}
