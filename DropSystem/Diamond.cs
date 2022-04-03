using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Drop
{
    [SerializeField] private float speed;
    public override string GetName()
    {
        return "diamond";
    }
    public override int GetDropChance()
    {
        return 20;
    }
    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
