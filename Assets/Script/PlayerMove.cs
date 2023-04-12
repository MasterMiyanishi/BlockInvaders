using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    // プレイヤーの移動速度
    [SerializeField]
    private float _speed = 5f;

    // 移動のタイプを離散値移動にするかどうか
    [SerializeField]
    private bool _moveTypeTranslate = true;

    private Rigidbody _moveRigidbody = default;

    private void Start() {
        _moveRigidbody = this.GetComponent<Rigidbody>();
    }
    private void Update () {

        // キー入力を受け付ける
        float inputX = Input.GetAxis("Horizontal");

        // プレイヤーを移動する
        if (_moveTypeTranslate) {
            transform.Translate(inputX * _speed * Time.deltaTime, 0, 0);
        } else {
            _moveRigidbody.velocity = new Vector3(inputX * _speed, 0, 0);
        }

	}
}
