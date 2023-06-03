using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public int xSize = 10, ySize = 10;
    public GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        createGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void createGrid()
    {
        for (int x = 0; x <= xSize; x++)
        {      
            for (int y = 0; y <= ySize; y++)
            {
                GameObject grid = Instantiate(block) as GameObject;
                grid.transform.position = new Vector3(x + this.transform.position.x, y + this.transform.position.y, 0);
            }
            //GameObject gridXTop = Instantiate(block) as GameObject;
            //gridXTop.transform.position = new Vector3(x + this.transform.position.x,this.transform.position.y - (-), 0);
        }
    }
}
