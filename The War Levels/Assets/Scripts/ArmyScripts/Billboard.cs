using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform main_camera_transform;
    void Start()
    {
        main_camera_transform = FindObjectOfType<Camera>().transform;
    }
    void Update()
    {
        this.transform.LookAt(main_camera_transform);
    }
}
