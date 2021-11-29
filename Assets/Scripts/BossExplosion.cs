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

    //ParticleAutoDestroy ������Ʈ���� ��ƼŬ ����� �Ϸ�Ǹ� ��ƼŬ�� �����ؾ� �ϱ� ������ 
    //������Ʈ�� ������ �� ȣ��Ǵ� onDestroy�Լ��� �̿��Ͽ� ��ƼŬ �����
    //�Ϸ�Ǿ��� �� �ʿ��� ó���� �����Ѵ�.

    private void OnDestroy()
    {
        //���� óġ + 10000
        playerController.Score += 10000;
        //�÷��̾� ȹ�� ������ "Score"Ű�� ����
        PlayerPrefs.SetInt("Score", playerController.Score);
        SceneManager.LoadScene(sceneName);
    }

}
