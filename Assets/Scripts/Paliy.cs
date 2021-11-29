using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Paliy : Monster
{
    [SerializeField] private int lives = 7;

    private float speed = 2f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        dir = transform.right * -1F;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }
    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public override void GetDamage()
    {
        lives--;
        Debug.Log("у Палия " + lives);
        if (lives < 1)
            Die();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
        }
        if (lives < 1)
            Die();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right * dir.x * 2.5F, 2.0F);
        if (colliders.All(x => !x.GetComponent<Hero>()))
        {
            if (colliders.Length > 1) dir *= -1.0F;
        }
        else if (colliders.Length > 2)
            dir *= -1.0F;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
}
