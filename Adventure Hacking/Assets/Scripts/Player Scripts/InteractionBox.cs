using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionBox : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject interPrompt;

    private GameObject interactionObject;

    private void Start()
    {
        interPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactionObject = collision.gameObject;
        interPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactionObject != collision.gameObject) return;
        interactionObject = null;
        interPrompt.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactionObject == null) return;
            Camera.main.GetComponent<GameController>().StartHack();
        }
    }
}
