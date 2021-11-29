using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData; //맵 데이터
    [SerializeField]
    private GameObject enemyPrefab; //적 프리팹 넣을 GameObject
    [SerializeField]
    private float spawnTime; //생성 주기
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
        textBossWarning.SetActive(false); //이 작업은 inspector view에서 하면 작성하지 않아도 됨
        StartCoroutine("SpawnEnemy");
        boss.SetActive(false); //보스 오브젝트 비활성화
        panelBossHP.SetActive(false);

    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0; //적 생성 숫자 카운트용 변수
        while(true)
        {
            //x랜덤의 위치 지정
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //적 생성 위치
            Vector3 position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            //적 생성
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            SpawnEnemySlider(enemyClone);
            //생성 주기

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
        //slider생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI오브젝트를 parent(Canvas오브젝트)의 자식으로 설정
        //***UI는 캔버스의 자식오브젝트로 설정되어 있어야 화면에 보인다.***
        sliderClone.transform.SetParent(canvasTransform);

        //계층설정으로 바뀐 크기를 다시 (1, 1, 1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI가 쫒아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);

        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    private IEnumerator SpawnBoss()
    {
        bgmController.ChangeBGM(BGMType.Boss); //bgmController.ChangeBGM(1);보단 가독성 좋음 
        
        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);  

        textBossWarning.SetActive(false);

        boss.SetActive(true);
        panelBossHP.SetActive(true);
        boss.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }
}
