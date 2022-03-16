using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseEnter : MonoBehaviour{
    [SerializeField] private UnityEvent events;
    private void OnMouseOver(){
        events.Invoke();
    }
}
