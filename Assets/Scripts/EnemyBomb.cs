using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] int bombDMG;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip explosionSFX;
    AudioSource aud;
    // Start is called before the first frame update
    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

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
        aud.PlayOneShot(explosionSFX);
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
