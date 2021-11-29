using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int scorePoint = 100;
    private PlayerController playerController; //�÷��̾��� ����(score)���� ������ ����
    [SerializeField]
    private GameObject explosionPrefab; //���� ȿ��
    [SerializeField]
    private GameObject[] itemPrefabs;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//�׳� �巡�׾� ������� ������ �ȵǴ°ǰ�?
        

        //���� �ڵ忡���� �ѹ��� ȣ���ϱ� ������ onDie()���� �ٷ� ȣ���ص� ������
        //������Ʈ Ǯ��(?)�� �̿��� ������Ʈ�� ������ ��쿡�� ���� 1���� Find�� �̿���
        //PlayerController�� ������ �����صΰ� ����ϴ� ���� ���꿡 ȿ�����̴�.

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //�� ���ݷ¸�ŭ ������
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //�� ���
            Destroy(gameObject);
            onDie();
        }
    }
    public void onDie()
    {
        playerController.Score += scorePoint;//���ھ� ���
        SpawnItem();

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);//���� �̺�Ʈ ����
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        //10%Ȯ���� �Ŀ�, 5%Ȯ���� ��ź, 5%Ȯ���� ü��
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
