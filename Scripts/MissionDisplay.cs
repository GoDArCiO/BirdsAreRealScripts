using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MissionDisplay : MonoBehaviour{
    [SerializeField] private UnityEvent missionHide;
    [SerializeField] private UnityEvent missionShow;
    [SerializeField] private TextMeshProUGUI text;

    public void Hide(){
        missionHide.Invoke();
    }

    public void Show(Mission mission){
        text.text = mission.MissionTitle;
        missionShow.Invoke();
    }
}
