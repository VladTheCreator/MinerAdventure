using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stone : MonoBehaviour
{

    [SerializeField] private Drop[] possibleDrops;
    [SerializeField] private ParticleSystem smash;
    [SerializeField] private float particleTime;
    [SerializeField] private float speed;
    private readonly int chanceOfNothing = 70;
    
    public bool DropSmth()
    {
       
        int rollForNothing = Random.Range(0, 101);
        if(rollForNothing < chanceOfNothing)
        {
            return false;
        }
        int itemWeight = 0;
        for (int i = 0; i < possibleDrops.Length; i++)
        {
            itemWeight += possibleDrops[i].GetDropChance();
        }
        int chance = Random.Range(0, itemWeight);
        for (int i = 0; i < possibleDrops.Length; i++)
        {
            if (chance <= possibleDrops[i].GetDropChance())
            {
                Instantiate(possibleDrops[i], transform.position, Quaternion.identity);
                return true;
            }
            chance -= possibleDrops[i].GetDropChance();
        }
        return false;
    }
    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;  
    }
    private void DestroyWithParticles()
    {
        ParticleSystem particales = Instantiate(smash, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(particales, particleTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Pickax pickax) && pickax.PickaxHandler.IsSwinging)
        {
            DropSmth();
            DestroyWithParticles();
        }  
    }
}
