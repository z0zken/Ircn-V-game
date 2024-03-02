using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestHandler : MonoBehaviour
{
    [SerializeField]
    public Tilemap interactableMap; // Kéo và thả Tilemap bạn muốn làm việc vào đây trong Inspector
    public TileBase highlightTile;   // Kéo và thả Tile bạn muốn sử dụng để làm nổi bật (ví dụ: Tile màu đỏ)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lấy vị trí chuột trong không gian thế giới
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Lấy vị trí ô trong Tilemap tương ứng với vị trí chuột
            Vector3Int cellPosition = interactableMap.WorldToCell(mouseWorldPos);

            // Hiện đỏ xung quanh ô được click
            HighlightSurroundingTiles(cellPosition);
        }
    }

    void HighlightSurroundingTiles(Vector3Int centerCell)
    {
        // Xóa tất cả các tile nổi bật trước đó
        interactableMap.ClearAllTiles();

        // Duyệt qua các ô xung quanh và đặt tile nổi bật
        for (int x = centerCell.x - 1; x <= centerCell.x + 1; x++)
        {
            for (int y = centerCell.y - 1; y <= centerCell.y + 1; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, centerCell.z);

                // Đặt tile nổi bật tại vị trí hiện tại
                interactableMap.SetTile(currentCell, highlightTile);
            }
        }
    }
}
