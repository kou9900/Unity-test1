using UnityEngine;
using System.Collections;

public class MainBollControll : MonoBehaviour {

	//オブジェクト
	public GameObject QueObj;
	public QueControl QueCont;

	//コーポーメント
	Rigidbody rb;
	MeshRenderer mr;
	SphereCollider sc;

	float timer;

	bool moveEndFlg;
	bool moveFlg;
	public bool hitflg;

	//位置関連
	Vector3 postion;		//移動前の位置
	Vector3 postion2;		//移動後の位置
	Vector2 mouse;			//マウス位置
	Vector3 first_pos;		//初期位置

	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		mr = this.GetComponent<MeshRenderer> ();
		sc = this.GetComponent<SphereCollider> ();

		moveEndFlg = false;
		moveFlg = false;
		hitflg = false;

		timer = 0;
		first_pos = this.transform.position;
	}

	void Update () {
		if (!moveFlg) { 
			rb.Sleep ();
		} else {
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

	void FixedUpdate(){
		//	ショットされたならば
		if (QueCont.GetShotFlg ()) {
			rb.constraints = RigidbodyConstraints.FreezePositionY;	//Y軸固定
			//球を動かす
			Vector3 vec = QueObj.transform.up * 10;
			vec.y = 0;
			vec *= QueCont.GetShotPower () * -1;
			rb.AddForce (vec);

			//ショットフラグをfalseに
			QueCont.SetShotFlg (false);
		}

	}
		
	//移動開始
	public void MoveStart(){
		moveEndFlg = false;
		moveFlg = true;
	}

	//移動終了
	public bool GetMoveEnd(){
		return moveEndFlg;
	}

	//ファウル時の初期設定
	public void FaulStart(){
		this.transform.position = first_pos;
		postion2 = postion = transform.position;
		mouse = new Vector3 (Screen.width / 2, Screen.height / 2, 0f);
		mr.enabled = true;
		hitflg = false;
		sc.isTrigger = true;
	}

	//ファウル中の移動
	public bool FaulMove(){
		bool flg = false;

		postion2.x = postion.x + -(mouse.x - Input.mousePosition.x) / 10;
		postion2.z = postion.z + -(mouse.y - Input.mousePosition.y) / 10;

		if (postion2.x > 9.5) {
			postion2.x = 9.5f;
		}

		if (postion2.x < -9.5) {
			postion2.x = -9.5f;
		}

		if (postion2.z > 19f) {
			postion2.z = 19f;
		}

		if (postion2.z < -19f) {
			postion2.z = -19f;
		}

		this.transform.position = postion2;
		postion = transform.position;

		mouse.x = Input.mousePosition.x;
		mouse.y = Input.mousePosition.y;

		if (Input.GetMouseButtonDown (0) && !hitflg) {
			sc.isTrigger = false;
			flg = true;
		}

		return flg;
	}

	//ボールの表示状態
	public bool GetEnable(){
		return mr.enabled;
	}

	public void SetEnable(bool enable){
		mr.enabled = enable;
	}



	//当たり判定(移動中)
	void OnCollisionEnter(Collision col){
		
		//移動中
		if (col.gameObject.tag == "Holl") {
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				mr.enabled = false;
		}
	}

	//当たり判定(ファウル中)
	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Ball" || col.gameObject.tag == "Holl") {
			hitflg = true;
		}


	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Ball" || col.gameObject.tag == "Holl") {
			hitflg = true;
		}
	}

	void OnTriggerExit(Collider col){
		
			hitflg = false;

	}



}
