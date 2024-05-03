using UnityEngine;

public class S_CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    private Vector3 cameraPos = Vector3.zero;

    void Start()
    {
        cameraPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        cameraPos.x = playerRef.transform.position.x;
        cameraPos.y = playerRef.transform.position.y;
        gameObject.transform.position = cameraPos;
    }
}
