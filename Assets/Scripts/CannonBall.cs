using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float damage;

    ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {

        particles.Play();

        Destroy(gameObject, GameManager.Instance.LimitTimeForTurn);
	}

    private void OnDestroy()
    {
        GameManager.Instance.EndOfThisTurn();
    }
}
