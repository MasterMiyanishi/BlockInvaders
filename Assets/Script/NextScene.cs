using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    // 移動するシーンの名前
    public string nextSceneName = "";

	void Update () {

        // 何かが押されたとき
        if (Input.anyKey)
        {
            // 指定したシーンへ遷移する
            SceneManager.LoadScene(nextSceneName);
        }
	}
}
