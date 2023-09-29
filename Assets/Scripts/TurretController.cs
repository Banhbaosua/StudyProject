using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    bool rotateStatus=true,shootStatus;
    float startTime,startShootTime;
    int shootCount;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject Turret;
    List<GameObject> bullets = new List<GameObject>();
    Rigidbody2D rb;

    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    /*
    public static TurretController Instance;

    private void Awake() 
    {
        Instance = this;
    }
    */
    public List<pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();

            for(int i = 0; i < pool.size;i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectpool);
        }
    
        StartCoroutine(turretShoot());
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateStatus)
            transform.Rotate(new Vector3(0,0,1),30*Time.deltaTime);
    }

    private IEnumerator turretShoot()
    {
        while(true)
        {
            rotateStatus = true;
            yield return new WaitForSeconds(2f);
            rotateStatus = false;
            shootCount = 0;
            while(shootCount < 3)
            {
                yield return new WaitForSeconds(0.3f);
                spawnFromPool("Bullet",bulletSpawn.position,bulletSpawn.rotation);
                shootCount++;
            }
        }
    }

    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject spawnObject = poolDictionary[tag].Dequeue();

        Vector2 Dir = (position - Turret.transform.position).normalized;

        spawnObject.SetActive(true);
        spawnObject.transform.position = position;
        spawnObject.GetComponent<Rigidbody2D>().AddForce(Dir*20f,ForceMode2D.Impulse);
        spawnObject.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(spawnObject);

        return spawnObject;
    }
}
