using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    // ショットの発射速度
    [SerializeField]
    private float _speed = 10f;

    // ショット発射済みかどうか
    private bool _isShot = false;

    [SerializeField] 
    private GameObject _effectObject = null;

    // ショットの種類を離散値移動にするかどうか
    [SerializeField]
    private bool _shotTypeTranslate = true;

    // ショットのスポーン位置を設定する
    [SerializeField]
    private GameObject _shotSpawnPosition = default;

    // レンダラー格納用
    private Renderer _shotRenderer = default;

    // コライダー格納用
    private Collider _shotColider = default;

    // コライダー格納用
    private Rigidbody _shotRigidbody = default;

    private void Start() {
        _shotRenderer = this.GetComponent<Renderer>();
        _shotColider = this.GetComponent<Collider>();
        _shotRigidbody = this.GetComponent<Rigidbody>();

    }
    private void Update () {

        // ショットが発射されているときのみ実行する
        if (_isShot) {

            if (_shotTypeTranslate) {
                this.transform.Translate(0, _speed * Time.deltaTime, 0);
            } else {
                _shotRigidbody.velocity = new Vector3(0, _speed, 0);
            }

        }else if (Input.GetButtonDown("Fire1")) {

            // ショットを発射済みにする
            _isShot = true;

            // ショットを表示にする
            _shotRenderer.enabled = true;

            // ショットの当たり判定をつける
            _shotColider.enabled = true;

            // ショットの位置をSpawnの位置に移動する
            this.transform.position = _shotSpawnPosition.transform.position;
        }

    }
    private void OnTriggerEnter(Collider other) {

        if (other.tag.Equals("Player")) {
            return;
        }
        // ショットを未発射にする
        _isShot = false;

        // ショットを非表示にする
        _shotRenderer.enabled = false;

        // ショットの当たり判定を消す
        _shotColider.enabled = false;

        // 物理挙動の場合加速度をリセットする
        if (!_shotTypeTranslate) {
            _shotRigidbody.velocity = Vector3.zero;
        }
        // エフェクトを表示する
        _effectObject.SetActive(true);
        _effectObject.transform.position = transform.position;
        
    }
}
