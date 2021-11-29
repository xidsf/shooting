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
        float attackRate = 0.5f; //���� �ֱ�
        int count = 30; //�߻�ü ����
        int intervalAngle = 360 / count; //�߻�ü ������ ����
        float weightangle = 0; // ���ߵǴ� ����(�׻�  ���� ��ġ�� �߻����� �ʵ���)

        while(true)
        {
            for (int i = 0; i < count; i++)
            {
                //�߻�ü ����
                GameObject clone = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

                //�߻�ü �̵� ����
                float angle = weightangle + intervalAngle * i;

                //�߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); //cos(����) , ���Ȱ��� ���ϱ� ���� Mathf.PI / 180.0f�� ���
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f); //cos(����) , ���Ȱ��� ���ϱ� ���� Mathf.PI / 180.0f�� ���

                clone.GetComponent<Movement2D>().MoveTo(new Vector3(x, y, 0));
            }
            weightangle += 1; //�߻�ü�� �����Ǳ� �����ϴ� ������ ����

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator SingleFireToCenterPosition()
    {
        Vector3 targetPosition = Vector3.zero;
        float attakeRate = 0.1f;

        while(true)
        {
            //boss projectile����
            GameObject clone = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            //���� ����
            Vector3 direction = (targetPosition - clone.transform.position).normalized;
            //�������� �̵�
            clone.GetComponent<Movement2D>().MoveTo(direction);
            //attakerate
            yield return new WaitForSeconds(attakeRate);
        }

    }
}
