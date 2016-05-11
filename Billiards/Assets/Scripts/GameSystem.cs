using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour {

	public GameObject otherBall;
	public QueControl queCont;
	public MainBollControll mainBallCont;
	public Text faulMessage, EndMessage;
	public Button EndButton, RestertButton;
	public Parameter para;
	OtherBallControll[] OtherBallCont;


	public enum seen{
		SHOTSEEN = 1,
		MOVESEEN = 2,
		FAULSEEN = 3,
		CLEARSEEN = 4,
	};

	seen nowSeen;
	float timer;
	bool ball_set_flg;

	// Use this for initialization
	void Start () {
		nowSeen = seen.SHOTSEEN;
		timer = 0;

		ball_set_flg = false;

		faulMessage.enabled = false;
		EndMessage.enabled = false;
		EndButton.gameObject.SetActive(false);
		RestertButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		switch (nowSeen) {
		case seen.SHOTSEEN:

			//キューのショット
			queCont.Shot ();

			///ショットされたら次のシーンに変更
			if (queCont.GetShotFlg()) {
				nowSeen = seen.MOVESEEN;
			}
				break;
		case seen.MOVESEEN:
			if (mainBallCont.GetMoveEnd()) {
				int count = 0;
				OtherBallCont = otherBall.transform.GetComponentsInChildren<OtherBallControll> ();

				for (int i = 0; i < OtherBallCont.Length; i++) {
					if (OtherBallCont [i].GetMoveEnd () || !OtherBallCont [i].GetMoveFlg()) {
						count++;
					}
				}

				if (OtherBallCont.Length == 0) {
					nowSeen = seen.CLEARSEEN;
				} //止まっている数がオブジェクトの数と一緒なら
				else if (count == OtherBallCont.Length){
					//表示されていればショットモードに
					if (mainBallCont.GetEnable()) {
						queCont.ShowEnable();
						queCont.SetShotPower (0);
						nowSeen = seen.SHOTSEEN;
					} else {
						//自球が消えていればファウルに
						faulMessage.enabled = true;
						ball_set_flg = false;
						nowSeen = seen.FAULSEEN;
					}
				}
			}
				break;
		case seen.FAULSEEN:
			//ファウルだった場合文字の表示
			if (faulMessage.enabled) {
				if (timer <= 3) {
					timer += Time.deltaTime;
				} else {
					faulMessage.enabled = false;
					mainBallCont.FaulStart ();
				}
			} else {
				if (!ball_set_flg) {
					ball_set_flg = mainBallCont.FaulMove ();
				} else {
					StartCoroutine("sleep");
					queCont.SetShotPower (0);
					timer = 0;
				}
			}
				break;
			case seen.CLEARSEEN:
				EndMessage.enabled = true;
				EndButton.gameObject.SetActive(true);
				RestertButton.gameObject.SetActive(true);
				break;
		}


			
	}

	IEnumerator sleep(){
		yield return new WaitForSeconds(1);
		queCont.ShowEnable();
		nowSeen = seen.SHOTSEEN;
	}

	//現在のシーンの取得
	public seen GetSeen(){
		return nowSeen;
	}

	public void RestertGame(){
		SceneManager.LoadScene("MainGame");
	}

	public void EndGame(){
		Application.Quit();
	}
}
