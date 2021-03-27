using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksPuzzle : PuzzleRoot
{
    /* COLORS CODE:
     * 
     * 0 -> RED
     * 1 -> BLUE
     * 2 -> YELLOW
     * 
     */
    [SerializeField] private List<RockColors> rocks;
    [SerializeField] private List<RockPlatform> platforms;

    [SerializeField] private GameManager gm;

    void Start()
    {
        SetupRocksAndBalls();
    }

    private void SetupRocksAndBalls()
    {
        for (int i = 0; i < rocks.Count; i++)
        {
            short randomNum = (short)Random.Range(0,3);
            rocks[i].Initialize(randomNum);
            randomNum = (short)Random.Range(0, 3);
            platforms[i].Initialize(randomNum);
        }

    }

    override public void CheckPuzzleCompletion()
    {
        short correctPuzzles = 0;

        for (int i = 0; i < rocks.Count; i++)
        {
            for (int x = 0; x < platforms.Count; x++)
            {
                if (rocks[i].GetRockId() == platforms[x].GetRockId())
                {
                    if (rocks[i].GetBallId() == platforms[x].currentBall)
                    {
                        correctPuzzles++;
                        continue;
                    }
                    else
                    {
                        //Puzzle not completed
                        return;
                    }
                }
            }
        }

        if(correctPuzzles >= rocks.Count)
        {
            puzzleCompleted = true;

            gm.CompletePuzzle(1);

            Debug.Log("Puzzle1 Complete");
        }
    }
}
