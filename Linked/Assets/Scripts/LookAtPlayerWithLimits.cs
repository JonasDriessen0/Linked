using UnityEngine;
using DG.Tweening;

public class LookAtPlayerWithLimits : MonoBehaviour
{
    public Transform player;
    public Transform head;

    void LateUpdate()
    {
        head.transform.LookAt(player);
    }
}