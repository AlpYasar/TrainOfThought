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
    [SerializeField] private GameObject counterGameObject;
    [SerializeField] private VoidEvent startSpawningEvent;
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
}
