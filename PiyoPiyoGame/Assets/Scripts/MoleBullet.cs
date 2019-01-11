using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBullet : MonoBehaviour {

    private Player player;

    public float BulletLifeTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // n 秒後に消滅する
        Destroy(gameObject, BulletLifeTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                Destroy(gameObject);
                col.GetComponent<Player>().Damage(1);
                StartCoroutine(player.Knockback(0.02f, 50, player.transform.position));
            }
        }
    }
}