using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AudioClip boomAudio; //사운드 파일
    [SerializeField]
    private int damage = 100;
    private float boomDelay = 0.5f;//복탄 이동 시간(0.5초 후 폭발)
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

            //boomDelay에 설정된 시간동안 startPosition부터 endPosition까지 이동
            //curve에 설정된 그래프처럼 처음엔 빠르게 이동하고 목적지에 다다를 때는 천천히 이동
            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }
        //이동한 후 에니메이션 변경
        animator.SetTrigger("onBoom");
        //사운드 변경
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
