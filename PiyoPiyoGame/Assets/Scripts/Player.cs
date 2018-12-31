using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    // Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    // Booleans
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    public bool Died = false;

    // Status
    public int curHealth;
    public int maxHealth = 6;


    // References
    private Rigidbody2D rb2d;
    private Animator anim;
    // private gameMaster gm; コインシステム作るのに必要だった
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;

	// Use this for initialization
	void Start () {
        // どのオブジェクトがプレイヤーかを確認する
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        curHealth = maxHealth;
        // gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

	}
	
	// Update is called once per frame
	void Update () {

        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));


        // 左右のスピードによってアニメーションの向きを変更する
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
            facingRight = false;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);
            facingRight = true;
        }

        // ジャンプ / 二段ジャンプ　(Space) の追加
        if (Input.GetButtonDown("Jump") && !wallSliding)
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower / 1.75f);
                }
            }
        }

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Die();
        }

        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if (facingRight && Input.GetAxis("Horizontal") > 0.1f 
                || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }
        }

        if (wallCheck == false || grounded)
        {
            wallSliding = false;
        }
    }


    // 壁キックとずり落ち
    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.5f);

        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-2, 1) * jumpPower);
            }
            else
            {
                rb2d.AddForce(new Vector2(2, 1) * jumpPower);
            }
        }
    }


    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        // h < 0 -> left,  h > 0 -> right
        float h = Input.GetAxis("Horizontal");

        // Fake friction / Easing the x speed of our Player
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        // Moving the Player
        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed / 2) * h);
        }



        
        // 右側のスピード上限の制限
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        // 左側のスピード上限の制限
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    void Die()
    {
        Died = true;


        Invoke("Reload", 1.5f);
    }

    void Reload()
    {
        // Restart
        SceneManager.LoadScene("SampleScene");
        
    }



    public void Damage(int dmg)
    {
        curHealth -= dmg;

        //Animation animDamaged = gameObject.GetComponent<Animation>();
        //animDamaged.Play();

        gameObject.GetComponent<Animation>().Play("playerRedFlash");

        //animation.Play();

    }

    // ノックバック
    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while ( knockDur > timer)
        {
            timer += Time.deltaTime;

            rb2d.AddForce(new Vector3(knockbackDir.x  * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }

        yield return 0;

    }


}
