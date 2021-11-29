using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Hero hero = collider.GetComponent<Hero>();

        if (hero && hero.Lives < 5)
        {
            ++hero.Lives;
            Debug.Log(hero.Lives);
            Destroy(gameObject);
        }
    }
}
