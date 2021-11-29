using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData; //�� ������
    [SerializeField]
    private GameObject enemyPrefab; //�� ������ ���� GameObject
    [SerializeField]
    private float spawnTime; //���� �ֱ�
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private int MaxEnemyCount = 100;
    [SerializeField]
    private BGMController bgmController;
    [SerializeField]
    private GameObject textBossWarning;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject panelBossHP;

    // Start is called before the first frame update
    private void Awake()
    {
        textBossWarning.SetActive(false); //�� �۾��� inspector view���� �ϸ� �ۼ����� �ʾƵ� ��
        StartCoroutine("SpawnEnemy");
        boss.SetActive(false); //���� ������Ʈ ��Ȱ��ȭ
        panelBossHP.SetActive(false);

    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0; //�� ���� ���� ī��Ʈ�� ����
        while(true)
        {
            //x������ ��ġ ����
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //�� ���� ��ġ
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            //�� ����
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            SpawnEnemySlider(enemyClone);
            //���� �ֱ�

            currentEnemyCount++;
            if(currentEnemyCount == MaxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
    private void SpawnEnemySlider(GameObject enemy)
    {
        //slider����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI������Ʈ�� parent(Canvas������Ʈ)�� �ڽ����� ����
        //***UI�� ĵ������ �ڽĿ�����Ʈ�� �����Ǿ� �־�� ȭ�鿡 ���δ�.***
        sliderClone.transform.SetParent(canvasTransform);

        //������������ �ٲ� ũ�⸦ �ٽ� (1, 1, 1)�� ����
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI�� �i�ƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);

        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss); //bgmController.ChangeBGM(1);���� ������ ���� 
        
        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);  

        textBossWarning.SetActive(false);

        boss.SetActive(true);
        panelBossHP.SetActive(true);
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
