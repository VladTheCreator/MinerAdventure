using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickax : MonoBehaviour
{
    [SerializeField] private PickaxHandler pickaxHandler;
    public PickaxHandler PickaxHandler=> pickaxHandler; 
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pickaxHandler.IsSwinging)
        {
            audioSource.Play();
            //if (collision.gameObject.TryGetComponent(out Health health)
            //    && collision.gameObject.tag == "Enemy")
            //{
            //    health.TakeOneDamage(name);
            //}
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickaxHandler.IsSwinging)
        {
            audioSource.Play();
            if (collision.gameObject.TryGetComponent(out Health health)
                && collision.gameObject.tag == "Enemy")
            {
                health.TakeOneDamage(name);
            }
        }
    }
}
