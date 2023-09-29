using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawnArea;
    [SerializeField] GameObject prefabObject;
    BoxCollider2D spawnCollider;
    float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        spawnCollider = spawnArea.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float spawnX = Random.Range(spawnCollider.bounds.min.x,spawnCollider.bounds.max.x);
        float spawnY = Random.Range(spawnCollider.bounds.min.y,spawnCollider.bounds.max.y);
        Vector3 randomPoint = new Vector3(spawnX,spawnY,0);

        if(timeCount < Time.time)
        {
            GameObject newPoint;
            newPoint = Instantiate(prefabObject);
            newPoint.transform.position = randomPoint;
            timeCount = Time.time + 2f;
        }
    }
}
