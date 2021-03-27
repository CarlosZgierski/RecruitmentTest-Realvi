using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectPuzzle : PuzzleRoot
{
    [SerializeField] private GameManager gm;
    [SerializeField] private List<Image> upperButtons;
    [SerializeField] private List<Image> lowerButtons;
    [SerializeField] private List<Color> buttonColors;
    [Header("UI")]
    [SerializeField] private GameObject puzzleCanvas;

    //[SerializeField] private List<>

    //-1 is the default value
    private int firstClicked = -1;

    private int numberOfConnected = 0;

    public void ButtonClicked(int id)
    {
        if(firstClicked == -1)
        {
            firstClicked = id;
        }
        else
        {
            if(firstClicked == id)
            {
                numberOfConnected++;

                upperButtons[id].color = buttonColors[id];
                lowerButtons[id].color = buttonColors[id];

                firstClicked = -1;

                if (numberOfConnected >= upperButtons.Count)
                {
                    puzzleCanvas.SetActive(false);
                    gm.PlayerMovement(true);
                    puzzleCompleted = true;
                    gm.CompletePuzzle(3);
                    Debug.Log("Puzzle4 completed");
                }
            }
            else
            {
                gm.WrongSharade();
                firstClicked = -1;
            }
        }
    }

    public void PlatformStepped()
    {
        //Start Puzzle
        puzzleCanvas.SetActive(true);
        gm.PlayerMovement(false);
    }
}
