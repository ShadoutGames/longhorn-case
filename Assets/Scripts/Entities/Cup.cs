using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField]
    private Material waterMaterial;

    private bool isFlowersWatered = false;
    private Material firstMaterial;
    private Renderer meshRenderer;
    private Vector3 offset;
    private Vector3 firstPosition;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<Renderer>();

        firstPosition = transform.position;
        firstMaterial = meshRenderer.material;
    }

    private void OnMouseDown()
    {
        if(GameManager.Instance.State != GameState.WaterState) return;

        LeanTween.cancel(gameObject);
        offset = Camera.main.transform.position - transform.position;
    }

    private void OnMouseDrag()
    {
        if(GameManager.Instance.State != GameState.WaterState) return;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, offset.z));
    }

    private void OnMouseUp()
    {
        if(GameManager.Instance.State != GameState.WaterState) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position - Camera.main.transform.position, out hit, 20))
        {
            var water = hit.transform.gameObject.GetComponentInParent<Water>();
            if (water != null)
            {
                LeanTween.move(gameObject, water.transform.GetChild(0), 1).setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() => 
                    {
                        meshRenderer.material = waterMaterial;
                    });
                return;
            }

            var flowerpot = hit.transform.gameObject.GetComponentInParent<Flowerpot>();
            if (flowerpot != null)
            {
                LeanTween.move(gameObject, flowerpot.transform.GetChild(0), 1).setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() => 
                    {
                        LeanTween.rotateX(gameObject, 30, 1).setEase(LeanTweenType.easeOutQuad).setLoopPingPong(1);
                        LeanTween.rotateZ(gameObject, 30, 1).setEase(LeanTweenType.easeOutQuad).setLoopPingPong(1)
                        .setOnComplete(()=> LeanTween.move(gameObject, firstPosition, 1).setEase(LeanTweenType.easeOutQuad));
                        meshRenderer.material = firstMaterial;
                        isFlowersWatered = true;
                    });
                return;
            }
            
            var trash = hit.transform.gameObject.GetComponentInParent<Trash>();
            if (trash != null)
            {
                if(isFlowersWatered)
                {
                    LeanTween.move(gameObject, trash.transform.GetChild(0), 1).setEase(LeanTweenType.easeOutQuad)
                        .setOnComplete(() => 
                        {
                            gameObject.SetActive(false);
                            GameManager.Instance.ChangeGameState(GameState.DoorState);
                        });
                    return;
                }
            }
        }

        LeanTween.move(gameObject, firstPosition, 1).setEase(LeanTweenType.easeOutQuad);
    }
}
