using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float damage;
    [SerializeField]
    AudioClip explotionSound;

    ParticleSystem particles;
    ParticleSystem explodeParticles;

    AudioSource audioSource;
    Rigidbody _rigidbody;
    Collider _collider;
    Renderer _renderer;

    public Rigidbody _Rigidbody { get { return _rigidbody; } }
    public Collider _Collider { get { return _collider; } }
    public Renderer _Renderer { get { return _renderer; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        particles = GetComponent<ParticleSystem>();
        explodeParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        explodeParticles.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {

        particles.Play();
        Destroy(gameObject, GameManager.Instance.LimitTimeForTurn);
	}

    private void OnDestroy()
    {
        if (GameManager.Instance.GameMode == GameMode.ByTurn)
            GameManager.Instance.EndOfThisTurn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.transform.root.tag;
        if (colTag == "Player 1" || colTag == "Player 2")
        {
            Health playerHealth = collision.transform.root.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            StartCoroutine(IEExplode());
        }
    }

    IEnumerator IEExplode()
    {
        explodeParticles.gameObject.SetActive(true);
        explodeParticles.transform.SetParent(null);
        Destroy(explodeParticles.gameObject, 4);
        PlaySound(explotionSound);
        explodeParticles.Play();
        yield return new WaitForEndOfFrame();
        particles.Stop();
        _Renderer.enabled = false;
        _Collider.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
