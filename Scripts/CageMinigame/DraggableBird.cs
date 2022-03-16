using System;
using System.Collections;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider))]
public class DraggableBird : MonoBehaviour{
    
    
    [SerializeField] private GameEvent resetEvent;
    [HideInInspector] public CageMinigameManager manager;
    [HideInInspector] public ObjectPool<GameObject> pool;
    [SerializeField] private GameEvent succesEvent;
    [SerializeField] private GameEvent failEvent;
    [SerializeField] private FloatVariable timeWindow;
    private float t = 0;
    
    [HideInInspector] public InteractableScreen screen;
    [SerializeField] private SmoothFollow smoothFollow;
    private Plane dragPlane;
    private Vector3 dragStartPos = Vector3.zero;
    private bool alreadySpawned = false;
    private bool draggable = true;

    private void ReleaseBird(){
        if(gameObject.activeSelf)
            pool.Release(gameObject);
    }

    private void OnEnable(){
        if (alreadySpawned){
            ResetBirdState();
        }
    }

    private void ResetBirdState(){
        transform.localScale = Vector3.one;
        transform.position = manager.GetWaitPos();
        smoothFollow.SetPosition(manager.GetSpawnPos());
        correctPosition = false;
        t = 0;
        draggable = true;
        dragStartPos = Vector3.zero;
    }

    private void Update(){
        t += Time.deltaTime;
        if (t > timeWindow.Value){
            Fail();
            t = -999;
            StartCoroutine(Disappear());
            draggable = false;
        }
    }

    private void Start(){
        ResetBirdState();
        resetEvent.AddListener(ReleaseBird);
        dragPlane = new Plane(-screen.camera.transform.forward, transform.position);
        alreadySpawned = true;
    }

    public void StartDrag(){
        dragStartPos = transform.position;
    }

    public void MoveToMouse(){
        if (draggable){
            Ray ray = screen.camera.ViewportPointToRay(screen.pointerPos.Value);
            dragPlane.Raycast(ray, out float enter);
            Vector3 pos = enter * ray.direction + screen.camera.transform.position;
            transform.position =  pos;
        }
    }

    public void EndDrag(){
        if (draggable){
            if (correctPosition) Succed();
            else Fail();
            StartCoroutine(Disappear());
            draggable = false;
        }
    }

    private void Succed(){
        succesEvent.Raise();
        transform.position = targetPos;
    }

    private void Fail(){
        transform.position += Vector3.up * 10f;
        failEvent.Raise();
    }

    IEnumerator Disappear(){
        float t = 1f;
        while(t>0){
            t -= Time.deltaTime;
            transform.localScale = Vector3.one * t;
            yield return null;
        }

        pool.Release(gameObject);
    }

    private bool correctPosition = false;
    private Vector3 targetPos = Vector3.zero;
    
    private void OnTriggerEnter(Collider other){
        correctPosition = true;
        targetPos = other.transform.position;
    }

    private void OnTriggerExit(Collider other){
        correctPosition = false;
    }
}
