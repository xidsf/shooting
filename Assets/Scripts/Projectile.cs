using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //적 제거
            collision.GetComponent<EnemyHP>().TakeDamage(damage);
            //총알 제거
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Boss"))
        {
            //적 제거
            collision.GetComponent<BossHP>().takeDamage(damage);
            //총알 제거
            Destroy(gameObject);
        }
    }
}
