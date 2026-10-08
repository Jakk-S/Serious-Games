using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveInputs;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Camera.main.GetComponent<GameController>().Hacking) return;
        rb.linearVelocity = moveInputs * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Attempting Move!");
        moveInputs = context.ReadValue<Vector2>();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
