using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeartHolder : MonoBehaviour
{
    private List<GameObject> hearts;
    void Awake()
    {
        hearts = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            hearts.Add(transform.GetChild(i).gameObject);
        }
    }
    public void DeleteHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heart = hearts[0];
            hearts.Remove(hearts[0]);
            Destroy(heart);
        }
    }
    public void DeleteAllHearts()
    {
        if (hearts.Count > 0)
        {
            foreach (var h in hearts)
            {
                Destroy(h);
            }
            hearts.Clear();
        }
    }
}
