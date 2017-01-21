using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(RectTransform))]
public abstract class AbsRectTransformShowHideAction : MonoBehaviour {

    public Action showCompleteCallback;
    public Action hideCompleteCallback;
    public abstract void Show(params object[] options);
    public abstract void Hide(params object[] options);
}
