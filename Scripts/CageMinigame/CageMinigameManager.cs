using System;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using MoreMountains.Feedbacks;

public class CageMinigameManager : MonoBehaviour{

    [SerializeField] private GameEvent resetEvent;
    [SerializeField] private FloatVariable spawnDelay;
    
    [SerializeField] private InteractableScreen screen;
    [SerializeField] private List<GameObject> birdTypes;
    private List<ObjectPool<GameObject>> birdPools = new List<ObjectPool<GameObject>>();
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform waitPos;
    public MMFeedbacks feedbacks;

    void Start(){
        resetEvent.AddListener(ResetMinigameState);
        
        birdPools.Add(new ObjectPool<GameObject>(
            () => CreateBird(0),
            bird => { bird.SetActive(true); },
            bird => { bird.SetActive(false); },
            bird => { Destroy(bird); },
            true,
            6,
            6));
        birdPools.Add(new ObjectPool<GameObject>(
            () => CreateBird(1),
            bird => { bird.SetActive(true); },
            bird => { bird.SetActive(false); },
            bird => { Destroy(bird); },
            true,
            6,
            6));
        birdPools.Add(new ObjectPool<GameObject>(
            () => CreateBird(2),
            bird => { bird.SetActive(true); },
            bird => { bird.SetActive(false); },
            bird => { Destroy(bird); },
            true,
            6,
            6));
    }

    private void OnDestroy(){
        foreach (var pool in birdPools){
            pool.Dispose();
        }
    }

    private void ResetMinigameState(){
        t = 0;
    }
    
    private GameObject CreateBird(int type){
        var bird = Instantiate(birdTypes[type],GetWaitPos(),quaternion.identity).GetComponent<DraggableBird>();
        bird.screen = screen;
        bird.manager = this;
        bird.pool = birdPools[type];
        feedbacks.PlayFeedbacks();
        return bird.gameObject;
    }
    
    private float t = 0;
    
    private void Update(){
        t += Time.deltaTime;
        if (t > spawnDelay.Value){
            int type = Random.Range(0, birdTypes.Count);
            birdPools[type].Get();
            t = 0;
        }
    }

    public Vector3 GetSpawnPos(){
        return spawnPos.position;
    }

    public Vector3 GetWaitPos(){
        return waitPos.position  + waitPos.right * Random.Range(-2.1f, 2.1f);
    }
}
