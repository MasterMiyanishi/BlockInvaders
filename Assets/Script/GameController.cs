using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // スコア
    private int score = 0;

    public int Score {
        get => score;
        set => score = value;
    }
    private void Update () {
        // Blockのタグが付いたオブジェクトを検索し
        // その個数が0以下の場合ゲームクリア画面に遷移する
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            // GameClearのシーンへ遷移する
            SceneManager.LoadScene("GameClear");
        }
	}
}
