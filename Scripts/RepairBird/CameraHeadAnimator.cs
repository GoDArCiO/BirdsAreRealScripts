using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraHeadAnimator : MonoBehaviour
{
    public Transform headBox;

    [Header("animation settings")]
    [SerializeField] private DamagedBird damagedBird;
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private Transform headParent;
    [SerializeField] private Transform localPosStart;
    [SerializeField] private Transform localPosEnd;
    [SerializeField] private Vector3 headRotate;
    [SerializeField] private float firstMovementDuration;
    [SerializeField] private float secondMovementDuration;

    private GameObject animatedCamera;

    public void HeadRepairAnimation()
    {
        MoveHeadOverTheBird();
    }

    private void MoveHeadOverTheBird()
    {
        animatedCamera = Instantiate(headPrefab, headBox.position, localPosStart.rotation);
        animatedCamera.transform.SetParent(headParent, false);
        animatedCamera.transform.position = headBox.position;
        animatedCamera.transform.DOLocalMove(localPosStart.localPosition, firstMovementDuration).OnComplete(ScrewHeadOnBird);
    }

    private void ScrewHeadOnBird()
    {
        animatedCamera.transform.DOLocalMove(localPosEnd.localPosition, secondMovementDuration);
        animatedCamera.transform.DOLocalRotate(headRotate, secondMovementDuration, RotateMode.LocalAxisAdd).OnComplete(OnCameraRepairAnimationComplete);
    }

    private void OnCameraRepairAnimationComplete()
    {
        animatedCamera.SetActive(false);
        damagedBird.cameraHead.DamagedView(false);
    }
}
