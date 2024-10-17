using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] int bombDMG;
    [SerializeField] GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
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
            IDamage damage = other.transform.root.GetComponent<IDamage>();
            if (damage != null)
            {
                damage.TakeDamage(bombDMG);
            }
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 5);
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        IDamage dmg = GetComponent<IDamage>();
    //        if (dmg != null)
    //        {
    //            dmg.TakeDamage(bombDMG);
    //        }
    //    }
    //}
}
