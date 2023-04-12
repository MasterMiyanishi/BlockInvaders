using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    // 経過時間
    private float _nowTime = 0;
    // 移動するまでの時間
    [SerializeField]
    private float _moveTime = 1f;
    // 敵の速度上昇割合
    [SerializeField]
    private float _moveSpeedLevel = 1.5f;
    // 最後の一匹になった時の上昇速度
    [SerializeField]
    private float _moveMaxSpeedLevel = 3f;
    // 移動量
    private float _moveVolume = 0.25f;
    // 最大移動量
    [SerializeField]
    private float _moveVolumeMaximam = 1.5f;

    // 移動のタイプを離散値にするかどうか
    [SerializeField]
    private bool _moveTypeTranslate = true;

    // 段下げ量
    [SerializeField]
    private float _rowDownVolume = -0.5f;

    // 段下げ中かどうか
    private bool _isRowDown = false;

    private Rigidbody _enemyRigidbody = default;
    // 物理挙動移動の補正値
    [SerializeField]
    private float _rigidbodyMoveCorrection = 4f;

    // 敵の最大数を格納
    private float _enemyMaxCount = default;

    // 何段階速度を上げるかの設定
    [SerializeField]
    private int _enemyDestructionSteps = 5;
    private float _enemyDestructionSpeedLate = default;

    private void Start() {
        _enemyRigidbody = this.GetComponent<Rigidbody>();
        _enemyMaxCount = this.transform.childCount;
        _enemyDestructionSpeedLate = this.transform.childCount / _enemyDestructionSteps;
    }

    private void Update() {
        _nowTime += Time.deltaTime;

        if (_moveTime < _nowTime) {
            if (_moveTypeTranslate) {
                this.transform.Translate(_moveVolume,0,0);
            } else {
                _enemyRigidbody.velocity = new Vector3(_moveVolume * _rigidbodyMoveCorrection, 0,0);
            }
            _nowTime = 0;
        }

    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Wall") && !_isRowDown) {

            // 段下げ状態にする
            _isRowDown = true;

            // 反転する
            _moveVolume *= -1;

            // 経過時間リセット
            _nowTime = 0;

            // 段下げする
            this.transform.Translate(0,_rowDownVolume,0);
            if (!_moveTypeTranslate) {
                _enemyRigidbody.velocity = new Vector3(_moveVolume * _rigidbodyMoveCorrection, 0, 0);
            }
        }
        // 敵を倒した時
        if (other.tag.Equals("Shot")) {

            // 残り1匹になったら移動速度レベルを引き上げる
            if ((this.transform.childCount - 1) == 1) {
                _moveSpeedLevel = _moveMaxSpeedLevel;
                SpeedUp();
                print("最後の一匹");
            }

            // 敵の最大カウント÷破壊レートより現在の敵が下回ったら敵の移動速度を上げる
            if (this.transform.childCount-1 < (_enemyMaxCount - _enemyDestructionSpeedLate)) {

                SpeedUp();

                // 敵の速度を上げるためのレートを上げる
                _enemyDestructionSpeedLate += _enemyDestructionSpeedLate;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        _isRowDown = false;
    }
    public void SpeedUp() {

        // 移動速度の上限を下回った場合
        if (_moveVolumeMaximam > Mathf.Abs(_moveVolume)) {
            // 移動速度を上げる
            _moveVolume *= _moveSpeedLevel;
        }

        // 移動までの時間を減らす
        _moveTime /= _moveSpeedLevel;
    }
}
