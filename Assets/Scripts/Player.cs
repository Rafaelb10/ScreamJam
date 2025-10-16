using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Componentes")]
    private Rigidbody rb;
    private Camera cam;

    [Header("Movimentação")]
    private float moveSpeed = 5f;
    private float runMultiplier = 2f;
    private float crouchMultiplier = 0.25f;
    private float rotationSpeed = 10f;
    private float crouchHeight = 0.25f;
    private float originalHeight;
    private bool isRunning = false;
    private bool isCrouching = false;

    [Header("Interação")]
    public float interactDistance = 5f;

    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        originalHeight = transform.localScale.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Mover();
    }

    private void Mover()
    {
        if (cam == null) return;

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * moveInput.y + right * moveInput.x).normalized;

        Vector3 move = direction * moveSpeed * (isRunning ? runMultiplier : 1f) * (isCrouching ? crouchMultiplier : 1f);
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.started) isRunning = true;
        if (context.canceled) isRunning = false;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = !isCrouching;
            Vector3 scale = transform.localScale;
            scale.y = isCrouching ? crouchHeight : originalHeight;
            transform.localScale = scale;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && cam != null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                var interactable = hit.collider.GetComponent<IInteract>();
                if (interactable != null)
                {
                    interactable.Interagir();
                }
            }
        }
    }
}