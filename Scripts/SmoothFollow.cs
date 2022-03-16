using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform FollowTarget;
    [SerializeField] private float smoothing;

    private Vector3 position;

    public void SetPosition(Vector3 pos){
        position = pos;
        transform.position = pos;
    }
    
    private void Start(){
        SetPosition(transform.position);
    }

    void Update(){
        position = Vector3.Lerp(position, FollowTarget.position, Time.deltaTime * smoothing);
        transform.position = position;
    }
}
