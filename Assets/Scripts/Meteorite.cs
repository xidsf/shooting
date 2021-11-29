using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;//� ���ݷ�
    [SerializeField]
    private GameObject explosionPrefab;//���� ȿ��������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //� �浹�� HP����
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //� ���� �Լ�
            onDie();
        }
    }
    public void onDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
