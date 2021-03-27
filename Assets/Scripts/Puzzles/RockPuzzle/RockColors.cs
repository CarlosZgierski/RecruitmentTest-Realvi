using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockColors : MonoBehaviour
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

    private short currentBall;

    void Start()
    {
        rebBall.SetActive(false);
        blueBall.SetActive(false);
        yellowBall.SetActive(false);
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

    public short GetRockId() => rockId;
    public short GetBallId() => currentBall;
}
