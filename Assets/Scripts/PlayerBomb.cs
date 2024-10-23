using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] int bombDMG;
    [SerializeField] GameObject explosionPrefab;
    public Transform target;
    Rigidbody rb;
    [SerializeField] float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void FixedUpdate()
    {
        Vector3 dir = target.position - rb.position;
        rb.velocity = dir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        print("Bomb hit " + other.gameObject.name);

        if (other.CompareTag("Enemy"))
        {
            IDamage damage = other.transform.GetComponent<IDamage>();
            if (damage != null)
            {
                damage.TakeDamage(bombDMG);
            }
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Bomb hit " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            IDamage dmg = other.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.TakeDamage(bombDMG);
            }

        }
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 5);
        Destroy(gameObject);
    }
}
