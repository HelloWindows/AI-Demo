using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Steering[] steerings;
    public float maxSpeed = 10;
    public float maxForce = 100;
    public float mass = 1;
    public Vector3 velocity;
    public float damping = 0.9f;
    public float computInterval = 0.2f;
    public bool isPlanar = true;

    [SerializeField]
    protected float sqrMaxSpeed;
    [SerializeField]
    protected Vector3 acceleration;

    private Vector3 steeringForce;

    private float timer;

    protected virtual void Start() {
        steeringForce = Vector3.zero;
        sqrMaxSpeed = maxSpeed * maxSpeed;
        timer = 0;
        steerings = GetComponents<Steering>();
    }

    private void Update() {
        timer += Time.deltaTime;
        steeringForce = Vector3.zero;
        if (timer > computInterval) {
            foreach (Steering s in steerings) {
                if (s.enabled)
                    steeringForce += s.Force() * s.weight;
            }
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
            acceleration = steeringForce / mass;
            timer = 0;
        }
    }
}
