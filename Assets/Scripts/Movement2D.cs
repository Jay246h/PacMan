using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f;  // �� ĭ �̵��� �ʿ��� �ð�
    private bool isMoved = false;   // ������Ʈ�� �̵�/��� ���� ����

    public bool MoveTo(Vector3 moveDirection)
    {
        // �̵����̶�� �޼��尡 ������� �ʰ� ��
        if (isMoved)
        {
            return false;
        }

        // ���� ��ġ�κ��� �̵��������� 1 ���� �̵��� ��ġ�� �Ű������� �ڷ�ƾ �޼ҵ� ����
        StartCoroutine(SmoothGridMovemet(transform.position + moveDirection));

        return true;
    }

    private IEnumerator SmoothGridMovemet(Vector2 endPostion)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        // movTime �ð�����  while() �ݺ��� ���
        // while() �ݺ����� ȣ���ϴ� ���� isMoved = true, �ݺ��� ���� �� isMoved = false
        isMoved = true;

        while (percent < 1) 
        {
            percent += Time.deltaTime;
            // startPosition���� endPosition���� moveTime �ð� ���� �̵�
            transform.position = Vector2.Lerp(startPosition, endPostion, percent);

            yield return null;
        }

        isMoved = false;
    }
}
