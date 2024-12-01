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
        //tileLayer = 1 << LayerMask.NameToLayer("Tile"); // �ڵ带 �̿��� ��
        movement = GetComponent<Movement2D>();
    }

    private void Update()
    {
        // 1. ����Ű �Է����� �̵����� ����
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

        // 2. �̵����⿡ ���� �߻� (��ֹ� �˻�)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. ��ֹ��� ������ �̵�
        if(hit.transform == null)
        {
            // MoveTo() �޼ҵ忡 �̵������� �Ű������� ������ �̵�
            movement.MoveTo(moveDirection);
        }
    }
}
