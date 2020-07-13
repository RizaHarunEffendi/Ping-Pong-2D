using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour 
{
	Text ScoreUIP1, ScoreUIP2;
	int ScoreP1, ScoreP2;
	GameObject panelSelesai;
	Text txPemenang;

	public int force;
	Rigidbody2D rigid;	
	// Use this for initialization
	void Start () {
		ScoreP1 = 0;
		ScoreP2 = 0;

		ScoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
		ScoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();

		panelSelesai = GameObject.Find("PanelSelesai");
		panelSelesai.SetActive(false);

		rigid = GetComponent<Rigidbody2D>();
		Vector2 arah = new Vector2(2,0).normalized;
		rigid.AddForce(arah * force);
	}
	// Update is called once per frame
	void Update () {
		
	}
	void TampilkanScore(){
		ScoreUIP1.text = ScoreP1 + "";
		ScoreUIP2.text = ScoreP2 + "";
	}
	void ResetBall() {
		transform.localPosition = new Vector2(0,0);
		rigid.velocity = new Vector2(0,0);
	}
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "TepiKiri")
		{
			if(ScoreP2 == 5){
				panelSelesai.SetActive(true);
				txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
				txPemenang.text = "Player Biru Pemenang !";
				Destroy(gameObject);
				return;
			}
			ScoreP2 += 1;
			TampilkanScore();
			ResetBall();
			Vector2 arah = new Vector2(-2,0).normalized;
			rigid.AddForce(arah * force);
		}
		if (coll.gameObject.name == "TepiKanan")
		{
			if(ScoreP1 == 5){
				panelSelesai.SetActive(true);
				txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
				txPemenang.text = "Player Kuning Pemenang !";
				Destroy(gameObject);
				return;
			}
			ScoreP1 += 1;
			TampilkanScore();
			ResetBall();
			Vector2 arah = new Vector2(2,0).normalized;
			rigid.AddForce(arah * force);
		}
		if(coll.gameObject.name =="Pemukul1" || coll.gameObject.name =="Pemukul2")
		{
			float sudut = (transform.position.y - coll.transform.position.y) * 5f;
			Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
			rigid.velocity = new Vector2(0,0);
			rigid.AddForce(arah * force * 2);
		}
	}
}
