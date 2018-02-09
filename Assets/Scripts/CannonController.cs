using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

    public float traslationSpeed = 5;
    public float rotationSpeed = 25;
    public float shootForce = 10;

    [SerializeField]
	Transform pivote;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    Rigidbody cannonBallPrefab;

    Vector2 tInputs = Vector2.zero;
    int rInput = 0;
    //bool fire;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.Instance.IsTurnOf(this)) return;

        GetInputs();

        transform.Translate(tInputs.x * traslationSpeed * Time.deltaTime, tInputs.y * traslationSpeed * Time.deltaTime, 0);

        pivote.Rotate(0, 0, rInput * rotationSpeed * Time.deltaTime);

	}

    private void GetInputs()
    {
        //Traslation Inputs
        tInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Rotation Inputs
        if (Input.GetKey(KeyCode.Q))
            rInput = 1;

        if (Input.GetKey(KeyCode.E))
            rInput = -1;

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
            rInput = 0;

        //fire = Input.GetKeyDown(KeyCode.Space);
    }

    public void Shoot(int angle, int force)
    {
        StartCoroutine(eShoot(angle, force));
    }

    private IEnumerator eShoot(int angle, int force)
    {
        do
        {
            pivote.localEulerAngles = Vector3.Lerp(pivote.localEulerAngles, new Vector3(0, 0, angle), 2 * Time.deltaTime);
            yield return null;
        } while (Mathf.Abs(angle - pivote.localEulerAngles.z) > 1);
        pivote.localEulerAngles = new Vector3(0, 0, angle);
        yield return new WaitForSeconds(0.3f);
        Rigidbody bullet = Instantiate(cannonBallPrefab, muzzle.position, muzzle.rotation);
        bullet.AddForce(muzzle.forward * force, ForceMode.Impulse);
    }


}
