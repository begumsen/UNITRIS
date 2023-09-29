using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// turn this property off if you don't want the shape to rotate (Shape O)
	public bool canRotate = true;
	public Color color;


    private void Start()
    {
        foreach (Transform child in transform)
        {
            // Try to get the SpriteRenderer component from the child
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();

            if (childSpriteRenderer != null)
            {
                // Get the color of the first child's SpriteRenderer
                color = childSpriteRenderer.color;
                break;
            }
        }
    }
    // general move method
    void Move(Vector3 moveDirection)
	{
		transform.position += moveDirection;
	}


	//public methods for moving left, right, up and down, respectively
	public void MoveLeft()
	{
		Move(new Vector3(-1, 0, 0));
	}

	public void MoveRight()
	{
		Move(new Vector3(1, 0, 0));
	}

	public void MoveUp()
	{
		Move(new Vector3(0, 1, 0));
	}

	public void MoveDown()
	{
		Move(new Vector3(0, -1, 0));
	}


	//public methods for rotating right and left
	public void RotateRight()
	{
		if (canRotate)
			transform.Rotate(0, 0, -90);
	}
	public void RotateLeft()
	{
		if (canRotate)
			transform.Rotate(0, 0, 90);
	}
		
}
