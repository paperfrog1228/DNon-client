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
    private static Player instance = null;
    [SerializeField,Range(0,30)]
    private float speed=5.0f;
    private int hp;
    public Text hpText;
    [ShowInInspector]public int Hp { get { return hp; } set { hp = value;
            hpText.text = "Hp : " + hp;
        } }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        base.Start();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontal, Space.World);
        transform.Translate(Vector3.up  * Time.deltaTime * speed * vertical, Space.World);
        if (Input.GetMouseButtonUp(0)) {
            Attack(Input.mousePosition);
        }
        SetNicknamePosition();
    }
    private void Attack(Vector3 mousePos) {
        var flask = Instantiate(Resources.Load("Prefab/flask")) as GameObject;
        flask.transform.position = gameObject.transform.position;
        flask.GetComponent<Flask>().targetPos=Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0, 0, 10));
        flask.SetActive(true);
    }
    #region Damage
    public void ReceiveDamage(int damage) {
        Hp -= damage;
        Debug.Log(Time.time);
    }
    #endregion
    public static Player Instance()
    {
        return instance;
    }
}
