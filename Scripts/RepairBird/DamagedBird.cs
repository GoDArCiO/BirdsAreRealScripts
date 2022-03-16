using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;


public class DamagedBird : MonoBehaviour
{
    [SerializeField] private Color damaged;
    [SerializeField] private Color unDamaged;

    [SerializeField] private GameEvent startRepairEvent;

    public PartType battery;
    public PartType cameraHead;
    public PartType wings;
    private List<PartType> parts = new List<PartType>();

    public void Repair(PartTypeEnum partTypeEnum)
    {
        if (partTypeEnum == PartTypeEnum.BATTERY)
        {
            if (!battery.needRepair) return;
            battery.needRepair = false;
            battery.repairedHighlightEffect.outlineColor = unDamaged;

            batteryAnimator.BatteryRepairAnimation();
        }
        if (partTypeEnum == PartTypeEnum.CAMERA)
        {
            if (!cameraHead.needRepair) return;
            cameraHead.needRepair = false;
            cameraHead.repairedHighlightEffect.outlineColor = unDamaged;
            
            cameraHeadAnimator.HeadRepairAnimation();
        }
        if (partTypeEnum == PartTypeEnum.WINGS)
        {
            if (!wings.needRepair) return;
            wings.needRepair = false;
            wings.repairedHighlightEffect.outlineColor = unDamaged;
            wings.DamagedView(false);
        }

    }

    public bool IsDamaged()
    {
        foreach (PartType pt in parts)
        {
            if (pt.needRepair) return true;
        }
        return false;
    }

    public void OnClick()
    {
        startRepairEvent.Raise();
        foreach (PartType pt in parts)
        {
            pt.repairedHighlightEffect.SetHighlighted(true);
            if(pt.needRepair) pt.damagedHighlightEffect.SetHighlighted(true);

        }
        RepairManager.Instance.SetBird(this);
    }

    public void OnRepairEnd()
    {
        DisableHighlights();
    }

    public void DisableHighlights()
    {
        foreach (PartType pt in parts)
        {
            pt.repairedHighlightEffect.SetHighlighted(false);
        }
    }

    public void Initialize()
    {
        parts.Add(battery);
        parts.Add(cameraHead);
        parts.Add(wings);

        foreach (PartType pt in parts)
        {
            if(Random.Range(0,2)>0) pt.needRepair = false;
            else pt.needRepair = true;

            pt.DamagedView(pt.needRepair);
            if (pt.needRepair)
            {
                pt.repairedHighlightEffect.outlineColor = damaged;
            }
            else
            {
                pt.repairedHighlightEffect.outlineColor = unDamaged;
            }
        }
    }

    [Space, Header("Animations")]
    public BatteryAnimator batteryAnimator;
    public CameraHeadAnimator cameraHeadAnimator;
}
