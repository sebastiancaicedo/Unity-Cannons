using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

    private delegate void UserInputs();
    private UserInputs GetInputs;

    public float traslationSpeed = 5;
    public float rotationSpeed = 25;
    public float defaultShootForce = 18;

    [SerializeField]
	Transform pivote;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    CannonBall cannonBallPrefab;
    [SerializeField]
    AudioClip shootSound;
    [SerializeField]
    AudioClip aimSound;


    AudioSource audioSource;
    ParticleSystem smokeParticles;
    Vector2 tInputs = Vector2.zero;
    int yRotationFixValue;


    public bool HasShooted { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        smokeParticles = muzzle.GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () {

        if(GameManager.Instance.GameMode == GameMode.ByTurn)
        {
            GetInputs += ByTurnsInputs;
        }
        else
            if(GameManager.Instance.GameMode == GameMode.Free)
        {
            GetInputs += FreeInputs;
        }
        yRotationFixValue = transform.localEulerAngles.y == 0 ? 1 : -1;
    }
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.Instance.IsTurnOf(this)) return;

        GetInputs();
        transform.Translate(tInputs.x * yRotationFixValue * traslationSpeed * Time.deltaTime, tInputs.y * traslationSpeed * Time.deltaTime, 0);

	}

    private void ByTurnsInputs()
    {
        if (HasShooted) return;

        //Traslation Inputs
        tInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

    private void FreeInputs()
    {

        float h = Input.GetAxis(string.Format("Horizontal {0}", gameObject.tag));
        float v = Input.GetAxis(string.Format("Vertical {0}", gameObject.tag));
        tInputs = new Vector2(0, v);

        if(h != 0 && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            PlaySound(aimSound);
        }

        if(h == 0)
        {
            audioSource.Stop();
        }

        pivote.Rotate(0, 0, -h * yRotationFixValue * rotationSpeed * Time.deltaTime);

        string shootInput = string.Format("Fire1 {0}", gameObject.tag);
        if (Input.GetButtonDown(shootInput))
        {
            Shoot();
        }


    }

    private void Shoot()
    {
        Fire(defaultShootForce);
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

        } while (Mathf.Abs(Mathf.Abs(angle) - Mathf.Abs(pivote.localEulerAngles.z)) > 2);

        pivote.localEulerAngles = new Vector3(0, 0, angle);
        audioSource.Stop();
        audioSource.loop = false;
        yield return new WaitForSeconds(0.3f);

        Fire(force);
        yield return new WaitForEndOfFrame();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void Fire(float force)
    {
        //Disparo
        smokeParticles.Play();
        CannonBall bullet = Instantiate(cannonBallPrefab, muzzle.position, muzzle.rotation);
        bullet.PlaySound(shootSound);//Lo sonamos desde la bala porque cada bala es una instancia diferencte con su propio audioSource
        bullet._Rigidbody.AddForce(muzzle.forward * force, ForceMode.Impulse);
    }


}
