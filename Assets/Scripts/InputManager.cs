using System;
using System.Collections;
using System.Collections.Generic;
using HoverController;
using NaughtyAttributes;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask ForkLayerMask;
    private Camera mainCamera;
    [SerializeField, ReadOnly] private bool isHovering;
    
    private HoverObjectController hoverObjectController;
    private RaycastHit hit;
    private Ray ray;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        hoverObjectController = HoverObjectController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        HoverFork();
    }
    
    private void HoverFork()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ForkLayerMask))
        {
            if (isHovering) return;
            hoverObjectController.SetActiveAndSetPosition(hit.transform.position);
            isHovering = true;
            Debug.Log("Hovering over fork");

        }else
        {
            if (!isHovering) return;
            hoverObjectController.SetInactive();
            isHovering = false;
        }
    }
}
