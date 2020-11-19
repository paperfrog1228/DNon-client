using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플라스크 오브젝트이 병입니다.
/// 날라가는 동작을 표현합니다.
/// </summary>
public class Bottle : MonoBehaviour
{
	[Range(1,30),SerializeField]private float journeyTime = 1.0F;
	[Range(0,10),SerializeField]private float reduceHeight = 1f;
	private float startTime;
	public Vector3 targetPos;
	Flask flask;
    void Start()
    {
		flask = transform.parent.gameObject.GetComponent<Flask>();
		startTime = Time.time;
    }
    void Update()
    {
		Vector3 center = (transform.position + targetPos) * 0.5F; 
		center -= new Vector3(0, 1f * reduceHeight, 0);
		Vector3 riseRelCenter = transform.position - center;
		Vector3 setRelCenter = targetPos - center;
		float fracComplete = (Time.time - startTime) / journeyTime;
		transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
		transform.position += center;

		if (transform.position == targetPos)
			flask.CompleteThrow();
    }
}
