using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCarrier : MonoBehaviour
{
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask diamondMask;
    [SerializeField] private float throwForth;
    private Rigidbody2D diamond;

    private void Update()
    {
        if (diamond != null)
        {
            MoveDiamondWithCarrier();
        }
    }
    public bool CarryingDiamond()
    {
        return diamond != null;
    }
    private void MoveDiamondWithCarrier()
    {
        diamond.transform.position = CarryingPosition();
    }

    private Rigidbody2D CastForDiamonds()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, castDistance, diamondMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, castDistance, diamondMask);
        if (hitLeft)
        {
            return hitLeft.collider.gameObject.GetComponent<Rigidbody2D>();
        }
        else if (hitRight)
        {
            return hitRight.collider.gameObject.GetComponent<Rigidbody2D>();
        }
        return null;
    }
    private Vector3 CarryingPosition()
    {
        return transform.position + Vector3.up; ;
    }
    public void PickDiamond()
    {
        Rigidbody2D diamond = CastForDiamonds();
        if (diamond != null)
        {
            diamond.transform.position = CarryingPosition();
            this.diamond = diamond;
        }
    }
    public void ThrowDiamond(float horizontalInputRaw)
    {
        Rigidbody2D d = diamond;
        diamond = null;
        d.AddForce(new Vector3(horizontalInputRaw * throwForth, throwForth), ForceMode2D.Impulse);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.left * castDistance);
    }
}
