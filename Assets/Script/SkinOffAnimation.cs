using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkinOffAnimation : MonoBehaviour
{
    const float _locomotionAnimationSmoothTime = 0.1f;
    [SerializeField] Animator _animator;
    [SerializeField] NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float SkinOffAnimator = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("SkinOffBlend", SkinOffAnimator, _locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
