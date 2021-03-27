using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPlatform : MonoBehaviour
{
    [SerializeField] private ConnectPuzzle cp;

    private bool playerInside = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerInside == false)
        {
            cp.PlatformStepped();
            playerInside = true;
        }
    }
}
