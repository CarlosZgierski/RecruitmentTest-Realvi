using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private Animator gateAnimator;

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
            OpenGate();
            playerInside = true;
        }
    }

    public void OpenGate()
    {
        gateAnimator.SetBool("GateOpen", true);
        this.gameObject.SetActive(false);
    }
}
