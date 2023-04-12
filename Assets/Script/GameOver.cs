using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    // コライダーに何かが入ってきた時の処理
    private void OnTriggerEnter(Collider other) {

        // 何かが入ったらGameOverのシーンへ遷移する
        SceneManager.LoadScene("GameOver");
    }

}
