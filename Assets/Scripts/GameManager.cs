using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private IntVariable spawned;
    [SerializeField] private UiController uiController;
    [SerializeField] private IntVariable trainCount;
    [SerializeField] private int arrivedCount;
    [SerializeField] private int correctCount;


    private void Awake()
    {
        spawned.Value = 0;
    }

    public void ExitMethod()
    {
        Application.Quit();
    }
    
    public void AddArrivedCount(bool correct)
    {
        if (correct)
        {
            correctCount++;
        }

        arrivedCount++;

        if (arrivedCount == trainCount.Value)
        {
            FinishGame();
        }
        
        
        // Update UI
        uiController.UpdateCorrectCount(arrivedCount, correctCount);
    }
    
    private void FinishGame()
    {
        uiController.ShowFinishScreen(correctCount, arrivedCount);
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    
}
