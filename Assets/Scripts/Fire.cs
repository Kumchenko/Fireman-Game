using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Monster
{
    private Heal heal;

    protected override void Awake()
    {
        heal = Resources.Load<Heal>("Heal");
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        Hero hero = collider.GetComponent<Hero>();

        if (bullet)
        {
            Vector3 position = transform.position;
            Heal newHeal = Instantiate(heal, position, heal.transform.rotation) as Heal;
            GetDamage();
        }


        if (hero)
        {
            hero.GetDamage();
        }
    }
}
