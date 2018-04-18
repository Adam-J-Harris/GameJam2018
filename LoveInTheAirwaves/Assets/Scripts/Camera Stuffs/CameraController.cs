using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 camOffset;
    public Quaternion rotOffset;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private bool followPlayer = false;

    void LateUpdate()
    {
        Vector3 target = this.target.position + camOffset;
        target.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);

    }
}
