using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public FixedTouchField TouchField;
    public VirtualJoystick vLeftJoystick;

    protected Rigidbody rb;

    public float walkSpeed = 12.5f;
    protected float TouchDragLeftRight;
    protected float CameraLeftRightSpeed = 0.1f;
    protected float TouchDragUpDownClamped;
    protected float CameraUpDownSpeed = 0.02f;

    public Transform startPoint;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TouchDragLeftRight = transform.eulerAngles.y;


      
            Vector3 startPos = startPoint.position;
            startPos.y = transform.position.y;
            transform.position = startPos;
        
    }

    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        var input = vLeftJoystick.InputDirection;
        var vel = Quaternion.AngleAxis(TouchDragLeftRight, Vector3.up) * input * walkSpeed;
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        TouchDragLeftRight = TouchDragLeftRight + TouchField.TouchDist.x * CameraLeftRightSpeed;

        transform.rotation = Quaternion.AngleAxis(TouchDragLeftRight, transform.up);
        TouchDragUpDownClamped = Mathf.Clamp(TouchDragUpDownClamped + TouchField.TouchDist.y * CameraUpDownSpeed, -10f, 5f);
        Vector3 rot = Quaternion.AngleAxis(TouchDragLeftRight, Vector3.up) * new Vector3(0, TouchDragUpDownClamped, 4);
        Camera.main.transform.rotation = Quaternion.LookRotation(rot, Vector3.up);
    }
}
