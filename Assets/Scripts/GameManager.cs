using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Private Variables
    [Tooltip("Time, in minutes, that the playes has to complete the puzzles")]
    [SerializeField] float initialTime = 5; //In minutes

    float timeLeft; //In seconds

    Coroutine messageCoroutine;

    //Serialized Variables
    [Header("Canvas Stuff")]
    [SerializeField] TMP_Text timeLeftText;
    [SerializeField] TMP_Text messageTmp;
    [Header("UI")]
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject victoryScreen;
    [Space]
    [SerializeField] private PlayerController player;
    [SerializeField] List<PuzzleRoot> puzzles; //List of all the puzzles
    [Header("Gate")]
    [SerializeField] private GameObject gateTrigger;

    private bool finalWarning = false;

    private void Start()
    {
        messageTmp.gameObject.SetActive(false);
        gateTrigger.SetActive(false);
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);

        timeLeft = initialTime * 60;
    }

    void Update()
    {
        TimeUi();

        //Loss condition
        if(timeLeft <= 0)
        {
            deathScreen.SetActive(true);
            PlayerMovement(false);
            StartCoroutine(CloseAfterWinOrLose());
        }

        //Victory condition
        if(PuzzlesCompleted())
        {
            if (finalWarning == false)
            {
                finalWarning = true;
                if (messageCoroutine != null)
                    StopCoroutine(messageCoroutine);
                StartCoroutine(ShowTimedMessage($"Você completou todos os desafios, fuja para a praça!", 4));

                gateTrigger.SetActive(true);
                //Mensagem completo
            }
        }

        timeLeft -= Time.deltaTime;
    }

    //Private Functions
    private void TimeUi()
    {
        timeLeft = timeLeft < 0 ? 0 : timeLeft;
        timeLeftText.text = $"Tempo Restante:\n{((int)timeLeft).ToString()}";
    }

    private bool PuzzlesCompleted()
    {
        for (int i = 0; i < puzzles.Count; i++)
        {
            if(puzzles[i].puzzleCompleted == false)
            {
                return false;
            }
        }
        return true;
    }

    private void AddTime(int timeAdd)
    {
        timeLeft += timeAdd;
    }

    private IEnumerator ShowTimedMessage(string message, float time = 2)
    {
        messageTmp.text = message;
        messageTmp.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        messageTmp.gameObject.SetActive(false);
    }

    private IEnumerator CloseAfterWinOrLose()
    {
        yield return new WaitForSecondsRealtime(7);
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }

    //Public Functions
    public void CompletePuzzle(int id)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);
        messageCoroutine = StartCoroutine(ShowTimedMessage($"Você completou o {id}° desafio, vá para o proximo!", 3));

        AddTime(30);
    }

    public void WrongSharade()
    {
        AddTime(-10);
    }

    public void PlayerMovement(bool state)
    {
        player.MovementState(state);
    }

    public void WinGame()
    {
        victoryScreen.SetActive(true);
        PlayerMovement(false);
        StartCoroutine(CloseAfterWinOrLose());
    }
}
