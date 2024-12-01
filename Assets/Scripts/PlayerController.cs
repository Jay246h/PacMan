using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.right;
    private Movement2D movement;

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
    }

    private void Update()
    {
        // 1. 방향키 입력으로 이동방향 설정
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
        }

        //if (Input.anyKeyDown)
        //{
        //    // MoveTo() 메소드에 이동방향을 매개변수로 전달해 이동
        //    movement.MoveTo(moveDirection);
        //}
    }
}
