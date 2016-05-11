using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class QueControl : MonoBehaviour {

	//ボールの位置
	public GameObject MainBall;
	public Parameter para;
	GameObject _child;

	Vector2 mousePos;					//マウス位置

	bool shotFlg;						//ショットフラグ
	float shotPower;					//ショットパワー
	bool shotPowerChange;				//ショットパワー上下切り替え

	public RawImage bar;
	float parsent;

	float bar_pos_lenght, bar_lenght;

	// Use this for initialization
	void Start () {
		_child = this.transform.FindChild ("Que").gameObject;
		mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f);
		this.transform.position = MainBall.transform.position;

		shotPower = 0;
		shotFlg = false;
		shotPowerChange = false;

		bar.rectTransform.anchoredPosition = new Vector2(bar.rectTransform.anchoredPosition.x, para.MIN_BAR_POSY);
		bar.rectTransform.sizeDelta = new Vector2 (bar.rectTransform.sizeDelta.x, para.MIN_BAR);

		bar_lenght = para.MAX_BAR - para.MIN_BAR;
		bar_pos_lenght = para.MAX_BAR_POSY - para.MIN_BAR_POSY;

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Shot(){
		
		//キューが表示されている時
		if (_child.activeInHierarchy) {

			//キューの移動
			float x = mousePos.x - Input.mousePosition.x;
			mousePos.x = Input.mousePosition.x;
			Vector3 aular = new Vector3 (0.0f, x, 0.0f);
			this.transform.Rotate (aular, Space.World);	

			//右クリックが押されたら
			if (Input.GetMouseButtonDown (0)) {
				_child.SetActive (false);
				shotFlg = true;
				MainBall.GetComponent<MainBollControll> ().MoveStart ();

				//規定値以下ならば
				if (shotPower <= para.MIN_POWER) {
					shotPower += para.MIN_POWER;
				}
			}



			//パワー加算
			if (shotPowerChange) {
				shotPower += 0.5f;

				//最大パワー以上なら下降に変化
				if ((int)shotPower >= para.MAX_POWER) {
					shotPowerChange = false;
				}
			} else {
				shotPower -= 0.5f;

				//0以下なら上昇に変化
				if ((int)shotPower <= 0) {
					shotPowerChange = true;
				}
			}

			if (!shotFlg) {
				parsent = shotPower / para.MAX_POWER;
				bar.rectTransform.anchoredPosition = new Vector2 (bar.rectTransform.anchoredPosition.x, para.MIN_BAR_POSY + bar_pos_lenght * parsent);
				bar.rectTransform.sizeDelta = new Vector2 (bar.rectTransform.sizeDelta.x, para.MIN_BAR + bar_lenght * parsent);
			}
		}



	}

	//キューの表示
	public void ShowEnable(){
		this.transform.position = MainBall.transform.position;
		_child.SetActive (true);
	}


	public void SetShotFlg(bool flg){
		shotFlg = flg;
	}

	public bool GetShotFlg(){
		return shotFlg;
	}

	public void SetShotPower(float power){
		shotPower = power;
	}

	public float GetShotPower(){
		return shotPower;
	}
}
