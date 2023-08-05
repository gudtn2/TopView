using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using CharacterController;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Vector3 MouseDir { get; private set; }
    public Player player { get; private set; }
    public Vector3 inputDir { get; private set; }
    public Vector3 moveDir { get; private set; }

    MoveState moveState;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

        moveState = player.stateMachine.GetState(StateName.Move) as MoveState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (player.IsDied)
        {
            inputDir = Vector3.zero;
            return;
        }

        Vector2 input = context.ReadValue<Vector2>();
        inputDir = new Vector3(input.x, 0f, input.y);
    }
}
