using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip boomAudio; //���� ����
    [SerializeField]
    private int damage = 100;
    private float boomDelay = 0.5f;//��ź �̵� �ð�(0.5�� �� ����)
    private Animator animator;
    private AudioSource audioSource;
    

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;

            //boomDelay�� ������ �ð����� startPosition���� endPosition���� �̵�
            //curve�� ������ �׷���ó�� ó���� ������ �̵��ϰ� �������� �ٴٸ� ���� õõ�� �̵�
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }
        //�̵��� �� ���ϸ��̼� ����
        animator.SetTrigger("onBoom");
        //���� ����
        audioSource.clip = boomAudio;
        audioSource.Play();
    }

    public void onBoom()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] Meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
        GameObject[] EnemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<Enemy>().onDie();
        }
        for (int i = 0; i < Meteorites.Length; i++)
        {
            Meteorites[i].GetComponent<Meteorite>().onDie();
        }
        for (int i = 0; i < EnemyProjectiles.Length; i++)
        {
            EnemyProjectiles[i].GetComponent<EnemyProjectile>().onDie();
        }
        Destroy(gameObject);

        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if(boss != null)
        {
            boss.GetComponent<BossHP>().takeDamage(damage);
        }

    }
}
