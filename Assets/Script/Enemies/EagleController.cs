using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemies
{
    public Transform topPoint, bottomPoint;

    public float speed;
    private float topY, bottomY;

    private bool isUp = true;

    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();

        topY = topPoint.position.y;
        bottomY = bottomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
    }

    void MoveMent()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > topY)
            {
                isUp = false;
            }
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < bottomY)
            {
                isUp = true;
            }
        }
    }
}
