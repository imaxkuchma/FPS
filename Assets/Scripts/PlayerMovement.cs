using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 moveDir = Vector3.zero;
    private Vector3 verticalVelosity = Vector3.zero;
    private JoystickDetector _JoystickDetector;

    private bool isGrounded;
    private bool Jumping;

    private bool _isMovementStarted;
    private Vector2 _movementDirection;

    [SerializeField] private GameObject JoystickForMove;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float jumpHight = 3f;
    [SerializeField] private float gravity = -9.81f;


    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        _JoystickDetector = JoystickForMove.GetComponent<JoystickDetector>();

        _JoystickDetector.IsJoystickUse += SetIsMovementStarted;
        _JoystickDetector.Direction += OnDirectionChange;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        //Player movement

        moveDir = new Vector3(_movementDirection.x, 0, _movementDirection.y);
        moveDir = transform.TransformDirection(moveDir);

        if (_isMovementStarted)
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);

        //Player jump
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                verticalVelosity.y =   Mathf.Sqrt((jumpHight) * -2f * gravity);
            }
            else 
            {
                verticalVelosity.y = 0;
            }
        }
        else
        {
            verticalVelosity.y += gravity * Time.deltaTime;
        }

        characterController.Move(verticalVelosity * Time.deltaTime);

    }

    private void SetIsMovementStarted(bool isMovementStarted)
    {
        _isMovementStarted = isMovementStarted;
    }

    private void OnDirectionChange(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }

}
