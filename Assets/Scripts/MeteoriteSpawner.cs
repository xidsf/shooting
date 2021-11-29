using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject alartLinePrefab;
    [SerializeField]
    private GameObject meteoritePrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine("spawnMeteorite");
    }

    private IEnumerator spawnMeteorite()
    {
        while(true)
        {
            //x내의 임의의 값 생성
            float PositionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //경고선 오브젝트를 생성하고 alaertLineClone에 넣음
            GameObject alartLineClone = Instantiate(alartLinePrefab, new Vector3(PositionX, 0, 0), Quaternion.identity);
            //1초 기다림
            yield return new WaitForSeconds(1.0f);
            //alaerLineClone삭제
            Destroy(alartLineClone);

            //메테오 오브젝트 생성
            Vector3 meteoritePosition = new Vector3(PositionX, stageData.LimitMax.y, 0);
            Instantiate(meteoritePrefab, meteoritePosition, Quaternion.identity);

            //대기시간
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            //대기시간동안 기다림
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
