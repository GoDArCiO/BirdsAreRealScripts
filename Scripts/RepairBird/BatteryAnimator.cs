using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatteryAnimator : MonoBehaviour
{
    public Transform batteryBox;

    [Header("animation settings")]
    [SerializeField] private DamagedBird damagedBird;
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private Transform batteryParent;
    [SerializeField] private Transform batLocalPosStart;
    [SerializeField] private Transform batLocalPosEnd;
    [SerializeField] private Vector3 batRotate;
    [SerializeField] private float batFirstMovementDuration;
    [SerializeField] private float batSecondMovementDuration;
    [SerializeField] private GameObject batteryFlap;

    private GameObject animatedBattery;

    public void BatteryRepairAnimation()
    {
        MoveBatteryOverTheBird();
    }

    private void MoveBatteryOverTheBird()
    {
        animatedBattery = Instantiate(batteryPrefab, batteryBox.position, batLocalPosStart.rotation);
        animatedBattery.transform.SetParent(batteryParent, false);
        animatedBattery.transform.position = batteryBox.position;
        animatedBattery.transform.DOLocalMove(batLocalPosStart.localPosition, batFirstMovementDuration).OnComplete(InsertBattery);
    }

    private void InsertBattery()
    {
        animatedBattery.transform.DOLocalMove(batLocalPosEnd.localPosition, batSecondMovementDuration).OnComplete(CloseBatteryFlap);
    }

    private void CloseBatteryFlap()
    {
        batteryFlap.transform.DOLocalRotate(batRotate, batSecondMovementDuration, RotateMode.LocalAxisAdd).OnComplete(OnBatteryRepairAnimationComplete);
    }

    private void OnBatteryRepairAnimationComplete()
    {
        damagedBird.battery.DamagedView(false);
    }
}
