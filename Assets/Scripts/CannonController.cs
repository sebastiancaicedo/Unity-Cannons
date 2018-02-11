﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

    public float traslationSpeed = 5;
    //public float rotationSpeed = 25;
    //public float shootForce = 10;

    [SerializeField]
	Transform pivote;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    Rigidbody cannonBallPrefab;
    [SerializeField]
    AudioClip shootSound;
    [SerializeField]
    AudioClip aimSound;


    AudioSource audioSource;
    ParticleSystem smokeParticles;
    Vector2 tInputs = Vector2.zero;
    //int rInput = 0;
    int yRotationFixValue;
    //bool fire;
    public bool HasShooted { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        smokeParticles = muzzle.GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {

        yRotationFixValue = transform.localEulerAngles.y == 0 ? 1 : -1;
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.Instance.IsTurnOf(this) || HasShooted) return;

        GetInputs();

        transform.Translate(tInputs.x * yRotationFixValue * traslationSpeed * Time.deltaTime, tInputs.y * traslationSpeed * Time.deltaTime, 0);

        //pivote.Rotate(0, 0, rInput * rotationSpeed * Time.deltaTime);

	}

    private void GetInputs()
    {
        //Traslation Inputs
        tInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Rotation Inputs
        //if (Input.GetKey(KeyCode.Q))
        //    rInput = 1;

        //if (Input.GetKey(KeyCode.E))
        //    rInput = -1;

        //if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        //    rInput = 0;

        //fire = Input.GetKeyDown(KeyCode.Space);
    }

    public void Shoot(int angle, int force)
    {
        HasShooted = true;
        StartCoroutine(IEShoot(angle, force));
    }

    private IEnumerator IEShoot(int angle, int force)
    {
        //Manejo de camara
        if (GameManager.Instance.UseDynamicCamera)
        {
            GameManager.Instance.Camera.SetShootPosition();
            yield return new WaitUntil(() => GameManager.Instance.Camera.IsCameraReady);
        }

        //Rotacion
        audioSource.loop = true;
        PlaySound(aimSound);
        angle = angle % 360;
        do
        {
            pivote.localEulerAngles = Vector3.Lerp(pivote.localEulerAngles, new Vector3(0, 0, angle), 1.2f * Time.deltaTime);
            yield return null;

        } while (Mathf.Abs(Mathf.Abs(angle) - Mathf.Abs(pivote.localEulerAngles.z)) > 1);

        pivote.localEulerAngles = new Vector3(0, 0, angle);
        audioSource.Stop();
        audioSource.loop = false;
        yield return new WaitForSeconds(0.3f);

        //Disparo
        PlaySound(shootSound);
        smokeParticles.Play();
        Rigidbody bullet = Instantiate(cannonBallPrefab, muzzle.position, muzzle.rotation);
        bullet.AddForce(muzzle.forward * force, ForceMode.Impulse);
        yield return new WaitForEndOfFrame();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }


}
