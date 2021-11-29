using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int scorePoint = 100;
    private PlayerController playerController; //플레이어의 점수(score)정보 접근을 위해
    [SerializeField]
    private GameObject explosionPrefab; //폭발 효과
    [SerializeField]
    private GameObject[] itemPrefabs;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//그냥 드래그앤 드롭으로 넣으면 안되는건가?
        

        //현재 코드에서는 한번만 호출하기 때문에 onDie()에서 바로 호출해도 되지만
        //오브젝트 풀링(?)을 이용해 오브젝트를 재사용할 경우에는 최초 1번만 Find를 이용해
        //PlayerController의 정보를 저장해두고 사용하는 것이 연산에 효율적이다.

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //적 공격력만큼 데미지
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //적 사망
            Destroy(gameObject);
            onDie();
        }
    }
    public void onDie()
    {
        playerController.Score += scorePoint;//스코어 상승
        SpawnItem();

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);//폭발 이벤트 생성
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        //10%확률로 파워, 5%확률로 폭탄, 5%확률로 체력
        int spawnItem = Random.Range(1, 100);
        if (spawnItem <= 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
        else if(spawnItem <= 15)
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        }
        else if(spawnItem <= 20)
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }
    }
}
