using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 35.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //적이 파괴되어 쫒아다닐 대상이 사하지면 Slider UI도 삭제
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //오브젝트의 위치가 갱신된 이후에 SliderUI도 함께 위치를 설정하도록 하기위해
        //LateUpdate()에서 호출함

        //오브젝트위 월드 좌표를 기준으로 화면에서의 좌표값을 구함
        //게임 오브젝트는 게임내의 3차원 좌표를 사용하지만
        //UI는 카메라에서 보이는 2차원 좌표를 사용하기 때문에 이렇게 써야한다.
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        //화면 내에서의 좌표+distnace만큼 떨어진 위치를 Slider UI의 위치로 설정
        rectTransform.position = screenPosition + distance;

    }
}
