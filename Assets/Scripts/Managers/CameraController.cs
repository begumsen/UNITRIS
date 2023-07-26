using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform boardTransform; // Private reference to the board's transform
    public float padding = 4f; // Extra padding around the board

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Find the board by tag (assuming you tagged your board GameObject with "Board")
        GameObject boardObject = GameObject.FindWithTag("GameBoard");
        if (boardObject != null)
        {
            boardTransform = boardObject.transform;
        }
        else
        {
            Debug.LogWarning("No board found with the 'Board' tag.");
        }
    }

    private void Update()
    {
        if (boardTransform == null)
        {
            return;
        }

        // Get the bounds of the board in world space
        Bounds boardBounds = CalculateBoardBounds();

        // Calculate the camera size based on the board's width, height, and padding
        float targetCameraSize = CalculateCameraSize(boardBounds);

        // Calculate the desired camera position, considering the padding and placing the board at the bottom
        float cameraYPosition = targetCameraSize-1;

        // Move the camera to the center of the board with the board at the bottom of the camera's view
        Vector3 targetCameraPosition = new Vector3(boardBounds.center.x+1.5f, cameraYPosition, mainCamera.transform.position.z);

        mainCamera.transform.position = targetCameraPosition;
        mainCamera.orthographicSize = targetCameraSize;
    }

    private Bounds CalculateBoardBounds()
    {
        // Calculate the bounds of the board in world space
        Renderer[] renderers = boardTransform.GetComponentsInChildren<Renderer>();
        Bounds boardBounds = new Bounds(boardTransform.position, Vector3.zero);

        foreach (Renderer renderer in renderers)
        {
            boardBounds.Encapsulate(renderer.bounds);
        }

        return boardBounds;
    }

    private float CalculateCameraSize(Bounds targetBounds)
    {
        // Calculate the required camera size based on the target bounds and padding
        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = targetBounds.size.x / targetBounds.size.y;

        float targetHeight = targetBounds.size.y + padding * 4f;
        float targetWidth = targetBounds.size.x + padding * 2.5f;

        if (screenAspect >= targetAspect)
        {
            return targetHeight * 0.54f;
        }
        else
        {
            float correctAspectRatioHeight = targetWidth / screenAspect;
            return correctAspectRatioHeight * 0.5f;
        }
    }
}
