using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 firstPosition;
    private Quaternion firstRotation;

    private void Awake()
    {
        firstPosition = transform.position;
        firstRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.State != GameState.BoardState) return;

        LeanTween.cancel(gameObject);
        offset = Camera.main.transform.position - transform.position;
    }

    private void OnMouseDrag()
    {
        if(GameManager.Instance.State != GameState.BoardState) return;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, offset.z));
    }

    private void OnMouseUp()
    {
        if(GameManager.Instance.State != GameState.BoardState) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position - Camera.main.transform.position, out hit, 20))
        {
            var board = hit.transform.gameObject.GetComponentInParent<Board>();
            if (board != null)
            {
                LeanTween.move(gameObject, board.transform.GetChild(0), 1).setEase(LeanTweenType.easeOutQuad);
                LeanTween.rotate(gameObject, board.transform.GetChild(0).eulerAngles, 1).setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() => 
                    {
                        LeanTween.move(gameObject, new Vector3(transform.position.x + .2f, transform.position.y + .1f, transform.position.z), 1).setEase(LeanTweenType.easeShake)
                        .setOnComplete(() => 
                        {
                            LeanTween.move(gameObject, firstPosition, 1).setEase(LeanTweenType.easeOutQuad);
                            LeanTween.rotate(gameObject, firstRotation.eulerAngles, 1).setEase(LeanTweenType.easeOutQuad);
                            GameManager.Instance.ChangeGameState(GameState.WaterState);
                        });

                        board.Paint();
                    });
                return;
            }
        }

        LeanTween.move(gameObject, firstPosition, 1).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotate(gameObject, firstRotation.eulerAngles, 1).setEase(LeanTweenType.easeOutQuad);
    }
}
