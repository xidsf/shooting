using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //�ε��� ������Ʈ ü�� ����(�÷��̾�)
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //������Ʈ ����
            onDie();
        }
    }
    public void onDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
