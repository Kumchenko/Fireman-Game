using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= 5) lives = value;
            livesbar.Refresh();
        }
    }

    private LivesBar livesbar;


    [SerializeField] private float jumpForce = 1.5f;
    [SerializeField] private Color bulletColor = Color.blue;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isGrounded = false;
    private Bullet bullet;
    private Vector3 dir;

    private int progress = 0;
    public int Progress
    {
        get { return progress; }
        set
        {
            if (value > 0) progress = value;
        }
    }
    [SerializeField] public Text progressText;
    public static Hero Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Progress);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButton("Jump"))
        {
            Jump();
            State = States.jump;
        }
        if (!isGrounded && Input.GetButtonUp("Jump"))
            Fall();
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        livesbar = FindObjectOfType<LivesBar>();
    }

    private void Run()
    {
        if (isGrounded) State = States.run;
        dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Fall()
    {
        State = States.fall;
    }


    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 1.0F;
        if (dir.x > 0)
            position.x += 1.3F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        newBullet.Color = bulletColor;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            GetDamage();
        }
    }

    public override void GetDamage()
    {
        --Lives;
        Debug.Log(Lives);
        if (Lives < 1)
            Die();
    } 

    public void AddProgress()
    {
        ++Progress;
        progressText.text = "Progress: " + Progress.ToString() + "/12";
        Debug.Log("Прогресс " + Progress);
    }
}

public enum States
{
    idle,
    run,
    jump,
    fall
}
