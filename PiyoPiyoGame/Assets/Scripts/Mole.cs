using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    // Integers
    public int curHearlth;
    public int maxHearlth;

    // Floats
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 300;
    public float bulletTimer;
    public float awakeTime = 1.4f;
    public float awaking = 0;

    // Booleans
    public bool awake = false;
    public bool lookingRight = true;
    public bool attack = false;

    // References
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft, shootPointRight;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        curHearlth = maxHearlth;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("awake", awake);
        anim.SetBool("lookingRight", lookingRight);
        anim.SetBool("attack", attack);

        RangeCheck();

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }

        if (target.transform.position.x <= transform.position.x)
        {
            lookingRight = false;
        }
    }


    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakeRange)
        {
            awake = true;
            awaking += Time.deltaTime;
        }

        if (distance >= wakeRange)
        {
            awake = false;
            awaking = 0;
            bulletTimer = 0;
        }
    }

    public void AttackAnim(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval && awaking >= awakeTime)
        {
            attack = true;
            if (attackingRight)
            {
                bulletTimer = 0;
                Invoke("AttackRight", 0.33f);
            }
            if (!attackingRight)
            {
                bulletTimer = 0;
                Invoke("AttackLeft", 0.33f);
            }
        }
    }

    void AttackRight()
    {
        Vector2 direction = target.transform.position;
        direction.Normalize();

        GameObject bulletClone;

        bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
        bulletClone.transform.localScale = new Vector3(-0.19f, 0.19f, 1);
        Rigidbody2D bulletclone = bulletClone.GetComponent<Rigidbody2D>();
        bulletclone.AddForce(new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed * 1.5f));
        attack = false;
    }
    
    void AttackLeft()
    {
        Vector2 direction = target.transform.position;
        direction.Normalize();

        GameObject bulletClone;
        bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
        Rigidbody2D bulletclone = bulletClone.GetComponent<Rigidbody2D>();
        bulletclone.AddForce(new Vector2(direction.x * -bulletSpeed, direction.y * bulletSpeed * 1.5f));
        attack = false;
    }
        
}
