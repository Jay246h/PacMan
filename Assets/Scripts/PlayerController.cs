using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask tileLayer;

    private Vector2 moveDirection = Vector2.right;
    private float rayDistance = 0.55f;
    private Direction direction = Direction.Right;

    private Movement2D movement;
    private AroundWrap aroundWrap;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //tileLayer = 1 << LayerMask.NameToLayer("Tile"); // �ڵ带 �̿��� ��
        movement = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 1. ����Ű �Է����� �̵����� ����
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
            direction = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
            direction = Direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            direction = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
            direction = Direction.Right;
        }

        // 2. �̵����⿡ ���� �߻� (��ֹ� �˻�)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. ��ֹ��� ������ �̵�
        if(hit.transform == null)
        {
            // MoveTo() �޼ҵ忡 �̵������� �Ű������� ������ �̵�
            bool movePossible = movement.MoveTo(moveDirection);
            // �̵��� �����ϱ� �Ǹ�
            if(movePossible)
            {
                // ������Ʈ ȸ�� ��Ŵ (�ִϸ��̼��� ó���ص� ��)
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }

            // ȭ�� ������ ������ �ݴ����� ����
            aroundWrap.UpdateAroundWrap();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Enemy"))
        {
            StopCoroutine(nameof(OnHit));
            StartCoroutine(nameof(OnHit));

            Destroy(collision.gameObject);
        }
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = Color.white;
    }
}
