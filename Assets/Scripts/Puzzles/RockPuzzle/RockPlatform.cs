using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPlatform : MonoBehaviour
{
    /* COLORS CODE:
     * 
     * 0 -> RED
     * 1 -> BLUE
     * 2 -> YELLOW
     */
    [SerializeField] private GameObject rebBall;
    [SerializeField] private GameObject blueBall;
    [SerializeField] private GameObject yellowBall;

    [SerializeField] private short rockId;
    [SerializeField] private RocksPuzzle rp;

    public short currentBall { get; private set; }

    void Start()
    {
        rebBall.SetActive(false);
        blueBall.SetActive(false);
        yellowBall.SetActive(false);
    }

    void Update()
    {
        
    }

    // Manager initializer
    public void Initialize(short ball)
    {
        switch (ball)
        {
            case 0: //RED
                rebBall.SetActive(true);
                blueBall.SetActive(false);
                yellowBall.SetActive(false);
                break;
            case 1: //BLUE
                rebBall.SetActive(false);
                blueBall.SetActive(true);
                yellowBall.SetActive(false);
                break;
            case 2: //YELLOW
                rebBall.SetActive(false);
                blueBall.SetActive(false);
                yellowBall.SetActive(true);
                break;
        }
        currentBall = ball;
    }

    public void ChangeBall()
    {
        switch (currentBall)
        {
            case 0: //RED
                rebBall.SetActive(false);
                blueBall.SetActive(true);
                yellowBall.SetActive(false);
                currentBall = 1;
                break;
            case 1: //BLUE
                rebBall.SetActive(false);
                blueBall.SetActive(false);
                yellowBall.SetActive(true);
                currentBall = 2;
                break;
            case 2: //YELLOW
                rebBall.SetActive(true);
                blueBall.SetActive(false);
                yellowBall.SetActive(false);
                currentBall = 0;
                break;
        }

        rp.CheckPuzzleCompletion();
    }

    public void DeactivateColliders()
    {
        this.GetComponent<BoxCollider>().enabled = false;
    }

    public short GetRockId() => rockId;
}
