using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairManager : MonoBehaviourSingleton<RepairManager>
{
    [Header("RepairReferences")]
    public DamagedBird birdUnderRepair;
    [SerializeField] private Transform headBox;
    [SerializeField] private Transform batteryBox;

    public void SetBird(DamagedBird db)
    {
        if (db == birdUnderRepair) return;

        if(birdUnderRepair!=null) birdUnderRepair.DisableHighlights();

        db.batteryAnimator.batteryBox = batteryBox;
        db.cameraHeadAnimator.headBox = headBox;

        birdUnderRepair = db;
    }

    public void RemoveBird()
    {
        birdUnderRepair.DisableHighlights();
        birdUnderRepair = null;
    }

    public void RepairBird(PartTypeEnum partTypeEnum)
    {
        birdUnderRepair.Repair(partTypeEnum);
    }

}
