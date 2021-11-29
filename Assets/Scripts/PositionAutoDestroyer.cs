using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionAutoDestroyer : MonoBehaviour
{
    [SerializeField]
    private StageData stagedata;
    private float destroyWeight = 2.0f;

    private void LateUpdate()
    {
        if(transform.position.y < stagedata.LimitMin.y - destroyWeight ||
           transform.position.y > stagedata.LimitMax.y + destroyWeight ||
           transform.position.x < stagedata.LimitMin.x - destroyWeight ||
           transform.position.x > stagedata.LimitMax.x + destroyWeight)
        {
            Destroy(gameObject);
        }
    }
}
