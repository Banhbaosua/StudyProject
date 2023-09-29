using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRespawn : MonoBehaviour
{
    public float respawn=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() 
    {
        Invoke("disableBullet",respawn);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void disableBullet()
    {
        gameObject.SetActive(false);
    }
}
