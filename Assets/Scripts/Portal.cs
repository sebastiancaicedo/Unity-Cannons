using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    PortalsCluster cluster;
    Transform muzzle;

    private void Awake()
    {
        cluster = GetComponentInParent<PortalsCluster>();
    }

    private void Start()
    {
        muzzle = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        string colTag = other.transform.root.tag;
        if (colTag == "Cannon Ball")
        {
            CannonBall ball = other.GetComponent<CannonBall>();
            Vector2 velocity = ball._Rigidbody.velocity;
            ball._Rigidbody.velocity = Vector2.zero;
            ball._Collider.enabled = false;
            ball._Renderer.enabled = false;
            cluster.Travel(ball, velocity, transform.GetSiblingIndex());
        }
    }

    public void Teleport(CannonBall ball, Vector3 velocity)
    {
        ball.transform.position = muzzle.position;
        ball._Renderer.enabled = true;
        ball._Collider.enabled = true;
        ball._Rigidbody.velocity = muzzle.forward * velocity.magnitude;
    }
}
