﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GamePhoto : MonoBehaviour
{
    public Texture2D bestPhotoTexture => _bestPhotoTexture;
    public int bestPhotoScores => _bestPhotoScore;

    public void doPhoto() {
        int thePhotoScores = getScoresByPhoto();
        _flash.DOFade(1, 0);
        _flash.DOFade(0, 0.5f);
        _audioSource.time = 0.8f;
        _audioSource.Play();
        if (thePhotoScores >= _bestPhotoScore) {
            StartCoroutine(capture.doCapture(_camera, (Texture2D inTexture)=>{
                _bestPhotoTexture = inTexture;
                _bestPhotoScore = thePhotoScores;
            }));
        }
    }

    private int getScoresByPhoto() {
        return getChildrenNumInCaptureZone();
    }

    private int getChildrenNumInCaptureZone() {
        Rect theCaptureBounds = capture.captureZoneWorldRect;

        int theResult = 0;
        foreach (ChildAI theChild in _children) {
            if (theCaptureBounds.Contains(theChild.transform.position))
                ++theResult;
        }
        return theResult;
    }

    bool pressed = false;
    private void Update()
    {
        bool theKeyIsPressed = Input.GetKey(_activationKey);
        if (theKeyIsPressed) {
            if (!pressed) {
                doPhoto();

                pressed = true;
            }
        } else {
            pressed = false;
        }
    }


    ScreenCapture capture => GetComponent<ScreenCapture>();

    private void Awake() { _children = Object.FindObjectsOfType<ChildAI>(); }

    //Fields
    [SerializeField] private Camera _camera = null;
    [SerializeField] private SpriteRenderer _flash = null;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private KeyCode _activationKey = KeyCode.Space;

    private ChildAI[] _children = null;

    private Texture2D _bestPhotoTexture = null;
    private int _bestPhotoScore = int.MinValue;
}
