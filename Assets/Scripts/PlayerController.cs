using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.Z;
    private bool isDie;
    private Weapon weapon;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }
    private Movement2D movement2D;
    private Animator animator;


    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) return;
        //방향키를 이용해 이동
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement2D.MoveTo(new Vector3(x, y, 0));
        

        
        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.startFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }
        if(Input.GetKeyDown(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    private void LateUpdate()
    {
        //플레이어 이동 제한
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
            
    }

    public void onDie()
    {
        //이동키, 방향 초기화
        movement2D.MoveTo(Vector3.zero);
        //사망 에니메이션 재생
        animator.SetTrigger("onDie");
        //적들과 충돌하지 않도록 충돌박스 삭제
        Destroy(GetComponent<CircleCollider2D>());
        //isDie true
        isDie = true;

        
    }
    public void onDieEvent()
    {
        PlayerPrefs.SetInt("Score", score);
        //신 이동
        SceneManager.LoadScene(nextSceneName);
    }
}
