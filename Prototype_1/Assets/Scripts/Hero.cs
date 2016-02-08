using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public GameObject reflectorPrefab;
    public static Hero hero;


    private float speed = 5f;
    private Rigidbody body;
    private HealthBar healthBar;
    private StaminaBar staminaBar;
    private int _maxHealth, health, bulletLayer, _maxStamina, stamina;

    private float reflectorCooldown = 0.75f;
    private float lastRefl;
    private float spawnDist = 1f;
	private int reflCost = 20;

	private bool staminaFrame = true;
	private float staminaTick;
	private float staminaCooldown = 0.1f;

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
        bulletLayer = LayerMask.NameToLayer("Bullet");
        hero = this;
        health = _maxHealth = 10;
		stamina = _maxStamina = 100;
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
        Move();

        FaceCursor();

        if (Input.GetKey(KeyCode.Space) && Time.time - lastRefl > reflectorCooldown)
        {
			if (stamina >= reflCost)
				spawnReflector();
        }

    }

	void FixedUpdate()
	{
		if (Time.time - staminaTick < staminaCooldown)
		{
			return;
		}

		staminaTick = Time.time;
		if (stamina < maxStamina)
		{
			stamina += 1;
			staminaBar.Add(1);
		}
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == bulletLayer)
        {
            health -= 1;    // TODO Make a better way to determine the amount of damage
            healthBar.Remove(1);  // Maybe a static class functions?
        }
    }

    private void spawnReflector()
    {
        lastRefl = Time.time;
        GameObject refl = Instantiate<GameObject>(reflectorPrefab);
        Vector3 pos = transform.position + transform.right * spawnDist;
        refl.transform.position = pos;
        refl.transform.rotation = transform.rotation;

		stamina -= 20;
		staminaBar.Remove(20);
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
        transform.LookAt(target, transform.up);
		transform.rotation *= Quaternion.Euler(new Vector3(0, -90, 0));
    }
}
