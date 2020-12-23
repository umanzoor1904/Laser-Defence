using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.8f;
    [SerializeField] int health = 500;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootVolume = 0.25f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float attackSpeed = 0.1f;
    public int guns = 1;
    private Vector3 rightGun;
    private Vector3 leftGun;
    private Vector3 centerGun;

    Coroutine firing;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    public static Player instance;


    // Use this for initialization
    void Start () {
        SetBoundary();
        
	}



    // Update is called once per frame
    void Update () {
        Move();
        Fire();
		
	}

    IEnumerator holdFire()
    {
        while (true)
        {
            if (guns == 1)
            {
                centerGun = new Vector3(transform.position.x, transform.position.y + 1f);
                GameObject laser = Instantiate(laserPrefab, centerGun, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                yield return new WaitForSeconds(attackSpeed);
            }
            else if (guns == 2)
            {
                leftGun = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f);
                rightGun = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f);
                GameObject laser = Instantiate(laserPrefab, rightGun, Quaternion.identity) as GameObject;
                GameObject laser2 = Instantiate(laserPrefab, leftGun, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                yield return new WaitForSeconds(attackSpeed);
            }
            else if (guns == 3)
            {
                centerGun = new Vector3(transform.position.x, transform.position.y + 1f);
                leftGun = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f);
                rightGun = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f);
                GameObject laser = Instantiate(laserPrefab, rightGun, Quaternion.identity) as GameObject;
                GameObject laser2 = Instantiate(laserPrefab, leftGun, Quaternion.identity) as GameObject;
                GameObject laser3 = Instantiate(laserPrefab, centerGun, Quaternion.identity) as GameObject;
                laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
                yield return new WaitForSeconds(attackSpeed);
            }

        }
        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firing = StartCoroutine(holdFire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firing);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetBoundary()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        //SceneManager.LoadScene("GameOver");
    }

    public void SetGuns(int x)
    {
        this.guns = x;
    }

    public int GetHealth()
    {
        return health;
    }
}
