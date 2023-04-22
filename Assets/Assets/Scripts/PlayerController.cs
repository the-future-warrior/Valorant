using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Vector3 startingRotation;
    [SerializeField ]private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        inputManager = InputManager.Instance;
        //cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward* move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }

        // Changes the height position of the player..
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        gameObject.transform.rotation = cameraTransform.rotation;
        Vector2 deltaInput = inputManager.GetMouseDelta();
        //Vector3 startingRotation;
        if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
        startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
        startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;

        startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
        gameObject.transform.rotation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);



        
        if(inputManager.PlayerFiredThisFrame()) {
            animator.SetTrigger("isFired");
        }
    }
}
