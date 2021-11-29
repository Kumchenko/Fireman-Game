using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        Hero hero = collider.GetComponent<Hero>();

        if (bullet)
        {
            GetDamage();
        }


        if (hero)
        {
            hero.GetDamage();
        }
    }
    public override void Die()
    {
        Hero.Instance.AddProgress();
        Destroy(this.gameObject);
    }
}
