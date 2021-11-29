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
            //x���� ������ �� ����
            float PositionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //��� ������Ʈ�� �����ϰ� alaertLineClone�� ����
            GameObject alartLineClone = Instantiate(alartLinePrefab, new Vector3(PositionX, 0, 0), Quaternion.identity);
            //1�� ��ٸ�
            yield return new WaitForSeconds(1.0f);
            //alaerLineClone����
            Destroy(alartLineClone);

            //���׿� ������Ʈ ����
            Vector3 meteoritePosition = new Vector3(PositionX, stageData.LimitMax.y, 0);
            Instantiate(meteoritePrefab, meteoritePosition, Quaternion.identity);

            //���ð�
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            //���ð����� ��ٸ�
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
