using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour{
    
    [SerializeField] private UnityEvent clickEvent;
    [SerializeField] private UnityEvent startDragEvent;
    [SerializeField] private UnityEvent dragEvent;
    [SerializeField] private UnityEvent endDragEvent;

    private bool dragging = false;

    public void Click(){
        clickEvent.Invoke();
    }
    
    public void StartDrag(){
        startDragEvent.Invoke();
    }

    public void Drag(){
        dragEvent.Invoke();
    }
    
    public void EndDrag(){
        endDragEvent.Invoke();
    }
}
