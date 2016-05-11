using UnityEngine;
using System.Collections;

public class OtherBallControll : MonoBehaviour {

	//コンポーメント
	Rigidbody rb;

	float timer;

	bool moveEndFlg;
	bool moveFlg;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.FreezePositionY;	//Y軸固定

		moveEndFlg = false;
		moveFlg = false;

		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (moveFlg) {
			//移動したなら

			if (timer <= 1) {
				timer += Time.deltaTime;
			} else {
				//xとzの移動量が一定値以下ならば
				if ((-0.2f <= rb.velocity.x && rb.velocity.magnitude <= 0.2f) && (-0.2f <= rb.velocity.z && rb.velocity.z <= 0.2f)) {
					rb.velocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
					timer = 0;
					moveFlg = false;
					moveEndFlg = true;
				}
			}
		}
	}


	void OnCollisionEnter(Collision col){
		//穴に入ったら
		if (col.gameObject.tag == "Holl") {
			//処理を止めて
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			//ボールを消す
			Destroy (this.gameObject);
		}

		if (col.gameObject.tag == "MainBall" || col.gameObject.tag == "Ball") {
			
			moveEndFlg = false;
			moveFlg = true;
		}
	}

	//移動終了
	public bool GetMoveEnd(){
		return moveEndFlg;
	}

	//移動終了
	public bool GetMoveFlg(){
		return moveFlg;
	}
}
