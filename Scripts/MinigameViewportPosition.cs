using System;
using ScriptableObjectArchitecture;
using UnityEngine;

public class MinigameViewportPosition : MonoBehaviour{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector2Variable viewportPos;
    [SerializeField] private LayerMask screenLayer;

    private void Update(){
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out RaycastHit hit,99f,screenLayer)){
            viewportPos.Value = WorldToViewportPos(hit.point);
            v1 = hit.point;
        }
    }

    private Vector2 WorldToViewportPos(Vector3 worldPos){
        Vector2 viewportPos = transform.InverseTransformPoint(worldPos);
        viewportPos += Vector2.one / 2;
        viewportPos.x = Mathf.Clamp01(viewportPos.x);
        viewportPos.y = Mathf.Clamp01(viewportPos.y);
        return viewportPos;
    }


    private Vector3 v1;
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(v1,0.2f);
    }
}
