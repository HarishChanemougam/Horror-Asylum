using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAnimation : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerMovement _movement;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float PlayerAnimator = _movement._speed / _movement._runSpeed;
        _animator.SetFloat("PlayerAnimator", PlayerAnimator, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
