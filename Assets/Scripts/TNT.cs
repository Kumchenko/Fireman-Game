using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Hero.Instance.gameObject && !(anim.GetBool("exploded")))
        {
            anim.SetBool("touched", true);
            Hero.Instance.GetDamage();
            anim.SetBool("exploded", true);
        }
    }
}
