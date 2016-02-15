using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public GameObject reflectorPrefab;
    public static Hero hero;
    public ParticleSystem DeathParticles;
    public float invulnTime = 0.5f;

    private float speed = 7.5f;
    private Rigidbody body;
    private HealthBar healthBar;
    private StaminaBar staminaBar;
    private int _maxHealth, health;
    private int _maxStamina, stamina;
    private int /*bulletLayer,*/ enemyLayer;
    private bool invulnerable = false;

	private Renderer rend;

    //private float reflectorCooldown = 0.75f;
    private float lastRefl;
    private float lastDamage;
    //private float spawnDist = 1f;
    private int reflCost = 10;

    public bool reflOut = false;
    //private bool staminaFrame = true;
    //private float staminaTick;
    //private float staminaCooldown = 0.1f;

    private Reflector reflector;

    private Camera cam;

    public int maxHealth
    {
        get { return _maxHealth; }
    }

    public int maxStamina
    {
        get { return _maxStamina; }
    }

    void Awake()
    {
        //bulletLayer = LayerMask.NameToLayer("Bullet");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        hero = this;
        health = _maxHealth = 10;
        stamina = _maxStamina = 200;
        reflector = transform.Find("Reflector").GetComponent<Reflector>();

		rend = GetComponent<Renderer>();
    }

    void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        cam = FindObjectOfType<Camera>();
        body = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (lastDamage + invulnTime < Time.time)
        {
            invulnerable = false;    
        }

		if (invulnerable)
		{
			if (rend.material.color == Color.white)
			{
				rend.material.color = Color.red;
			}
			else
			{
				rend.material.color = Color.white;
			}
		}
		else
		{
			rend.material.color = Color.white;
		}

        Move();

        FaceCursor();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            spawnReflector();
            reflOut = true;
        }
        else if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Mouse0))
        {
            reflector.gameObject.SetActive(false);
            reflOut = false;
        }
    }

    void FixedUpdate()
    {
        if (stamina <= 0)
        {
            reflOut = false;
            reflector.gameObject.SetActive(false);
        }

        if (reflOut)
        {
            stamina -= 3;
            staminaBar.Remove(3);
        }

        //staminaTick = Time.time;
        if (stamina < maxStamina)
        {
            stamina += 2;
            staminaBar.Add(2);
        }
    }

    void OnDestroy()
    {
        staminaBar.Remove(stamina);
        stamina = 0;

        if (EnemyBase.NumEnemies() != 0)
        {
            ParticleSystem part = Instantiate(DeathParticles);
            part.transform.position = transform.position;
            part.Play();
            Destroy(part, 20);
	}

        hero = null;

        return;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            takeDamage(1);
        }
    }

//	(\(\
//	(='.')
//	( )  )
//	(_)(_) /\/\/\

    public void takeDamage(int damage)
    {
        if (invulnerable)
            return;

        invulnerable = true;
        lastDamage = Time.time;
        health -= damage;
        healthBar.Remove(damage);
    }

    public void addHealth(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        healthBar.Add(amount);
    }

	public void addSides()
	{
		transform.Find("RightReflector").gameObject.SetActive(true);
		transform.Find("LeftReflector").gameObject.SetActive(true);
		transform.Find("BackReflector").gameObject.SetActive(true);
	}

    public bool Dead()
    {
        return health <= 0;
    }

    private void spawnReflector()
    {
        reflector.initReflector();
        stamina -= reflCost;
        staminaBar.Remove(reflCost);
    }

    private void Move()
    {
        Vector3 vel = body.velocity;

        if (Input.GetKey(KeyCode.W))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            vel.x = speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            vel.x = -speed;
        }
        else
        {
            vel.x = 0;
        }

        body.velocity = vel;
    }

    private void FaceCursor()
    {
        Vector3 temp = new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    (transform.position - cam.transform.position).magnitude);

        Vector3 target = cam.ScreenToWorldPoint(temp);
        target.z = transform.position.z;
        transform.LookAt(target, Vector3.up);

		float angle = Vector3.Angle(Vector3.right, target - transform.position);

		if (target.y < transform.position.y)
		{
			angle *= -1;
		}

		transform.localEulerAngles = new Vector3(0,0,angle);

    }
}
