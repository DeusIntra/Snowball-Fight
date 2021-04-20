﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimator : MonoBehaviour
{
    public string initialMeshAnimation;

    [Tooltip("Mesh Animations Parent")]
    public Transform holder;

    private MeshFilter _meshFilter;
    private Dictionary<string, AnimatedMeshSequence> _meshAnimations;
    private AnimatedMeshSequence _currentMeshAnimation;
    public bool isPaused { get; private set; }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshAnimations = new Dictionary<string, AnimatedMeshSequence>();

        foreach (Transform child in holder)
        {
            var seq = child.GetComponent<AnimatedMeshSequence>();
            seq.SetMeshFilter(_meshFilter);
            _meshAnimations.Add(child.name, seq);
        }
    }

    private void Start()
    {
        bool isValid = _meshAnimations.TryGetValue(initialMeshAnimation, out _currentMeshAnimation);
        if (!isValid) Debug.LogError("Initial mesh animation is not valid");
        else _currentMeshAnimation.Play();
    }

    public void Play(string name)
    {
        if (_currentMeshAnimation != null)
            _currentMeshAnimation.Stop();

        bool isValid = _meshAnimations.TryGetValue(name, out _currentMeshAnimation);
        if (!isValid) Debug.LogError(name + " mesh animation is not valid");
        else _currentMeshAnimation.Play();
    }

    public void SetFPS(float FPS)
    {
        if (_currentMeshAnimation != null)
            _currentMeshAnimation.framesPerSecond = FPS;
    }

    public float GetFPS()
    {
        return _currentMeshAnimation.framesPerSecond;
    }

    public void Pause(float seconds)
    {
        StartCoroutine(PauseCoroutine(seconds));
    }

    public void SlowDown(bool flag)
    {
        foreach (var entry in _meshAnimations)
        {
            AnimatedMeshSequence animation = entry.Value;
            animation.isSlowDown = flag;
        }
    }

    private IEnumerator PauseCoroutine(float seconds)
    {
        _currentMeshAnimation.Stop();
        isPaused = true;

        yield return new WaitForSeconds(seconds);

        _currentMeshAnimation.Play();
        isPaused = false;
    }
}
