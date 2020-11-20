using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour
{

    [BoxGroup, ShowInInspector] private GameObject nickname;
    [ShowInInspector] private Transform userTransfrom;
    [BoxGroup] public int SocketID;
    [BoxGroup] private string nicknameStr;
    private int hp;
    protected Animator animator;
    private Transform mainBody;
    public  Transform nicknameTransform;
    private Vector3 targetPos;
       [ShowInInspector]
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            hpbar.SetHpBar(hp);
        }
    }
    [SerializeField] protected HpBar hpbar;
    public void DestroyThis() {
        Destroy(this);
    }
    public void SetSocketID(int id) {
        SocketID = id;
    }
    public void SetNickname(string str) {
        nicknameStr = str;
    }
    protected void Start()
    {
        nickname = Instantiate(Resources.Load("Prefab/Nickname")) as GameObject;
        nickname.transform.parent = UIManager.Instance().NicknamePanel.transform;
        nickname.GetComponent<Text>().text = nicknameStr;
        userTransfrom = this.gameObject.transform;
        hpbar = transform.GetChild(1).GetComponent<HpBar>();
        Hp = 100;
        hpbar.SetMaxHp(100);
        mainBody = transform.GetChild(0);
        animator = mainBody.GetComponent<Animator>();
        nicknameTransform = transform.GetChild(2);
    }
    #region Transform&Animation
    protected void SetNicknamePosition() {
        nickname.transform.position = Camera.main.WorldToScreenPoint(nicknameTransform.position);
    }
    public void SetPosition(Vector2 vec) {
        if (userTransfrom == null) return;
        targetPos = new Vector3(vec.x, vec.y, -10);
    }
    public void SetHp(int hp) {
        Hp = hp;
    }
    public void SetDir(int dir)
    {
       SetDirection(dir);
    }
    /// <summary>
    /// localscale을 통해 main body만 좌우 반전 시킴.
    /// </summary>
    /// <param name="dir"> 0 == left, 1== right</param>
    protected void SetDirection(int dir) {
        if(dir==-1)
        mainBody.localScale = new Vector3(-1, 1, 1);
        else
        mainBody.localScale = new Vector3(1, 1, 1);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos,5*Time.deltaTime);
        SetNicknamePosition();
    }
    #endregion
    #region State
    public enum State {

        idle,
        run,
        attack,
        hurt,
        die,
    }
    [BoxGroup]public State state;
    public void SetState(int state) {
        switch (state) {
            case ((int)State.idle):
                this.state = State.idle;
                animator.SetBool("run", false);
                break;
            case ((int)State.attack):
                this.state = State.attack;
                animator.SetTrigger("Attack");
                break;
            case ((int)State.run):
                this.state = State.run;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                animator.SetBool("run", true);
                break;
        }
    }
    #endregion
}
