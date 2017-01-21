using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimation : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Image>().material;
    }

    private void Update()
    {
        _material.SetTextureOffset("_MainTex", new Vector2(Time.time * _speed, 0));
    }
}