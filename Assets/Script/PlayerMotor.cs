using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMotor : MonoBehaviour
{
    Transform _target;
    PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if( _target != null )
        {
            _playerMovement.SetDestination(_target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        _playerMovement.SetDestination(point);
    }
    public void FollowTarget(Interactable newTarget)
    {
        _playerMovement._stoppingDistance = newTarget._radius * .8f;
        _playerMovement.updateRotation = false;
        _target = newTarget._interactionTransform;
    }

    public void StopFollowingTarget()
    {
        _playerMovement._stoppingDistance = 0f;
        _playerMovement.updateRotation = true;
        _target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
