using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cam : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
