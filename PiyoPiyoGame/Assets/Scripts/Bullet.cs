using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Player player;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                //col.GetComponent<Player>().Damage(1);
                //StartCoroutine(player.Knockback(0.02f, 50, player.transform.position));
            }

            // n 秒後に消滅する
            Invoke("DestroyObj", 0.7f);
        }
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }


}
