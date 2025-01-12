using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjectPos : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform targetTransform;

    public void moveObject()
    {
        objectToMove.transform.position = targetTransform.position;
    }
}
