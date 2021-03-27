using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharadePlatform : MonoBehaviour
{
    [SerializeField] private SharadePuzzle sp;

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
            sp.PlatformStepped();
            playerInside = true;
        }
    }
}
