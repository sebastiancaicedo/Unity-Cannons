using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    [SerializeField]
    bool useParticles = true;
    public float damage;

    ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {

        if (useParticles) particles.Play();

        Destroy(gameObject, GameManager.Instance.limitTimeForTurn);
	}

    private void OnDestroy()
    {
        GameManager.Instance.EndOfThisTurn();
    }
}
