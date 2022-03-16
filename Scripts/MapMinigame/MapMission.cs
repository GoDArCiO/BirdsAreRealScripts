using System;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapMission : MonoBehaviour
{
    [SerializeField] private GameEvent resetEvent;
    [SerializeField] private FloatVariable timeWindow;
    private MissionManager manager;
    private Mission mission;

    public void SetParams(MissionManager manager, Mission mission){
        this.manager = manager;
        this.mission = mission;
    }

    private void Start(){
        resetEvent.AddListener(GameReset);
    }

    private float t = 0;

    public void ShowMission(){
        manager.Show(mission,this);
    }

    private void FixedUpdate(){
        t += Time.deltaTime;
        if (t > timeWindow.Value){
            manager.Fail();
            Destroy(gameObject);
            t = 0;
        }
    }

    private void OnDestroy(){
        resetEvent.RemoveListener(GameReset);
    }

    private void GameReset(){
        Destroy(gameObject);
    }
}
