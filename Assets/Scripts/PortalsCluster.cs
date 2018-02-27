using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalsCluster : MonoBehaviour {

    [SerializeField]
    Portal[] portals;

    private void Start()
    {
        portals = new Portal[transform.childCount];
        for (int child = 0; child < transform.childCount; child++)
        {
            portals[child] = transform.GetChild(child).GetComponent<Portal>();
        }
    }

    public void Travel(CannonBall ball, Vector3 velocity, int sender)
    {
        int index = -1;
        do
        {
            index = Random.Range(0, portals.Length);
        } while (index == sender);
        portals[index].Teleport(ball, velocity);
    }
}
