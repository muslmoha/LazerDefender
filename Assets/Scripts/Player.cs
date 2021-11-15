using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config Params
    [Header("Player")]
    [Range(10f, 100f)] [SerializeField] float xSpeed = 10f;
    [Range(10f, 100f)] [SerializeField] float ySpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField][Range(0, 1)] float deathVol= 0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField][Range(0, 1)] float shootVol = 0.5f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))//get button down will relate w/e is in input manager under "Fire1" to the inpute
        {                                 //GetKeyDown requires a specific key to be pressed
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //StopAllCoroutines(); Stops ALL coroutines
            StopCoroutine(firingCoroutine); //stops specific coroutine
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true) //hold down to constantly fire
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;//identity means use current rotation
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootVol);                            //as GameObject is overtly making it a game object, else will be Obj
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    { 
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * xSpeed;//finds all movement based in the horizontal axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * ySpeed;


        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);//get change of position
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);//update it
    }

    private void SetMoveBoundaries()
    {

        Camera gameCamera = Camera.main; //grabbing camera object
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding; //gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x 
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding; //translates the position of the camera position in Unity to a boundary where min = 0, max = 1
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        //if (other.tag != "Player") a novel solution but gonna use layers
        if (!damageDealer) { return; }//protects if no damage dealer
         ProcessHit(damageDealer);

    }


    public float GetHealth()
    {
        return health;
    }
    private void ProcessHit(DamageDealer damage)
    {
        health -= damage.GetDamage();
        damage.Hit();
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVol);
        FindObjectOfType<Level>().LoadGameOverScene();
        Destroy(gameObject);      
    }
}
