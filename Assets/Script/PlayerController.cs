using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Collider2D coll;

    public int cherryScore;
    public int gemScore;

    public Text CherryScoreText;
    public Text GemScoreText;

    public float speed = 300f;
    public float jumpPower = 700f;

    public LayerMask ground;

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //跳
        if (Input.GetButtonDown("Jump") && anim.GetBool("idle"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetBool("jumping", true);
        }
    }

    void FixedUpdate()
    {
        if (!isHurt) { 
            Move();
         }
        SwitchAnim();
    }

    //Move Player
    void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");

        //移动
        rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
        anim.SetFloat("running", Mathf.Abs(faceDirection));//动画

        //转向
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        //下降动画
        if (rb.velocity.y < 0)
        {
            anim.SetBool("falling", true);
            if (anim.GetBool("jumping"))
            {
                anim.SetBool("jumping", false);
            }
        } else if (isHurt) {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
            }
        } else if (coll.IsTouchingLayers(ground))
        {
            //下降到地面恢复闲置动作
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    void Crouch()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集物品得分
        if (collision.tag == "CollectionCherry")
        {
            Destroy(collision.gameObject);
            cherryScore++;
            CherryScoreText.text = cherryScore.ToString();
        }
        if (collision.tag == "CollectionGem")
        {
            Destroy(collision.gameObject);
            gemScore++;
            GemScoreText.text = gemScore.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //FrogController frog = collision.gameObject.GetComponent<FrogController>();
        Enemies enem = collision.gameObject.GetComponent<Enemies>();
        if (collision.gameObject.tag == "Enemies")
        {
            if (anim.GetBool("falling"))
            {
                enem.jumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                anim.SetBool("jumping", true);
            } else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            } else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
