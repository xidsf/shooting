using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossExplosion : MonoBehaviour
{
    private PlayerController playerController;
    private string sceneName;

    public void Setup(PlayerController playerController, string sceneName)
    {
        this.playerController = playerController;
        this.sceneName = sceneName;
    }

    //ParticleAutoDestroy 컴포넌트에서 파티클 재생이 완료되면 파티클을 삭제해야 하기 때문에 
    //오브젝트가 삭제될 때 호출되는 onDestroy함수를 이용하여 파티클 재생이
    //완료되었을 때 필요한 처리를 설정한다.

    private void OnDestroy()
    {
        //보스 처치 + 10000
        playerController.Score += 10000;
        //플레이어 획득 점수를 "Score"키에 저장
        PlayerPrefs.SetInt("Score", playerController.Score);
        SceneManager.LoadScene(sceneName);
    }

}
