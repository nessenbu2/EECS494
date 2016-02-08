using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public GameObject reflectorPrefab;
    public static Hero hero;

    private float speed = 5f;
    private Rigidbody body;
    private HealthBar bar;
    private int maxHealth, health, bulletLayer;

    private float reflectorCooldown = 0.75f;
    private float lastRefl;
    private float spawnDist = 1f;

    private Camera cam;

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    void Awake()
    {
        bulletLayer = LayerMask.NameToLayer("Bullet");
    }

    void Start()
    {
        bar = FindObjectOfType<HealthBar>();
        cam = FindObjectOfType<Camera>();
        body = GetComponent<Rigidbody>();
        hero = this;
        health = maxHealth = 10;
    }
    
    void Update()
    {
        Move();

        FaceCursor();

        if (Input.GetKey(KeyCode.Space) && Time.time - lastRefl > reflectorCooldown)
        {
            spawnReflector();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == bulletLayer)
        {
            health -= 1;    // TODO Make a better way to determine the amount of damage
            bar.Remove(1);  // Maybe a static class functions?
        }
    }

    private void spawnReflector()
    {
        lastRefl = Time.time;
        GameObject refl = Instantiate<GameObject>(reflectorPrefab);
        Vector3 pos = transform.position + transform.right * spawnDist;
        refl.transform.position = pos;
        refl.transform.rotation = transform.rotation;
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
        transform.LookAt(target);
    }
}
