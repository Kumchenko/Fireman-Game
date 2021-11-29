using UnityEngine;
using System.Collections;

public class LivesBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];

    private Hero hero;

    private SpriteRenderer sprite;


    private void Awake()
    {
        hero = FindObjectOfType<Hero>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < hearts.Length; ++i)
        {
            if (i < hero.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
