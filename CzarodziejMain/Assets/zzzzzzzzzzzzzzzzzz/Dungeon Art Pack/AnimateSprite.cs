using UnityEngine;

public class AnimateSprite : MonoBehaviour
{
    //vars for the whole sheet
    public int colCount = 4;
    public int colNumber = 0; //Zero Indexed
    public int fps = 10;

    private Vector2 offset;
    public int rowCount = 4;

    //vars for animation
    public int rowNumber = 0; //Zero Indexed
    public int totalCells = 4;

    //Update
    private void Update()
    {
        SetSpriteAnimation(colCount, rowCount, rowNumber, colNumber, totalCells, fps);
    }

    //SetSpriteAnimation
    private void SetSpriteAnimation(int colCount, int rowCount, int rowNumber, int colNumber, int totalCells, int fps)
    {
        // Calculate index
        var index = (int) (Time.time*fps);
        // Repeat when exhausting all cells
        index = index%totalCells;

        // Size of every cell
        var sizeX = 1.0f/colCount;
        var sizeY = 1.0f/rowCount;
        var size = new Vector2(sizeX, sizeY);

        // split into horizontal and vertical index
        var uIndex = index%colCount;
        var vIndex = index/colCount;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        var offsetX = (uIndex + colNumber)*size.x;
        var offsetY = 1.0f - size.y - (vIndex + rowNumber)*size.y;
        var offset = new Vector2(offsetX, offsetY);

        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
    }
}