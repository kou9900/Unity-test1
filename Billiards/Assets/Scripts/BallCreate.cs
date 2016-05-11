using UnityEngine;
using System.Collections;

public class BallCreate : MonoBehaviour {

	//オブジェクト
	public GameObject[] ballprefab;

	// Use this for initialization
	void Start () {
		
		float y = 0.6f;

		//ボール位置
		Vector3[] ball_pos = new Vector3[]{
			new Vector3(0f,y,0f),
			new Vector3(-1.2f + Mathf.Sin(Mathf.PI/180*30),y,Mathf.Cos(Mathf.PI/180*30)+ 0.4f),
			new Vector3(1.2f + -Mathf.Sin(Mathf.PI/180*30), y,  Mathf.Cos(Mathf.PI/180*30) + 0.4f),
			new Vector3 (-2.4f + Mathf.Sin(Mathf.PI/180*30)*2, y,  Mathf.Cos(Mathf.PI/180*30)*2 + 0.8f),
			new Vector3 (0f , y, Mathf.Cos(Mathf.PI/180*30)*2 + 0.8f),
			new Vector3 (2.4f + -Mathf.Sin(Mathf.PI/180*30)*2, y, Mathf.Cos(Mathf.PI/180*30)*2 + 0.8f),
			new Vector3 (-1.2f + Mathf.Sin(Mathf.PI/180*30), y,  Mathf.Cos(Mathf.PI/180*30)*3 + 1.2f),
			new Vector3 (1.2f + -Mathf.Sin(Mathf.PI/180*30), y,  Mathf.Cos(Mathf.PI/180*30)*3 + 1.2f),
			new Vector3 (0f, y, Mathf.Cos(Mathf.PI/180*30)*4  + 1.6f)
		};

		//ボール生成
		for (int i = 0; i < ball_pos.Length; i++) {
			GameObject ball = (GameObject)Instantiate (ballprefab[i], ball_pos[i], Quaternion.identity);
			ball.transform.parent = this.transform;
		}

	}
}
