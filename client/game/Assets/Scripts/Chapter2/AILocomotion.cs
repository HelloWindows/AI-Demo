using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILocomotion : Vehicle
{
    private CharacterController controller;
    private Rigidbody theRigidbody;
    private Vector3 moveDistance;

    // Start is called before the first frame update
    protected override void Start()
    {
        controller = GetComponent<CharacterController>();
        theRigidbody = GetComponent<Rigidbody>();
        moveDistance = Vector3.zero;
        base.Start();
    }

    private void FixedUpdate() {
        velocity += acceleration * Time.deltaTime;
        if (velocity.sqrMagnitude > sqrMaxSpeed) {
            velocity = velocity.normalized * maxSpeed;
        }
        moveDistance = velocity * Time.fixedDeltaTime;
        if (isPlanar) {
            velocity.y = 0;
            moveDistance.y = 0;
        }
        if (controller != null) {
            controller.SimpleMove(velocity);
        } else if (theRigidbody == null || theRigidbody.isKinematic) {
            transform.position += moveDistance;
        } else {
            theRigidbody.MovePosition(theRigidbody.position + moveDistance);
        } // end if

        if (velocity.sqrMagnitude > 0.00001) {
            Vector3 newForward = Vector3.Slerp(transform.forward, velocity, damping * Time.deltaTime);
            if (isPlanar) {
                newForward.y = 0;
            }
            transform.forward = newForward;
        }
    }
}
