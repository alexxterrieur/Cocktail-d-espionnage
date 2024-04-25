using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Teleport : MonoBehaviour
{
    public GameObject playerRef;
    public GameObject teleportPoint;

    public void Teleport()
    {
        playerRef.transform.position = teleportPoint.transform.position;
    }
}
