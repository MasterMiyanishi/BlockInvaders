using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockBreak : MonoBehaviour {
    // 倒した時のスコア
    [SerializeField]
    private int _enemyScore = 100;

    [SerializeField]
    private GameObject _effectObject = null;

    // コライダーに何かが当たった時の処理
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            // 何かが入ったらGameOverのシーンへ遷移する
            SceneManager.LoadScene("GameOver");
        } else if(other.tag.Equals("Shot")) {
            // スコアを加算する
            GameObject.Find("GameController").GetComponent<GameController>().Score += _enemyScore;

            // 何かが当たったら自分を削除する
            Destroy(this.gameObject);
        }
    }
    // 破壊された時エフェクトを表示する
    private void OnDestroy() {
        if (!_effectObject) {
            return;
        }
        _effectObject.SetActive(false);
        _effectObject.SetActive(true);
        _effectObject.transform.position = transform.position;
    }
}
