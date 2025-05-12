using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransation : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundry;
    [SerializeField] private Direction direction;

    private CinemachineConfiner confiner;

    public enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        // Surandame scenoje esantį CinemachineConfiner2D komponentą
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Atnaujiname kamerai ribas
            confiner.m_BoundingShape2D = mapBoundry;
            // Perstatome žaidėją pagal pasirinkimą
            UpdatePlayerPosition(collision.gameObject);

            MapController_Dynamic.Instance?.UpdateCurrentArea(mapBoundry.name);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += 2f;
                break;
            case Direction.Down:
                newPos.y -= 2f;
                break;
            case Direction.Left:
                newPos.x += 2f;
                break;
            case Direction.Right:
                newPos.x -= 2f;
                break;
        }

        player.transform.position = newPos;
    }
}
