using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _lookRadius;
    [SerializeField] Transform _target;
    [SerializeField] NavMeshAgent _agent;
   /* [SerializeField] Animator _animator;*/
    void Start()
    {
        _target = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if(distance <= _lookRadius)
        {
            _agent.SetDestination(_target.position);
            /*_animator.SetBool("Running Crawl", true);
*/
            if(distance <= _agent.stoppingDistance)
            {
                //enemy attack
                FaceTarget();
            }
        }
       /* else
        {
            _animator.SetBool("Running Crawl", false);
        }    */   
    }

   /* private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "PlayerTag")
        {
            _animator.SetBool("Double Leg Takedown - Attacker", true);

            if(_animator == true)
            {
                _animator.SetBool("Zombie Neck Bite", true);
            }
            else
            {
                _animator.SetBool("Zombie Neck Bite", false);
            }
        }
        else
        {
            _animator.SetBool("Double Leg Takedown - Attacker", false);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider == null)
        {
            _animator = null;
        }
    }*/

    void FaceTarget()
    {
        Vector3 direction = _target.position - transform.position.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }
}
