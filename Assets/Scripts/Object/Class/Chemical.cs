using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemical : Player
{
    private void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(0)) {
            animator.SetTrigger("attack");
            Attack(Input.mousePosition);
        }

    }
    private void Attack(Vector3 mousePos) {
        Vector3 screenMousePos=Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0, 0, 10));
        if(screenMousePos.x>transform.position.x) 
            transform.localScale = new Vector3(-1, 1, 1);
        else 
            transform.localScale = new Vector3(1, 1, 1);
        var flask = Instantiate(Resources.Load("Prefab/flask")) as GameObject;
        flask.transform.position = gameObject.transform.position;
        flask.GetComponent<Flask>().targetPos = screenMousePos;
            flask.SetActive(true);
    }
}
