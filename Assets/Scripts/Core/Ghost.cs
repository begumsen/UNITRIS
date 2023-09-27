using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    Block m_ghostShape = null;
    bool m_hitBottom = false;
    public Color m_color = new Color(1f, 1f, 1f, 0.2f);

    public void DrawGhost(Block originalShape, GameBoard gameBoard)
    {
        if (!m_ghostShape)
        {
            m_ghostShape = Instantiate(originalShape, originalShape.transform.position, originalShape.transform.rotation) as Block;
            m_ghostShape.gameObject.name = "GhostShape";

            SpriteRenderer[] allRenderers = m_ghostShape.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer r in allRenderers)
            {
                r.color = m_color;
            }

        }
        else
        {
            m_ghostShape.transform.position = originalShape.transform.position;
            m_ghostShape.transform.rotation = originalShape.transform.rotation;

        }

        m_hitBottom = false;

        while (!m_hitBottom)
        {
            m_ghostShape.MoveDown();
            if (!gameBoard.IsValidMove((m_ghostShape), new Vector2(0,-1)))
            {
                m_hitBottom = true;
            }
        }

    }

    public void Reset()
    {
        Destroy(m_ghostShape.gameObject);

    }

}
