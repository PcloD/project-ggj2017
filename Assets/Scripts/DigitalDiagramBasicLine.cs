using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalDiagramBasicLine : MonoBehaviour {
    [SerializeField] private Transform _anchorLeft;
    [SerializeField] private Transform _anchorRight;
    void Awake () {
        Vector3 leftPos = _anchorLeft.position;
        leftPos.x -= 50;
        Vector3 rightPos = _anchorRight.position;
        rightPos.x += 50;
        GetComponent<LineRenderer>().SetPositions(new Vector3[] { leftPos, rightPos });
    }
}
