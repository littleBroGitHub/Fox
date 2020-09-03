using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected Animator anim;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void jumpOn()
    {
        anim.SetTrigger("death");
    }
}
