using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform orientationTransform;
    [SerializeField] private Transform playerVisualTransform;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        if(GameManager.Instance.GetCurrentGameState() != GameState.Play 
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        Vector3 viewDirection = playerTransform.position - new Vector3(transform.position.x,
            playerTransform.position.y, transform.position.z);
        orientationTransform.forward = viewDirection.normalized;

        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical = -1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical = 1f;

            moveInput = new Vector2(horizontal, vertical);
        }

        Vector3 inputDirection = orientationTransform.forward * moveInput.y
            + orientationTransform.right * moveInput.x;

        if (inputDirection != Vector3.zero)
        {
            playerVisualTransform.forward = Vector3.Slerp(playerVisualTransform.forward,
                inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}