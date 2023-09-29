using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rb;
    bool chasing,patroling,leftSide;
    BoxCollider2D triggerArea;
    CapsuleCollider2D robotCollider;
    float moveSpeed;
    [SerializeField] GameObject player;

    Vector3 patrolpos;

    [SerializeField] Transform patrolMin,patrolMax;
    // Start is called before the first frame update

    private enum State
    {
        patroling,
        chasing,
        attacking,
    }

    private State state;

    void Start()
    {
        moveSpeed = 5f;
        rb = GetComponent<Rigidbody2D>();
        state = State.patroling;
        triggerArea = GetComponent<BoxCollider2D>();
        robotCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        stateHandle();
        //Debug.Log(Vector3.Distance(transform.position,patrolMax.position));
        switch(state)   {
            default:
            case State.patroling:
                patrol();
                break;

            case State.chasing:
                Chase();
                break;

            case State.attacking:
                Attack();
            break;
        }
    }

    private void Move(float speed)  {
        rb.velocity = new Vector3(speed,rb.velocity.y,0);
    }

    private void patrol()   {
        if(Vector3.Distance(transform.position,patrolMin.position)<.1f) {
            if(moveSpeed <0)
                moveSpeed *=-1;
            facingCheck(this.gameObject,patrolMax.position);
        }

        if(Vector3.Distance(transform.position,patrolMax.position)<.1f) {
            if(moveSpeed > 0)
                moveSpeed *= -1;
            facingCheck(this.gameObject,patrolMin.position);
        }

        if(transform.position.x > patrolMax.position.x) {
            facingCheck(this.gameObject,patrolMax.position);
            if(moveSpeed > 0)
                moveSpeed *= -1;
        }

        if(transform.position.x < patrolMin.position.x) {
            facingCheck(this.gameObject,patrolMin.position);
            if(moveSpeed <0)
                moveSpeed *=-1;
        }
        Move(moveSpeed);
    }

    private void facingCheck(GameObject objectscale, Vector3 facingPoint)   {
        if(objectscale.transform.position.x < facingPoint.x)    {
            leftSide = true;
            Vector3 scale = objectscale.transform.localScale;
            if(scale.x <0)
                scale.x*=-1;
            objectscale.transform.localScale = scale;
        }

        if(objectscale.transform.position.x > facingPoint.x)    {
            leftSide=false;
            Vector3 scale = objectscale.transform.localScale;
            if(scale.x >0)
                scale.x*=-1;
            objectscale.transform.localScale = scale;
        }
    }

    private void Chase()
    {
        facingCheck(this.gameObject,player.transform.position);
        if(leftSide)
        {
            moveSpeed = 5f;
        }

        if(!leftSide)
        {
            moveSpeed = -5f;
        }

        float Distance = Vector2.Distance(transform.position,player.transform.position);
        if(Distance<5f)
            moveSpeed = 0;
            state = State.attacking;
        
        Move(moveSpeed);
    }

    private void Attack()   {
        //attack trigger
    }

    private void stateHandle() {
        if(player.transform.position.x > triggerArea.bounds.min.x && player.transform.position.x < triggerArea.bounds.max.x)
            state = State.chasing;

        if(player.transform.position.x < triggerArea.bounds.min.x || player.transform.position.x > triggerArea.bounds.max.x)
            state = State.patroling;

    }

}
