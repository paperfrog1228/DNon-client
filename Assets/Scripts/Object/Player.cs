using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Player -> 조종하는 사람
/// </summary>
public class Player : User
{
    [BoxGroup,SerializeField] protected string type = "Chemical";
    private static Player instance = null;
    [SerializeField,Range(0,30)]
    private float speed=5.0f;
    
    private void Awake() 
    {
        instance = this;
    }
    protected void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
            Run(horizontal, vertical);
        else animator.SetBool("run", false);
        SetNicknamePosition();
     }

    #region Damage
    public void ReceiveDamage(int damage) {
        Hp -= damage;
    }
    #endregion
    #region Animation
    protected void Run(float horizontal,float vertical)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            animator.SetBool("run",true);

        if (horizontal > 0)
            SetDirection(0);
        else
            SetDirection(1);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontal, Space.World);
        transform.Translate(Vector3.up  * Time.deltaTime * speed * vertical, Space.World);
    }
    #endregion
    public static Player Instance()
    {
        return instance;
    }
}
