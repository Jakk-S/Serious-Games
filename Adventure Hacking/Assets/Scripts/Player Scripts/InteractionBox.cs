using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionBox : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject interPrompt;

    private GameObject interactionObject;

    private bool newCode = false;

    [Header("Tutorial")]
    [SerializeField] private bool inTuto;

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

            GameController gc = Camera.main.GetComponent<GameController>();

            if (interactionObject.CompareTag("Glitch"))
            {
                gc.StartHack(newCode);
                if (inTuto)
                {
                    Camera.main.GetComponent<TutorialManager>().NewTuto();
                }

            }
            else if (interactionObject.CompareTag("Code"))
            {
                newCode = true;
                gc.NewCodeStart();
                if (inTuto)
                {
                    Camera.main.GetComponent<TutorialManager>().NewTuto();
                }
            }
        }
    }
}
