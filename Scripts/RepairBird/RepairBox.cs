using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class RepairBox : MonoBehaviour
{
    [SerializeField] private float partHoldTime;
    [SerializeField] private PartTypeEnum partTypeEnum;
    [SerializeField] private MMFeedbacks feedbacks;
    [SerializeField] private Image progressCircle;

    private bool mouseIsOver = false;
    private bool freshPress = true;
    private float partHoldTimer;

    private void Update()
    {
        if (mouseIsOver)
        {
            if (RepairManager.Instance.birdUnderRepair != null)
            {

                if (Input.GetMouseButton(0))
                {
                    if (freshPress)
                    {
                        if (partHoldTimer > 0)
                        {
                            partHoldTimer -= Time.deltaTime;
                            progressCircle.fillAmount = (partHoldTime - partHoldTimer) / partHoldTime;
                        }
                        else
                        {
                            RepairManager.Instance.RepairBird(partTypeEnum);
                            freshPress = false;
                            mouseIsOver = false;
                            feedbacks.StopFeedbacks();
                        }
                    }

                }
                else
                {
                    StopRepairing();
                    freshPress = true;
                }
            }
            else
            {
                StopRepairing();
            }
        }
    }

    private void StopRepairing()
    {
        progressCircle.fillAmount = 0;
        mouseIsOver = false;
        feedbacks.StopFeedbacks();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (!mouseIsOver)
            {
                if (RepairManager.Instance.birdUnderRepair != null)
                {
                    progressCircle.fillAmount = 0;
                    partHoldTimer = partHoldTime;
                    mouseIsOver = true;
                    feedbacks.PlayFeedbacks();
                }
            }
        }
    }

    void OnMouseExit()
    {
        if (mouseIsOver)
        {
            StopRepairing();
        }
    }
}
