using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minShotDiff = 0.2f;
    [SerializeField] float maxShotDiff = 3f;
    [SerializeField] int scoreValue = 100;

    [Header("Projectile & FX")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 0.2f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField][Range(0,1)] float deathVol = 0.7f;
    [SerializeField][Range(0, 1)] float shootVol = 0.7f;

    private void Start()
    {
        shotCounter = Random.Range(minShotDiff, maxShotDiff);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minShotDiff, maxShotDiff);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootVol);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)//other = whatever hit the enemy object
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }//protects if no damage dealer
        ProcessHit(damageDealer);
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVol);
    }
}
