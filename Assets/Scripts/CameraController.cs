using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float zDistance = 4;
    Vector3 startPosition;

    public bool IsCameraReady { get; set; }

    private void Start()
    {
        startPosition = transform.position;
    }

    public void MoveTowardsPlayer(Transform player)
    {
        IsCameraReady = false;
        Vector3 targetPoint = new Vector3(player.position.x, player.position.y, -zDistance);
        StartCoroutine(IEMoveTowardsTarget(targetPoint));
    }

    IEnumerator IEMoveTowardsTarget(Vector3 point)
    {
        do
        {
            transform.position = Vector3.Lerp(transform.position, point, 2 * Time.deltaTime);
            yield return null;

        } while (Vector3.Distance(transform.position, point) > 0.01f);
        transform.position = point;

        yield return new WaitForEndOfFrame();
        IsCameraReady = true;
    }

    public void SetShootPosition()
    {
        IsCameraReady = false;
        StartCoroutine(IEMoveTowardsTarget(startPosition));
    }
}
