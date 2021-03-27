using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject playerRef;

    private float dist;

    private void Start()
    {
        dist = Mathf.Abs(playerRef.transform.position.z - this.transform.position.z);
    }
    void Update()
    {
        this.transform.position = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y + 14, playerRef.transform.position.z - dist);
    }
}
