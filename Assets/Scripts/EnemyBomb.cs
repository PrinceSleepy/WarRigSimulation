using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] int bombDMG;
    [SerializeField] GameObject explosionPrefab;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        //print("Bomb hit " + other.gameObject.name);

        if (other.CompareTag("Truck"))
        {
            IDamage damage = other.transform.root.GetComponent<IDamage>();
            if (damage != null)
            {
                damage.TakeDamage(bombDMG);
            }
        }
        else if (other.CompareTag("Trailer"))
        {
            IDamage damage = other.GetComponent<IDamage>();
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
    //    print("Bomb hit " + other.gameObject.name);

    //    if (other.CompareTag("Truck"))
    //    {
    //        IDamage damage = other.transform.root.GetComponent<IDamage>();
    //        if (damage != null)
    //        {
    //            damage.TakeDamage(bombDMG);
    //        }
    //    }
    //    else if (other.CompareTag("Trailer"))
    //    {
    //        IDamage damage = other.GetComponent<IDamage>();
    //        if (damage != null)
    //        {
    //            damage.TakeDamage(bombDMG);
    //        }
    //    }
    //    if (!other.CompareTag("Player"))
    //    {
    //        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
    //        Destroy(explosion, 5);
    //        Destroy(gameObject);
    //    }
    //}
}
