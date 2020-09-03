using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FrogController : Enemies
{
    public LayerMask ground;
    public Transform leftPoint, rightPoint;

    private bool faceLeft;
    public float speed, jumpPower;
    private float leftX, rigthX;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        transform.DetachChildren();
        leftX = leftPoint.position.x;
        rigthX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void MoveMent()
    {
        if (faceLeft)
        {

            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpPower);
            }

            if (transform.position.x < leftX && coll.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
            //rb.velocity = new Vector2(-speed, jumpPower);
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpPower);
            }
            if (transform.position.x > rigthX && coll.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
            //rb.velocity = new Vector2(speed, jumpPower);
        }
    }

    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (anim.GetBool("falling") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
}
