using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;
using UnityAtoms.BaseAtoms;

public class UiController : MonoBehaviour
{
    [SerializeField, BoxGroup("Countdown")] private GameObject counterGameObject;
    [SerializeField, BoxGroup("Countdown")] private VoidEvent startSpawningEvent;
    [SerializeField, BoxGroup("Spawned")] private TextMeshProUGUI spawnedText;
    [SerializeField, BoxGroup("Spawned")] private IntVariable spawnedCount;
    [SerializeField, BoxGroup("Correct Text")] private TextMeshProUGUI correctText;
    [SerializeField, BoxGroup("Finish")] private GameObject finishUI;
    [SerializeField, BoxGroup("Finish")] private TextMeshProUGUI finishText;
    private TextMeshProUGUI counterText;
    private int counterValue = 3;
    private float counterTime = 0.5f;

    private void Awake()
    {
        counterText = counterGameObject.GetComponent<TextMeshProUGUI>();
    }

    [Button]
    public void StartSpawning()
    {
        StartCounter();
    }

    private void StartCounter()
    {
        counterText.text = counterValue.ToString();
        counterGameObject.SetActive(true);
        counterGameObject.transform.DOScale(2, counterTime).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine)
            .OnComplete(()=> {
                counterValue--;
                if (counterValue > 0)
                {
                    StartCounter();
                }
                else
                {
                    startSpawningEvent.Raise();
                    counterGameObject.SetActive(false);
                }
            });
    }
    
    public void UpdateSpawnedCount()
    {
        spawnedText.text = spawnedCount.Value.ToString();
    }
    
    public void UpdateCorrectCount(int arrived, int correct)
    {
        correctText.text = $"{correct}/{arrived}";
    }


    public void ShowFinishScreen(int correctCount, int arrivedCount)
    {
        finishText.text = "You got " + correctCount + " out of " + arrivedCount + " correct!";
        finishUI.SetActive(true);
    }
}
