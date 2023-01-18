using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask = default;
    #endregion

    #region PRIVATE_FIELDS
    private bool initialized = false;

    private Transform camera = null;

    private PlayerUIActions playerUIActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(!initialized)
        {
            return;
        }

        Interact();
    }
    #endregion

    #region INIT
    public void Init(Transform camera, PlayerUIActions playerUIActions)
    {
        this.camera = camera;
        this.playerUIActions = playerUIActions;

        initialized = true;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Interact()
    {
        playerUIActions.onUpdateMessage?.Invoke(string.Empty);

        Ray ray = new Ray(camera.position, camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, distance, mask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                playerUIActions.onUpdateMessage?.Invoke(interactable.Message);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }
    #endregion
}
