using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonPuzzle : PuzzleRoot
{
    [SerializeField] private List<GameObject> lights;
    [SerializeField] private List<SimonPlatform> platforms;

    [SerializeField] private GameManager gm;

    private short[] firstSequence  = new short[1];
    private short[] secondSequence = new short[2];
    private short[] thirdSequence  = new short[3];
    private short[] forthSequence  = new short[4];

    private short currentSequence = 0;
    private short currentSequenceStep = 0;

    private Coroutine currentWaitLightRoutine;
    private Coroutine currentWaitPlayerStepRoutine;

    void Start()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(false);
        }

        SetupSequences();

        currentWaitLightRoutine = StartCoroutine(LightSequence());
    }

    private void SetupSequences()
    {
        firstSequence[0] = (short)Random.Range(0, lights.Count);

        for (int i = 0; i < secondSequence.Length; i++)
        {
            secondSequence[i] = (short)Random.Range(0, lights.Count);
        }

        for (int i = 0; i < thirdSequence.Length; i++)
        {
            thirdSequence[i] = (short)Random.Range(0, lights.Count);
        }

        for (int i = 0; i < forthSequence.Length; i++)
        {
            forthSequence[i] = (short)Random.Range(0, lights.Count);
        }
    }


    //Cooroutine
    private IEnumerator LightSequence()
    {
        if(currentWaitPlayerStepRoutine != null)
            StopCoroutine(currentWaitPlayerStepRoutine);

        currentWaitPlayerStepRoutine = null;
        yield return new WaitForSecondsRealtime(4);

        switch (currentSequence)
        {
            case 0:
                lights[firstSequence[0]].SetActive(true);
                yield return new WaitForSecondsRealtime(1);
                lights[firstSequence[0]].SetActive(false);
                yield return new WaitForSecondsRealtime(1);
                break;
            case 1:
                for (int i = 0; i < secondSequence.Length; i++)
                {
                    lights[secondSequence[i]].SetActive(true);
                    yield return new WaitForSecondsRealtime(1);
                    lights[secondSequence[i]].SetActive(false);
                    yield return new WaitForSecondsRealtime(1);
                }
                break;
            case 2:
                for (int i = 0; i < thirdSequence.Length; i++)
                {
                    lights[thirdSequence[i]].SetActive(true);
                    yield return new WaitForSecondsRealtime(1);
                    lights[thirdSequence[i]].SetActive(false);
                    yield return new WaitForSecondsRealtime(1);
                }
                break;
            case 3:
                for (int i = 0; i < forthSequence.Length; i++)
                {
                    lights[forthSequence[i]].SetActive(true);
                    yield return new WaitForSecondsRealtime(1);
                    lights[forthSequence[i]].SetActive(false);
                    yield return new WaitForSecondsRealtime(1);
                }
                break;
        }

        currentWaitLightRoutine = StartCoroutine(LightSequence());
    }

    private IEnumerator WaitNextPlayerStep()
    {
        if(currentWaitLightRoutine != null)
            StopCoroutine(currentWaitLightRoutine);

        yield return new WaitForSecondsRealtime(4);

        StopCoroutine(currentWaitLightRoutine);
        currentWaitLightRoutine = StartCoroutine(LightSequence());
    }

    public void PlatformStepped(int id)
    {
        Debug.Log($"Platform stepped, ID: {id}");

        if(currentSequence == 0)
        {
            if(id == firstSequence[0])
            {
                currentSequence++;
            }
            else
            {
                StopCoroutine(currentWaitLightRoutine);
                currentWaitLightRoutine = StartCoroutine(LightSequence());
            }
        }
        else if(currentSequence == 1)
        {
            if (id == secondSequence[currentSequenceStep])
            {
                if (currentSequenceStep < secondSequence.Length - 1)
                {
                    currentSequenceStep++;

                    if(currentWaitPlayerStepRoutine != null)
                        StopCoroutine(currentWaitPlayerStepRoutine);

                    currentWaitPlayerStepRoutine = StartCoroutine(WaitNextPlayerStep());
                }
                else
                {
                    currentSequence++;
                    currentSequenceStep = 0;
                    StopCoroutine(currentWaitLightRoutine);
                    currentWaitLightRoutine = StartCoroutine(LightSequence());
                }
            }
            else
            {
                currentSequence = 0;
                currentSequenceStep = 0;
                StopCoroutine(currentWaitLightRoutine);
                currentWaitLightRoutine = StartCoroutine(LightSequence());
            }
        }
        else if (currentSequence == 2)
        {
            if (id == thirdSequence[currentSequenceStep])
            {
                if (currentSequenceStep < thirdSequence.Length - 1)
                {
                    currentSequenceStep++;

                    if (currentWaitPlayerStepRoutine != null)
                        StopCoroutine(currentWaitPlayerStepRoutine);

                    currentWaitPlayerStepRoutine = StartCoroutine(WaitNextPlayerStep());
                }
                else
                {
                    currentSequence++;
                    currentSequenceStep = 0;
                    StopCoroutine(currentWaitLightRoutine);
                    currentWaitLightRoutine = StartCoroutine(LightSequence());
                }
            }
            else
            {
                currentSequence = 0;
                currentSequenceStep = 0;
                StopCoroutine(currentWaitLightRoutine);
                currentWaitLightRoutine = StartCoroutine(LightSequence());
            }
        }
        else if (currentSequence == 3)
        {
            if (id == forthSequence[currentSequenceStep])
            {
                if (currentSequenceStep < forthSequence.Length - 1)
                {
                    currentSequenceStep++;

                    if (currentWaitPlayerStepRoutine != null)
                        StopCoroutine(currentWaitPlayerStepRoutine);

                    currentWaitPlayerStepRoutine = StartCoroutine(WaitNextPlayerStep());
                }
                else
                {
                    puzzleCompleted = true;
                    gm.CompletePuzzle(2);

                    for (int i = 0; i < platforms.Count; i++)
                    {
                        platforms[i].GetComponent<BoxCollider>().enabled = false;
                    }

                    StopAllCoroutines();
                    Debug.Log("Puzzle2 Complete");
                }
            }
            else
            {
                currentSequence = 0;
                currentSequenceStep = 0;
                StopCoroutine(currentWaitLightRoutine);
                currentWaitLightRoutine = StartCoroutine(LightSequence());
            }
        }
    }


    public override void CheckPuzzleCompletion()
    {
        /*

        if (correctPuzzles >= rocks.Count)
        {
            puzzleCompleted = true;

            gm.CompletePuzzle();

            Debug.Log("Puzzle2 Complete");
        }
        */
    }
}
