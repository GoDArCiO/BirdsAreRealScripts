using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour{
    [SerializeField] private List<Mission> missions;
    [SerializeField] private GameObject missionUIprefab;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private MissionDisplay display;
    [SerializeField] private Transform missionObjectContainer;

    private MapMission activeMissionObject;
    private Mission activeMission;
    
    [SerializeField] private GameEvent failEvent;
    [SerializeField] private GameEvent successEvent;
    [SerializeField] private GameEvent resetEvent;
    [SerializeField] private FloatVariable spawnDelay;
    private float t = 0;

    void Start(){
        resetEvent.AddListener(GameReset);
    }

    void GameReset(){
        t = 0;
    }

    void Update(){
        t += Time.deltaTime;
        if (t > spawnDelay.Value){
            Spawn();
            t = 0;
        }
    }

    void Spawn(){
        Vector2 pos = new Vector2(Random.Range(-.3f,.3f),
                                  Random.Range(-.25f,.3f));
        var obj = Instantiate(missionUIprefab).GetComponent<RectTransform>();
        obj.SetParent(missionObjectContainer,false);
        obj.anchoredPosition = pos;
        Mission mission = missions[Random.Range(0, missions.Count)];
        obj.GetComponent<MapMission>().SetParams(this,mission);;
    }

    public void Show(Mission mission,MapMission missionObject){
        activeMissionObject = missionObject;
        activeMission = mission;
        display.Show(mission);
    }

    public void LeftPress(){
        if(activeMission.leftIsSucced)
            Success();
        else
            Fail();
    }

    public void RightPress(){
        if(!activeMission.leftIsSucced)
            Success();
        else
            Fail();
    }

    public void Success(){
        successEvent.Raise();
        MissionDone();
    }

    public void Fail(){
        failEvent.Raise();
        MissionDone();
    }

    private void MissionDone(){
        activeMission = null;
        display.Hide();
        if(activeMissionObject!=null)
            Destroy(activeMissionObject.gameObject);
    }
}
