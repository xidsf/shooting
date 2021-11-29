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
        //���� �ı��Ǿ� �i�ƴٴ� ����� �������� Slider UI�� ����
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //������Ʈ�� ��ġ�� ���ŵ� ���Ŀ� SliderUI�� �Բ� ��ġ�� �����ϵ��� �ϱ�����
        //LateUpdate()���� ȣ����

        //������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ���� ����
        //���� ������Ʈ�� ���ӳ��� 3���� ��ǥ�� ���������
        //UI�� ī�޶󿡼� ���̴� 2���� ��ǥ�� ����ϱ� ������ �̷��� ����Ѵ�.
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        //ȭ�� �������� ��ǥ+distnace��ŭ ������ ��ġ�� Slider UI�� ��ġ�� ����
        rectTransform.position = screenPosition + distance;

    }
}
