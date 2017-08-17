using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;
    public Transform interactionTransform;
    bool isFocus = false;
    Transform player;

    private void Update()
    {
        if (isFocus)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float distance = Vector3.Distance(player.position, transform.position);
                if (distance <= radius)
                {
                    Interact();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual void Interact()
    {
        //this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
    }

}
