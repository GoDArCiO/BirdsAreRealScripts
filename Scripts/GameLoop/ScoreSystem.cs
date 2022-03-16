using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using ScriptableObjectArchitecture;

public class ScoreSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MainUiReferences mainUiReferences;
    [SerializeField] private GameEvent failEvent;
    [SerializeField] private GameEvent pause;
    [SerializeField] private GameEvent got10Points;
    [SerializeField] private GameEvent gotPoint;
    [SerializeField] private MMFeedbacks scoreSound;

    [Header("Settings")]
    [SerializeField] private float suspicionCoolDownTime = 1f;
    [SerializeField] private float maxSuspicionMeter = 100;
    [SerializeField] private float susPoints = 10;
    [SerializeField] private float scorePoints = 1;

    private float score;
    private float suspicionMeter;
    private float suspicionCoolDown;
    private bool suspicionCoolingDown;

    [ContextMenu("Fail")]
    public void Fail()
    {
        OnFailedDo(susPoints);
    }

    [ContextMenu("Succed")]
    public void Succed()
    {
        OnSuccessDo(scorePoints);
    }

    public void Restart()
    {
        OnRestartDo();
    }

    private void OnRestartDo()
    {
        score = 0;
        suspicionMeter = 0;
        ChangeSusMeter(0);
        mainUiReferences.scoreText.text = "0";
    }

    private void OnFailedDo(float suspicion)
    {
        ChangeSusMeter(suspicionMeter += suspicion);
        suspicionCoolDown = suspicionCoolDownTime;

        if (maxSuspicionMeter <= suspicionMeter)
        {
            OnEndDo();
        }
    }

    private void OnSuccessDo(float score)
    {
        this.score += score;
        gotPoint.Raise();
        if (this.score%15 == 0 && Random.Range(0,2)<1) got10Points.Raise();
        mainUiReferences.scoreText.text = "" + this.score;
        scoreSound.PlayFeedbacks();
    }

    private void OnEndDo()
    {
        mainUiReferences.endGamePanel.SetActive(true);
        mainUiReferences.endScoreText.text = score+"";
        pause.Raise();
    }

    private void ChangeSusMeter(float value)
    {
        suspicionMeter = value;
        mainUiReferences.susBar.UpdateBar(suspicionMeter, 0, maxSuspicionMeter);
    }

    private void SusBarHandling()
    {
        if (suspicionCoolDown > 0)
        {
            suspicionCoolingDown = true;
            suspicionCoolDown -= Time.deltaTime;
            mainUiReferences.susCube.fillAmount = suspicionCoolDown / suspicionCoolDownTime;
        }
        else
        {
            if (suspicionCoolingDown)
            {
                ChangeSusMeter(0);
                suspicionCoolingDown = false;
            }
        }

        
    }

    private void Update()
    {
        SusBarHandling();
    }
}
