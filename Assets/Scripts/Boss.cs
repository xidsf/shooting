using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    MoveToAppearPoint = 0,
    Phase01, 
    Phase02, 
    Phase03
}

public class Boss : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private string nextSceneName;

    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;
    private BossWeapon bossWeapon;
    private BossHP bossHP;

    // Start is called before the first frame update
    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        bossWeapon = GetComponent<BossWeapon>();
        bossHP = GetComponent<BossHP>();
    }

    public void ChangeState(BossState newState)
    {
        //�������� .Tostring�� �ϰԵǸ� �������� string���·� �޾ƿ��� �ȴ�.
        //ex) BossState.MoveToAppearPoint �̸� "MoveToAppearPoint"

        //�̸� �̿��� �������� �̸��� �ڷ�ƾ�� �̸��� ��ġ���Ѽ�
        //������ ������ ���� �ڷ�ƾ �Լ��� �����ų �� �ִ�.

        //���� ���� ����
        StopCoroutine(bossState.ToString());

        //���ο� ���� ����
        bossState = newState;

        //���ο� ���� ���
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        //�̵����� ����(�ڷ�ƾ ����� �ѹ��� ȣ��)
        movement2D.MoveTo(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPoint)
            {
                //�̵������� 0, 0, 0����  ������ �������� ���ƿ����� ����
                movement2D.MoveTo(Vector3.zero);
                ChangeState(BossState.Phase01);
            }

            yield return null;

        }
    }

    private IEnumerator Phase01()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);
        while(true)
        {
            if(bossHP.CurrentHP <= bossHP.MaxHP * 0.7)
            {
                bossWeapon.StopFiring(AttackType.CircleFire);

                ChangeState(BossState.Phase02);
            }

            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while(true)
        {
            if(transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }

            if (bossHP.CurrentHP <= bossHP.MaxHP * 0.3f)
            {
                bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);

                ChangeState(BossState.Phase03);
            }

            yield return null;
        }
        
    }

    private IEnumerator Phase03()
    {
        bossWeapon.StartFiring(AttackType.CircleFire);
        bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

        Vector3 direction = Vector3.right;
        movement2D.MoveTo(direction);

        while (true)
        {
            if (transform.position.x <= stageData.LimitMin.x || transform.position.x >= stageData.LimitMax.x)
            {
                direction *= -1;
                movement2D.MoveTo(direction);
            }


            yield return null;
        }
    }

    public void onDie()
    {
        GameObject clone = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        clone.GetComponent<BossExplosion>().Setup(playerController, nextSceneName); 
        Destroy(gameObject);
    }
}
