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
        //����Ű�� �̿��� �̵�
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
        //�÷��̾� �̵� ����
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
            
    }

    public void onDie()
    {
        //�̵�Ű, ���� �ʱ�ȭ
        movement2D.MoveTo(Vector3.zero);
        //��� ���ϸ��̼� ���
        animator.SetTrigger("onDie");
        //����� �浹���� �ʵ��� �浹�ڽ� ����
        Destroy(GetComponent<CircleCollider2D>());
        //isDie true
        isDie = true;

        
    }
    public void onDieEvent()
    {
        PlayerPrefs.SetInt("Score", score);
        //�� �̵�
        SceneManager.LoadScene(nextSceneName);
    }
}
