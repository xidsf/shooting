using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;//운석 공격력
    [SerializeField]
    private GameObject explosionPrefab;//폭발 효과프리팹

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //운석 충돌시 HP감소
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //운석 제거 함수
            onDie();
        }
    }
    public void onDie()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
