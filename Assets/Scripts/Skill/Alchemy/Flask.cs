using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 화학공학과가 던지는 투척무기
/// 던진 자신도 밟으면 데미지를 입기 때문에
/// 별도의 예외처리를 할 필요는 없습니다.
/// </summary>
public class Flask : MonoBehaviour
{
	[ShowInInspector]private GameObject bottle;
	[ShowInInspector]private GameObject field;
	public Vector3 targetPos;
	[Range(0,10),SerializeField] private float fieldHoldTime;
	
	private void Start()
    {
		bottle = transform.GetChild(0).gameObject;
		bottle.GetComponent<Bottle>().targetPos = targetPos;
			field = transform.GetChild(1).gameObject;
    }


	public void CompleteThrow() { 
		field.transform.position = bottle.transform.position;
		bottle.SetActive(false);
		field.SetActive(true);
		StartCoroutine(HoldToxicField(fieldHoldTime));
	}
	IEnumerator HoldToxicField(float time) {
		yield return new WaitForSeconds(time);
		Destroy(this.gameObject);
	}
}
