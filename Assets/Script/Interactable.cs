using NaughtyAttributes;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public float _radius;

    public Transform _interactionTransform;
    bool _isFocus = false;
    Transform _player;
    bool _hasInteracted = false;

    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Interating with" + transform.name);
    }

    private void Update()
    {
        if(_isFocus && !_hasInteracted)
        {
            float distance = Vector3.Distance(_player.position, _interactionTransform.position);
            if(distance <= _radius)
            {
                Interact();
                _hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        _isFocus = true;
        _player = playerTransform;
        _hasInteracted = false;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _player = null;
        _hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(_interactionTransform == null)
        {
            _interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionTransform.position, _radius);
    }
}
