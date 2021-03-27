using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SharadePuzzle : PuzzleRoot
{
    [SerializeField] private GameManager gm;
    [Header("UI")]
    [SerializeField] private GameObject puzzleCanvas;
    [SerializeField] private TMP_Text questionTmp;
    [SerializeField] private List<TMP_Text> answersTmpFields;
    [Header("Answers")]
    [SerializeField] private List<int> answersId;
    [SerializeField] private List<string> questions;
    [SerializeField] private List<String> firstSharadeAnswers;
    [SerializeField] private List<String> secondSharadeAnswers;
    [SerializeField] private List<String> thirdSharadeAnswers;
    [SerializeField] private List<String> forthSharadeAnswers;

    private short currentSharade = 0;

    void Start()
    {
        currentSharade = 0;
        RedrawSharade();
    }

    public void AnswerChosen(int id)
    {
        if(answersId[currentSharade] == id)
        {
            if (currentSharade < 3)
            {
                currentSharade++;
                RedrawSharade();
            }
            else
            {
                puzzleCanvas.SetActive(false);
                gm.PlayerMovement(true);
                puzzleCompleted = true;
                gm.CompletePuzzle(3);
                Debug.Log("Puzzle3 completed");
            }
        }
        else
        {
            gm.WrongSharade();
        }
    }

    private void RedrawSharade()
    {
        if(currentSharade == 0)
        {
            for (int i = 0; i < answersTmpFields.Count; i++)
            {
                questionTmp.text = questions[0];
                answersTmpFields[i].text = firstSharadeAnswers[i];
            }
        }
        else if(currentSharade == 1)
        {
            for (int i = 0; i < answersTmpFields.Count; i++)
            {
                questionTmp.text = questions[1];
                answersTmpFields[i].text = secondSharadeAnswers[i];
            }
        }
        else if(currentSharade == 2)
        {
            for (int i = 0; i < answersTmpFields.Count; i++)
            {
                questionTmp.text = questions[2];
                answersTmpFields[i].text = thirdSharadeAnswers[i];
            }
        }
        else if(currentSharade == 3)
        {
            for (int i = 0; i < answersTmpFields.Count; i++)
            {
                questionTmp.text = questions[3];
                answersTmpFields[i].text = forthSharadeAnswers[i];
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
