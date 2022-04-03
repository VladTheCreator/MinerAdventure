using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private HeartHolder heartHolder;
    [SerializeField] private float invincibleTime;
    private AudioSource audioSource;
    [SerializeField] private AudioClip takeDamage;
    public event System.Action OnDestroy;
    private void Awake()
    {
        StartCoroutine(DecreaseTime());
        audioSource = GetComponent<AudioSource>();
    }
    private IEnumerator DecreaseTime()
    {
        while (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            yield return null;
        }
    }
    public void TakeOneDamage(string attackerName)
    {
        if (invincibleTime <= 0)
        {

            healthPoints--;
            heartHolder?.DeleteHeart();
            audioSource.clip = takeDamage;  
            audioSource?.Play();    
            if (healthPoints <= 0)
            {
                DestroyHealth();
            }
        }
    }
    public void DestroyHealth()
    {
        healthPoints = 0;
        heartHolder?.DeleteAllHearts();
        Destroy(gameObject, 0.3f);
        OnDestroy?.Invoke();
    }
}
