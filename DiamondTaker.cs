using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondTaker : MonoBehaviour
{
    private int diamondCount;
    private readonly int maxDiamonds = 10;
    private float maxImageWidth;
    public System.Action allDiamondsCollected;
    [SerializeField] private Image fillImage;
    [SerializeField] private float stonesToRemoveDiamond;
    [SerializeField] private float enemiesToRemoveDiamond;
    [SerializeField] private AudioClip diamondClip;
    [SerializeField] private AudioClip trashClip;
    private AudioSource audioSource;
    private float stoneStack;
    private float enemyStack;   
    private void Awake()
    {
        maxImageWidth = fillImage.GetComponent<RectTransform>().sizeDelta.x;
        fillImage.GetComponent<RectTransform>().sizeDelta =
          new Vector3(0,
          fillImage.GetComponent<RectTransform>().sizeDelta.y);
        audioSource = GetComponent<AudioSource>();  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diamond")
        {
            AddDiamond();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            AddToEnemyStack();
        }
        if(collision.gameObject.TryGetComponent(out Stone stone))
        {
            AddToStoneStack();
        }
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.DestroyHealth();
        }
        else
        {
            Destroy(collision.gameObject, 0.5f);
        }
    }
    private void AddToStoneStack()
    {
        stoneStack++;
        audioSource.clip = trashClip;
        audioSource.Play();
        if (stoneStack >= stonesToRemoveDiamond)
        {
            RemoveDiamond();
            stoneStack = 0;   
        }
    }
    private void AddToEnemyStack()
    {
        enemyStack++;
        audioSource.clip = trashClip;
        audioSource.Play();
        if (enemyStack >= enemiesToRemoveDiamond)
        {
            RemoveDiamond();
            enemyStack = 0;
        }
    }
    private void RemoveDiamond()
    {
        diamondCount--;
        fillImage.GetComponent<RectTransform>().sizeDelta =
           new Vector3((maxImageWidth / maxDiamonds) * diamondCount,
           fillImage.GetComponent<RectTransform>().sizeDelta.y);
    }
    private void AddDiamond()
    {
        if (diamondCount < maxDiamonds)
        {
            diamondCount++;
            fillImage.GetComponent<RectTransform>().sizeDelta =
               new Vector3((maxImageWidth / maxDiamonds) * diamondCount,
               fillImage.GetComponent<RectTransform>().sizeDelta.y);
            audioSource.clip = diamondClip;
            audioSource.Play();
        }
        if (diamondCount >= maxDiamonds)
        {
            allDiamondsCollected?.Invoke();
        }
    }
}
