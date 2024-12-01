using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask tileLayer;

    private Vector2 moveDirection = Vector2.right;
    private float rayDistance = 0.55f;

    private Movement2D movement;

    private void Awake()
    {
        //tileLayer = 1 << LayerMask.NameToLayer("Tile"); // 코드를 이용할 때
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

        // 2. 이동방향에 광선 발사 (장애물 검사)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. 장애물이 없으면 이동
        if(hit.transform == null)
        {
            // MoveTo() 메소드에 이동방향을 매개변수로 전달해 이동
            movement.MoveTo(moveDirection);
        }
    }
}
