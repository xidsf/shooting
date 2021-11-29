using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType
{
    Stage = 0,
    Boss
}

public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBGM(BGMType index)
    {
        //���� ������� ����� ����
        audioSource.Stop();

        //�ٸ� Ŭ�������� BGM�� ������ �� ������ ����Ѵٸ� BGM�� ������� Ȯ���Ϸ���
        //Inspector View�� BgmClip[]�� Ȯ���ؾ� �� �� �ֱ� ������ enum�� ����Ͽ�
        //�������� ���δ�.

        //index��ȣ�� bgm���� ����
        audioSource.clip = bgmClip[(int)index];
        //�ٲ� bgm���
        audioSource.Play();
    }
}
