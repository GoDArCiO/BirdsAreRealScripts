using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent resetEvent;
    
    [Space,Header("Spawn delays and time windows")]
    [SerializeField] private FloatVariable cageSpawnDelay;
    [SerializeField] private FloatVariable repairSpawnDelay;
    [SerializeField] private FloatVariable mapSpawnDelay;

    [SerializeField] private FloatVariable cageTimeWindow;
    [SerializeField] private FloatVariable repairTimeWindow;
    [SerializeField] private FloatVariable mapTimeWindow;

    [Space, Header("Difficulty Settings")] 
    [Header("Cage minigame")] 
    [SerializeField] private float startSpawnDelayCage;
    [SerializeField] private float endSpawnDelayCage;
    [SerializeField] private float startTimeWindowCage;
    [SerializeField] private float endTimeWindowCage;
    [SerializeField] private float timeToMaxDifficultyCage = 60f;
    [SerializeField] private float timeToStartDifficultyIncreaseCage = 5f;
    private float cageTimer = 0;
    
    [Header("Repair minigame")] 
    [SerializeField] private float startSpawnDelayRepair;
    [SerializeField] private float endSpawnDelayRepair;
    [SerializeField] private float startTimeWindowRepair;
    [SerializeField] private float endTimeWindowRepair;
    [SerializeField] private float timeToMaxDifficultyRepair = 60f;
    [SerializeField] private float timeToStartDifficultyIncreaseRepair = 5f;
    private float repairTimer = 0;
        
    [Header("Map minigame")] 
    [SerializeField] private float startSpawnDelayMap;
    [SerializeField] private float endSpawnDelayMap;
    [SerializeField] private float startTimeWindowMap;
    [SerializeField] private float endTimeWindowMap;
    [SerializeField] private float timeToMaxDifficultyMap = 60f;
    [SerializeField] private float timeToStartDifficultyIncreaseMap = 5f;
    private float mapTimer = 0;

    private void Start(){
        resetEvent.AddListener(ResetState);
    }

    private void ResetState(){
        cageTimer = 0;
        repairTimer = 0;
        mapTimer = 0;
    }
    
    void FixedUpdate(){
        CageDifficultyIncrease();
        RepairDifficultyIncrease();
        MapDifficultyIncrease();
    }

    private void CageDifficultyIncrease(){
        cageTimer += Time.deltaTime;
        cageSpawnDelay.Value = Mathf.Lerp(startSpawnDelayCage, endSpawnDelayCage, (cageTimer - timeToStartDifficultyIncreaseCage)/timeToMaxDifficultyCage);
        cageTimeWindow.Value = Mathf.Lerp(startTimeWindowCage, endTimeWindowCage, (cageTimer - timeToStartDifficultyIncreaseCage)/timeToMaxDifficultyCage);
    }

    private void RepairDifficultyIncrease(){
        repairTimer += Time.deltaTime;
        repairSpawnDelay.Value = Mathf.Lerp(startSpawnDelayRepair, endSpawnDelayRepair, (repairTimer - timeToStartDifficultyIncreaseRepair)/timeToMaxDifficultyRepair);
        repairTimeWindow.Value = Mathf.Lerp(startTimeWindowRepair, endTimeWindowRepair, (repairTimer - timeToStartDifficultyIncreaseRepair)/timeToMaxDifficultyRepair);
    }
    
    private void MapDifficultyIncrease(){
        mapTimer += Time.deltaTime;
        mapSpawnDelay.Value = Mathf.Lerp(startSpawnDelayMap, endSpawnDelayMap, (mapTimer - timeToStartDifficultyIncreaseMap)/timeToMaxDifficultyMap);
        mapTimeWindow.Value = Mathf.Lerp(startTimeWindowMap, endTimeWindowMap, (mapTimer - timeToStartDifficultyIncreaseMap)/timeToMaxDifficultyMap);
    }
}
