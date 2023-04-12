using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    // �o�ߎ���
    private float _nowTime = 0;
    // �ړ�����܂ł̎���
    [SerializeField]
    private float _moveTime = 1f;
    // �G�̑��x�㏸����
    [SerializeField]
    private float _moveSpeedLevel = 1.5f;
    // �Ō�̈�C�ɂȂ������̏㏸���x
    [SerializeField]
    private float _moveMaxSpeedLevel = 3f;
    // �ړ���
    private float _moveVolume = 0.25f;
    // �ő�ړ���
    [SerializeField]
    private float _moveVolumeMaximam = 1.5f;

    // �ړ��̃^�C�v�𗣎U�l�ɂ��邩�ǂ���
    [SerializeField]
    private bool _moveTypeTranslate = true;

    // �i������
    [SerializeField]
    private float _rowDownVolume = -0.5f;

    // �i���������ǂ���
    private bool _isRowDown = false;

    private Rigidbody _enemyRigidbody = default;
    // ���������ړ��̕␳�l
    [SerializeField]
    private float _rigidbodyMoveCorrection = 4f;

    // �G�̍ő吔���i�[
    private float _enemyMaxCount = default;

    // ���i�K���x���グ�邩�̐ݒ�
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

            // �i������Ԃɂ���
            _isRowDown = true;

            // ���]����
            _moveVolume *= -1;

            // �o�ߎ��ԃ��Z�b�g
            _nowTime = 0;

            // �i��������
            this.transform.Translate(0,_rowDownVolume,0);
            if (!_moveTypeTranslate) {
                _enemyRigidbody.velocity = new Vector3(_moveVolume * _rigidbodyMoveCorrection, 0, 0);
            }
        }
        // �G��|������
        if (other.tag.Equals("Shot")) {

            // �c��1�C�ɂȂ�����ړ����x���x���������グ��
            if ((this.transform.childCount - 1) == 1) {
                _moveSpeedLevel = _moveMaxSpeedLevel;
                SpeedUp();
                print("�Ō�̈�C");
            }

            // �G�̍ő�J�E���g���j�󃌁[�g��茻�݂̓G�����������G�̈ړ����x���グ��
            if (this.transform.childCount-1 < (_enemyMaxCount - _enemyDestructionSpeedLate)) {

                SpeedUp();

                // �G�̑��x���グ�邽�߂̃��[�g���グ��
                _enemyDestructionSpeedLate += _enemyDestructionSpeedLate;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        _isRowDown = false;
    }
    public void SpeedUp() {

        // �ړ����x�̏������������ꍇ
        if (_moveVolumeMaximam > Mathf.Abs(_moveVolume)) {
            // �ړ����x���グ��
            _moveVolume *= _moveSpeedLevel;
        }

        // �ړ��܂ł̎��Ԃ����炷
        _moveTime /= _moveSpeedLevel;
    }
}
