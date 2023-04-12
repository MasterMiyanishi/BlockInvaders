using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UfoMove : MonoBehaviour {

    // UFOスコア
    [SerializeField]
    private int _ufoScore = 5000;
    // 経過時間
    private float _nowTime = 0;
    // 出現するまでの時間
    [SerializeField]
    private float _respawnTime = 5f;
    // リスポーンの位置
    [SerializeField]
    private Vector3[] _respawnPosition= { new Vector3(-13,6,0), new Vector3(13, 6, 0), };

    [SerializeField]
    private GameObject _effectObject = null;

    // 移動速度
    [SerializeField]
    private float _speed = 0.02f;

    // レンダラー格納用
    private Renderer _ufoRenderer = default;

    // コライダー格納用
    private Collider _ufoColider = default;

    private void Start() {
        _ufoRenderer = this.GetComponent<Renderer>();
        _ufoColider = this.GetComponent<Collider>();

    }
    private void Update() {

        _nowTime += Time.deltaTime;
        // リスポーン時間が経過したらUFOを移動する
        if (_respawnTime < _nowTime) {
            // UFOを表示する
            _ufoRenderer.enabled = true;

            // UFOの当たり判定をつける
            _ufoColider.enabled = true;
            transform.Translate(_speed, 0, 0);
        }
    }
    // コライダーに何かが当たった時の処理
    private void OnTriggerEnter(Collider other) {

        // UFOを非表示にする
        _ufoRenderer.enabled = false;

        // UFOの当たり判定を消す
        _ufoColider.enabled = false;

        // 出現時間リセット
        _nowTime = 0;

        if (other.tag.Equals("Shot")) {
            // スコアを加算する
            GameObject.Find("GameController").GetComponent<GameController>().Score += _ufoScore;

            // エフェクト表示
            if (!_effectObject) {
                return;
            }
            _effectObject.SetActive(false);
            _effectObject.SetActive(true);
            _effectObject.transform.position = transform.position;

        }

        // 何かが当たったらリスポーン位置に移動
        this.transform.position = _respawnPosition[Random.Range(0, _respawnPosition.Length)];
        _speed = Mathf.Abs(_speed) * -Mathf.Sign(this.transform.position.x);

    }
}
