using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        // ���݂̃L�[�{�[�h���
        var current = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (current == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }

        // A�L�[�̓��͏�Ԏ擾
        var aKey = current.aKey;
        var dKey = current.dKey;

        // A�L�[�������ꂽ�u�Ԃ��ǂ���
        if (aKey.isPressed)
        {
//            Debug.Log("pressed a");
            Player.transform.Rotate(0f, -0.5f, 0f);
        }

        // D�L�[�̓��͏�Ԏ擾
        if (dKey.isPressed)
        {
//            Debug.Log("pressed d");
            Player.transform.Rotate(0f, 0.5f, 0f);
        }
    }
}
