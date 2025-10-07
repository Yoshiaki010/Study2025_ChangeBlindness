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
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            return;
        }

        // Aキーの入力状態取得
        var aKey = current.aKey;
        var dKey = current.dKey;

        // Aキーが押された瞬間かどうか
        if (aKey.isPressed)
        {
//            Debug.Log("pressed a");
            Player.transform.Rotate(0f, -0.5f, 0f);
        }

        // Dキーの入力状態取得
        if (dKey.isPressed)
        {
//            Debug.Log("pressed d");
            Player.transform.Rotate(0f, 0.5f, 0f);
        }
    }
}
