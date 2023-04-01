using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieGirlAnimation : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = 0.1f;
    [SerializeField] Animator _animator;
    [SerializeField] NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float ZombieGirlAnimator = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("ZombieGirlAnimator", ZombieGirlAnimator, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
