using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAddScript : MonoBehaviour {

	void Update () {

        // スコア表示
        this.GetComponent<Text>().text = 
            GameObject.Find("GameController").GetComponent<GameController>().Score.ToString();

	}
}
