using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelColorCreator : MonoBehaviour
{

    [SerializeField] GameObject panel;
    Image selectedColorImage;
    // Start is called before the first frame update
    void Start()
    {
        selectedColorImage = panel.GetComponent<Image>();
    }

    private void Update()
    {
        CheckForColorChangeClick();
    }

    void CheckForColorChangeClick()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.transform.gameObject;
                if (clickedObject != null)
                {
                    SpriteRenderer spriteRenderer = clickedObject.GetComponent<SpriteRenderer>();
                    if(spriteRenderer != null)
                    {
                        spriteRenderer.color = selectedColorImage.color;
                    }
                    
                }
            }
        }
    }
}
