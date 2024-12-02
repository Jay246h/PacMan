using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    private LayerMask tileLayer;

    private Vector2 moveDirection = Vector2.right;
    private Direction direction = Direction.Right;
    private float rayDistance = 0.55f;

    private Movement2D movement2D;
    private AroundWrap aroundWrap;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();

        // 이동방향을 임의로 설정
        SetMoveDirectionByRandom();
    }

    private void Update()
    {
        // 2. 이동 방향에 광선 발사 (장애물 검사)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. 장애물이 없으면 이동
        if(hit.transform == null)
        {
            // MoveTo() 매소드에 이동 방향을 매개변수로 전달해 이동
            movement2D.MoveTo(moveDirection);
            // 화면 밖으로 나가게 되면 반대편에서 등장
            aroundWrap.UpdateAroundWrap();
        }
        else
        {
            SetMoveDirectionByRandom();
        }

    }

   private void SetMoveDirectionByRandom()
    {
        // 이동 방향을 임의로 설정해서 SetMoveDirection() 매소드 호출
        direction = (Direction)Random.Range(0, (int)Direction.Count);
        // Vector3 타입의 이동 방향 값 설정
        moveDirection = Vector3FromEnum(direction);
    }

    private Vector3 Vector3FromEnum(Direction state)
    {
        Vector3 direction = Vector3.zero;

        switch(state)
        {
            case Direction.Up: direction = Vector3.up; break;
            case Direction.Down: direction = Vector3.down; break;
            case Direction.Right: direction = Vector3.right; break;
            case Direction.Left: direction = Vector3.left; break;

        }
        return direction;
    }
}
