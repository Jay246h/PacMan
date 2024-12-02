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

        // �̵������� ���Ƿ� ����
        SetMoveDirectionByRandom();
    }

    private void Update()
    {
        // 2. �̵� ���⿡ ���� �߻� (��ֹ� �˻�)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. ��ֹ��� ������ �̵�
        if(hit.transform == null)
        {
            // MoveTo() �żҵ忡 �̵� ������ �Ű������� ������ �̵�
            movement2D.MoveTo(moveDirection);
            // ȭ�� ������ ������ �Ǹ� �ݴ����� ����
            aroundWrap.UpdateAroundWrap();
        }
        else
        {
            SetMoveDirectionByRandom();
        }

    }

   private void SetMoveDirectionByRandom()
    {
        // �̵� ������ ���Ƿ� �����ؼ� SetMoveDirection() �żҵ� ȣ��
        direction = (Direction)Random.Range(0, (int)Direction.Count);
        // Vector3 Ÿ���� �̵� ���� �� ����
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
