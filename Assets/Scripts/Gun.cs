using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform b_Spawn;
    [SerializeField] Transform aim;
    [SerializeField] GameObject gun;
    public Animator animator;
    float shootDelay;
    public float startShootDelay;

    [System.Serializable]
    public class pool   {
        public string tag;
        public GameObject bullet;
        public int size;
    }

    public List<pool> pools;
    public Dictionary<string,Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string,Queue<GameObject>>();

        foreach(pool pool in pools) {
            Queue<GameObject> objqueue = new Queue<GameObject>();

            for(int i = 0; i<pool.size; i++)    {
                GameObject obj = Instantiate(pool.bullet);
                obj.SetActive(false);
                objqueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objqueue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotateGun();
        shotHandle();
    }

    private GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation)   {
        GameObject gameobj = poolDictionary[tag].Dequeue();

        Vector3 direction = (position - aim.position).normalized;

        gameobj.SetActive(true);
        gameobj.transform.position = position;
        gameobj.GetComponent<Rigidbody2D>().AddForce(direction*50f,ForceMode2D.Impulse);
        gameobj.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(gameobj);

        return gameobj;
    }

    private void shotHandle()   {
        if(shootDelay <=.1f)  {
            if(Input.GetMouseButtonDown(0)) {
                animator.SetTrigger("Shot");
                spawnFromPool("Bullet1",b_Spawn.position,b_Spawn.rotation);
                shootDelay = startShootDelay;
            }
        }

        else
            shootDelay-= Time.deltaTime;

    }

    public Vector3 mousePos()  {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void rotateGun()    {
        var m_Pos = mousePos(); 
        Vector3 direction = (m_Pos - aim.position).normalized;
        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        aim.eulerAngles = new Vector3(0,0,angle);

        
        if(m_Pos.x > transform.position.x)    {
            //scale robot
            Vector3 scale = transform.localScale;
            if(scale.x < 0)
                scale.x*=-1;
            transform.localScale = scale;

            //scale gun
            Vector3 scaleGun = aim.localScale;
            if(scaleGun.x < 0)
                scaleGun.x*=-1;
            if(scaleGun.y < 0)
                scaleGun.y*=-1;
            aim.localScale = scaleGun;
        }

        if(m_Pos.x < transform.position.x)    {
            //scale robot
            Vector3 scale = transform.localScale;
            if(scale.x > 0)
                scale.x*=-1;
            transform.localScale = scale;

            //scale gun
            Vector3 scaleGun = aim.localScale;
            if(scaleGun.x > 0)
                scaleGun.x*=-1;
            if(scaleGun.y > 0)
                scaleGun.y*=-1;
            aim.localScale = scaleGun;
        }
    }
}
