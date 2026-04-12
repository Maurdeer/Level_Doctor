using UnityEngine;

public class Draggable : MonoBehaviour
{

    // https://www.youtube.com/watch?v=yalbvB84kCg

    Vector3 mousePositionOffset;
    public GameManager manager;

    private Vector3 getMouseWorldPosition()
    {
        //capture mouse position
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnMouseDown()
    {
        // capture mouse offset
        if (manager.gamePhase == 0)
        {
            mousePositionOffset = gameObject.transform.position - getMouseWorldPosition();
        }

    }

    private void OnMouseDrag()
    {
        if (manager.gamePhase == 0)
        {
            transform.position = getMouseWorldPosition() + mousePositionOffset;
        }
        
    }


}
