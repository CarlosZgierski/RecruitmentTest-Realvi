using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonPlatform : MonoBehaviour
{
    [SerializeField] private short platformId;
    [SerializeField] private SimonPuzzle sp;
    public short GetPlatformId => platformId;

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
        if(other.gameObject.tag == "Player" && playerInside == false)
        {
            sp.PlatformStepped(platformId);
            playerInside = true;
        }
    }
}
