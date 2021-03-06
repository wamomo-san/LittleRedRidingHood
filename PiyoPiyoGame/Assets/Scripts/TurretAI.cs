﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
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

    // Booleans
    public bool awake = false;
    public bool lookingRight = true;

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
        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

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
        }

        if (distance >= wakeRange)
        {
            awake = false;
        }
    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position;
            direction.Normalize();

            if (!attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                Rigidbody2D bc = bulletClone.GetComponent<Rigidbody2D>();

                // bulletClone.GetComponent<Rigidbody2D>().velocity = direction * -bulletSpeed;
                bc.AddForce(new Vector2(direction.x * -bulletSpeed, direction.y * bulletSpeed * 1.5f));
                bulletTimer = 0;
            }

            if (attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                Rigidbody2D bc = bulletClone.GetComponent<Rigidbody2D>();

                // bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                bc.AddForce(new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed * 1.5f));

                bulletTimer = 0;
            }
        }
    }

}
