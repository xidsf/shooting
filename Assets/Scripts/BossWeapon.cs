using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    CircleFire = 0,
    SingleFireToCenterPosition,
}

public class BossWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject ProjectilePrefab;

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f; //공격 주기
        int count = 30; //발사체 갯수
        int intervalAngle = 360 / count; //발사체 사이의 각도
        float weightangle = 0; // 가중되는 각도(항상  같은 위치로 발사하지 않도록)

        while(true)
        {
            for (int i = 0; i < count; i++)
            {
                //발사체 생성
                GameObject clone = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

                //발사체 이동 각도
                float angle = weightangle + intervalAngle * i;

                //발사체 이동 방향(벡터)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); //cos(각도) , 라디안값을 구하기 위해 Mathf.PI / 180.0f를 사용
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f); //cos(각도) , 라디안값을 구하기 위해 Mathf.PI / 180.0f를 사용

                clone.GetComponent<Movement2D>().MoveTo(new Vector3(x, y, 0));
            }
            weightangle += 1; //발사체가 생성되기 시작하는 각도를 변경

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attakeRate = 0.1f;

        while(true)
        {
            //boss projectile생성
            GameObject clone = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            //방향 결정
            Vector3 direction = (targetPosition - clone.transform.position).normalized;
            //방향으로 이동
            clone.GetComponent<Movement2D>().MoveTo(direction);
            //attakerate
            yield return new WaitForSeconds(attakeRate);
        }

    }
}
