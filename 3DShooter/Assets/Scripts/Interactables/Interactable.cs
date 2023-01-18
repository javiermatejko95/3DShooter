using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private string message = string.Empty;
    #endregion

    #region PROPERTIES
    public string Message { get => message; }
    #endregion

    #region PUBLIC_METHODS
    public void Interact()
    {
        Interaction();
    }
    #endregion

    #region VIRTUAL_METHODS
    protected virtual void Interaction()
    {

    }
    #endregion
}
