using UnityEngine;
using System.Collections;

public class Smoker : Monster
{
    [SerializeField] private float rate = 2.0F;
    [SerializeField] private Color bulletColor = Color.red;
    [SerializeField] private int lives = 3;

    private Bullet bullet;

    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }
    public override void GetDamage()
    {
        lives--;
        if (lives < 1)
            Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("у Смокера " + lives);
        }
        if (lives < 1)
            Die();
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.35F; position.x -= 0.92F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
        newBullet.Color = bulletColor;
    }
}