using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer sr;
    ParticleSystem particle;
    Rigidbody2D rb;
    int hits;
    float timer = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("BulletHits",0));
    }
    private void OnEnable() {
        Invoke("respawnBullet",timer + 1f);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void respawnBullet()    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        particle.Play();
        gameObject.SendMessage("BulletHit",1);
        gameObject.SetActive(false);
    }

    private void BulletHit(int hit) {
        hits = PlayerPrefs.GetInt("BulletHits",0);
        hits +=hit;
        PlayerPrefs.SetInt("BulletHits",hits);
    }
}
