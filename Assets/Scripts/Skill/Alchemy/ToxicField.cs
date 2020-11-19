using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 바닥에 깔려있는 독성 물질입니다.
/// </summary>
public class ToxicField : MonoBehaviour
{
    [Range(0,10),SerializeField] private int damage;
    [Range(0, 5), SerializeField] private float triggerTime;
    private float startTime;
    private bool trigger = true;
    private bool enter = false;
    Player player;
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") return;
        enter = true;
        player = other.gameObject.GetComponent<Player>();
        StartCoroutine(DamageCoroutine());
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        enter = false;
    }
    IEnumerator DamageCoroutine() {
        while (enter)
        {
            player.ReceiveDamage(damage);
            yield return new WaitForSeconds(triggerTime);
        }
    }
}
