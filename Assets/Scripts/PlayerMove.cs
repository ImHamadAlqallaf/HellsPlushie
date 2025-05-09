using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float momentumDamping = 5f;

    private CharacterController myCC;
    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
    }

    void GetInput()
    {
        Vector3 rawInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        rawInput = transform.TransformDirection(rawInput.normalized);

 
        inputVector = Vector3.Lerp(inputVector, rawInput, momentumDamping * Time.deltaTime);

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
}
