using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cam : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;

    private float zoomDirection;
    private Rigidbody rb;
    private Vector3 zPlaneDirection;
    private Vector3 forward;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forward = gameObject.transform.forward;
    }

    /* Checks if there is a direction the camera should move on the Z plane.
     * 
     * Checks if  the camera should move zoom in or out.
     */
    void Update()
    {
        zPlaneDirection.x = Input.GetAxisRaw("Horizontal");
        zPlaneDirection.y = Input.GetAxisRaw("Vertical");

        zoomDirection = Input.GetAxisRaw("Mouse ScrollWheel");
    }

    /* Applies any Z plane movement that needs to occur, then any zooming.
     */
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + zPlaneDirection           * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + (zoomDirection * forward) * zoomSpeed * Time.fixedDeltaTime);
    }
}
