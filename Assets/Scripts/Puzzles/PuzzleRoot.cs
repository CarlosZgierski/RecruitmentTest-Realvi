using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoot : MonoBehaviour
{
    public bool puzzleCompleted { get; protected set; }

    virtual public void CheckPuzzleCompletion() { }
}
