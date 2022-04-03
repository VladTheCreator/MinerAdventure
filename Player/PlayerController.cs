using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour
{
    private Mover mover;
    [SerializeField] private PickaxHandler pickaxHandler;
    private DiamondCarrier diamondCarrier;
    private Health health;
    private Animator animator;
    public Health Health => health;
    void Awake()
    {
        health = GetComponent<Health>();
        mover = GetComponent<Mover>();
        diamondCarrier = GetComponent<DiamondCarrier>();
        mover.flip += pickaxHandler.InterruptSwing;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ControlMovement();
        ControlHitting();
        ControlPickUpAndThrow();
    }
    private void ControlMovement()
    {
        float input = Input.GetAxis("Horizontal");
        float inputRaw = Input.GetAxisRaw("Horizontal");
        if (pickaxHandler.IsSwinging)
        {
            //mover.ToggleSimulation();
            mover.SetXVelocityToZero();
            input = 0;
            inputRaw = 0;
        }
        else
        {
            //mover.ToggleSimulation();
        }
        animator.SetFloat("verticalSpeed", Mathf.Abs(input));
        if (mover.Grounded())
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }
        mover.RunRL(input, inputRaw);
        if (Input.GetButtonDown("Jump"))
        {
            mover.Jump(pickaxHandler.Rigidbody);
        }
    }
    private void ControlPickUpAndThrow()
    {
        if (Input.GetKeyDown(KeyCode.E) && !diamondCarrier.CarryingDiamond())
        {
            diamondCarrier.PickDiamond();
        }
        else if (Input.GetKeyDown(KeyCode.E) && diamondCarrier.CarryingDiamond())
        {
            diamondCarrier.ThrowDiamond(mover.CurrentHorizontalInputRaw);
        }
    }
    private void ControlHitting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pickaxHandler.Swing(mover.facingRight);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
