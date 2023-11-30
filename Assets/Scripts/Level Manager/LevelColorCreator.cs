using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class LevelColorCreator : MonoBehaviour
{

    [SerializeField] GameObject panel;
    Image selectedColorImage;
    LevelCreator levelCreator;
    // Start is called before the first frame update
    void Start()
    {

        selectedColorImage = panel.GetComponent<Image>();
        levelCreator = GameObject.FindObjectOfType<LevelCreator>() as LevelCreator;
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
                    Transform transform = clickedObject.GetComponent<Transform>();
                    if(spriteRenderer != null)
                    {
                        spriteRenderer.color = selectedColorImage.color;
                        levelCreator.ChangeColor((int)transform.position.y,(int)transform.position.x, selectedColorImage.color);
                    }
                    
                }
            }
        }
    }

    public void ColorSelectionIsFinished()
    {
        EventManager.TriggerEvent(EventName.CustomLevelFinalized, 0);
    }
}
