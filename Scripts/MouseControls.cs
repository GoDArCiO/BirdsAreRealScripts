using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor = UnityEngine.UIElements.Cursor;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private string interactable = "Interactable";
    [SerializeField] private string screen = "Screen";
    [SerializeField] private LayerMask layermask;

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            Click();
            StartDrag();
        }

        if (dragging){
            if(Input.GetMouseButton(0)) Drag();
            if(Input.GetMouseButtonUp(0)) EndDrag();
        }

    }

    //clicking 
    private void Click(){
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,99f,layermask)){
            if (hit.transform.CompareTag(interactable)){
                hit.transform.GetComponent<Interactable>().Click();
            }

            if (hit.transform.CompareTag(screen)){
                ScreenClick(hit.transform.GetComponent<InteractableScreen>());
            }
        }
    }

    private void ScreenClick(InteractableScreen screen){
        if (Physics.Raycast(screen.camera.ViewportPointToRay(screen.pointerPos.Value), out RaycastHit hit, 99f,layermask)){
            if (hit.transform.CompareTag(interactable)){
                hit.transform.GetComponent<Interactable>().Click();
            }
        }
    }

    //dragging
    private bool dragging = false;
    private Interactable cachedDragInteractable = null;
    
    private void StartDrag(){
        dragging = false;
        
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,99f,layermask)){
            if (hit.transform.CompareTag(interactable)){
                cachedDragInteractable = hit.transform.GetComponent<Interactable>();
                cachedDragInteractable.StartDrag();
                dragging = true;
            }

            if (hit.transform.CompareTag(screen)){
                cachedDragInteractable = GetScreenDraggable(hit.transform.GetComponent<InteractableScreen>());
                if (cachedDragInteractable != null){
                    cachedDragInteractable.StartDrag();
                    dragging = true;                    
                }
            }
        }
    }

    private void Drag(){
        cachedDragInteractable.Drag();
    }

    private void EndDrag(){
        cachedDragInteractable.EndDrag();
        dragging = false;
    }

    private Interactable GetScreenDraggable(InteractableScreen screen){
        if (Physics.Raycast(screen.camera.ViewportPointToRay(screen.pointerPos.Value), out RaycastHit hit, 99f,layermask)){
            if (hit.transform.CompareTag(interactable)){
                return hit.transform.GetComponent<Interactable>();
            }
        }
        return null;
    }
}
