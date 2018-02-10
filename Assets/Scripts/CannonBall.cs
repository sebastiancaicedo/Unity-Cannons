using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float damage;

    ParticleSystem particles;
    ParticleSystem explodeParticles;

    AudioSource audioSource;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        explodeParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        explodeParticles.gameObject.SetActive(false);
        audioSource = explodeParticles.GetComponent<AudioSource>();
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

    private void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.transform.root.tag;
        if (colTag == "Player 1" || colTag == "Player 2")
        {
            print(collision.transform.root.name);
            StartCoroutine(IEExplode());
        }
    }

    IEnumerator IEExplode()
    {
        explodeParticles.gameObject.SetActive(true);
        explodeParticles.transform.SetParent(null);
        Destroy(explodeParticles.gameObject, 5);
        audioSource.Play();
        explodeParticles.Play();
        yield return new WaitForEndOfFrame();
        particles.Stop();
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}
