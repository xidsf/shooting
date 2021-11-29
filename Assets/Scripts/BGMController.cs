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
        //현재 재생중인 오디오 정지
        audioSource.Stop();

        //다른 클래스에서 BGM을 설정할 때 정수를 사용한다면 BGM이 몇번인지 확인하려면
        //Inspector View의 BgmClip[]을 확인해야 알 수 있기 때문에 enum을 사용하여
        //가독성을 높인다.

        //index번호의 bgm으로 변경
        audioSource.clip = bgmClip[(int)index];
        //바뀐 bgm재생
        audioSource.Play();
    }
}
