using NaughtyAttributes;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float _radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
