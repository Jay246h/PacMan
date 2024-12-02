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
        //tileLayer = 1 << LayerMask.NameToLayer("Tile"); // 코드를 이용할 때
        movement = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 1. 방향키 입력으로 이동방향 설정
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

        // 2. 이동방향에 광선 발사 (장애물 검사)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. 장애물이 없으면 이동
        if(hit.transform == null)
        {
            // MoveTo() 메소드에 이동방향을 매개변수로 전달해 이동
            bool movePossible = movement.MoveTo(moveDirection);
            // 이동을 수행하기 되면
            if(movePossible)
            {
                // 오브젝트 회전 시킴 (애니메이션을 처리해도 됨)
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }

            // 화변 밖으로 나가면 반대편에서 등장
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
