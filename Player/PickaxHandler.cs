using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxHandler : MonoBehaviour
{
    [SerializeField] private float maxAngleLeft;
    [SerializeField] private float minAngleLeft;
    [SerializeField] private float rotationSpeed;
    private bool isNotSwinging = true;
    public bool IsSwinging => !isNotSwinging;
    private bool interruptSwing = false;
    [SerializeField] private Rigidbody2D pickaxRb;
    public Rigidbody2D Rigidbody => pickaxRb;
    private void Awake()
    {
        SetZAngle(minAngleLeft);
    }
    public void Swing(bool facingRight)
    {
        pickaxRb.simulated = true; // this is to make pickax interactable with other colliders
        if (isNotSwinging)
        {
            StartCoroutine(SwingEnumerator(DefineRotationStages(facingRight), facingRight));
            isNotSwinging = false;
        }
    }
    
    public void InterruptSwing(bool facingRight)
    {
        interruptSwing = true;
        IdlePickaxPosition(facingRight);
    }
    private void IdlePickaxPosition(bool facingRight)
    {
        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y,
                -minAngleLeft);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y,
                minAngleLeft);
        }
    }
    private List<float> DefineRotationStages(bool facingRight)
    {
        List<float> rotationStages = new List<float>();
        if (facingRight)
        {
            float max = -maxAngleLeft;
            float min = -minAngleLeft;
            float totalAngle = Mathf.Abs(max - min);
            for (float i = 0; i < (totalAngle / 10) + 1; i++)
            {
                rotationStages.Add(min - (i * 10));
            }
        }
        else
        {
            float max = maxAngleLeft;
            float min = minAngleLeft;
            float totalAngle = Mathf.Abs(max - min);
            for (float i = 0; i < (totalAngle / 10) + 1; i++)
            {
                rotationStages.Add(min + (i * 10));
            }
        }
        return rotationStages;
    }
    private IEnumerator SwingEnumerator(List<float> rotationStages, bool facingRight)
    {
        interruptSwing = false;
        for (int i = 0; i < rotationStages.Count && !interruptSwing; i++)
        {
            SetZAngle(rotationStages[i]);
            yield return new WaitForSeconds(0.03f);
        }
        rotationStages.Reverse();
        for (int i = 0; i < rotationStages.Count && !interruptSwing; i++)
        {
            SetZAngle(rotationStages[i]);
            yield return new WaitForSeconds(0.03f);
        }
        isNotSwinging = true;
    }
    private void SetZAngle(float zAngle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x,
            transform.rotation.y, zAngle);
    }
}
