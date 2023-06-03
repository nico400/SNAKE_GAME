using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    public int xSize, ySize;
    public GameObject block;
    GameObject head;
    GameObject apple;
    List<GameObject> tail;
    public Material headMaterial, tailMateria, appleMaterial;
    Vector2 dir;

    float timePassed, timeBetweenMovement;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenMovement = 0.5f;
        dir = Vector2.right;
        createPlayer();
        createGrid();
        spawnAppler();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir = Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir = Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = Vector2.right;
        }

        timePassed += Time.deltaTime;
        if (timeBetweenMovement < timePassed)
        {
            timePassed = 0;
            Vector2 positionNew = head.GetComponent<Transform>().position + new Vector3(dir.x,dir.y,0);

            //colision
            if (positionNew.x > xSize/2
                || positionNew.x < -xSize / 2
                || positionNew.y > ySize / 2
                || positionNew.y < -ySize / 2)
            {
                //game over
                Debug.Log("morreu");
            }

            if (positionNew.x == apple.transform.position.x && positionNew.y == apple.transform.position.y)
            {
                GameObject newTile = Instantiate(block);
                newTile.transform.position = apple.transform.position;
                DestroyImmediate(apple);
                head.GetComponent<SpriteRenderer>().material = tailMateria;
                tail.Add(head);
                head = newTile;
                head.GetComponent<SpriteRenderer>().material = headMaterial;
                spawnAppler();
            }

            if (tail.Count == 0)
            {
                head.transform.position = positionNew;
            }
            else
            {
                head.GetComponent<SpriteRenderer>().material = tailMateria;
                tail.Add(head);
                head = tail[0];
                head.GetComponent<SpriteRenderer>().material = headMaterial;
                tail.RemoveAt(0);
                head.transform.position = positionNew;
            }
        }
    }
    void createGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject grid = Instantiate(block) as GameObject;
                grid.transform.position = new Vector3(x - xSize/2, y - ySize / 2, 1);
            }
        }
    }
    void createPlayer()
    {
        head = Instantiate(block) as GameObject;
        head.GetComponent<SpriteRenderer>().material = headMaterial;
        tail = new List<GameObject>();
    }
    Vector2 ranPosition()
    {
        return new Vector2(Random.Range(-xSize / 2 , xSize / 2 + 1), Random.Range(-ySize / 2, ySize / 2 + 1));
    }
    bool conteinedInSkane(Vector2 pos)
    {
        bool inHead = pos.x == head.transform.position.x && pos.y == head.transform.position.y;
        bool inTail = false;
        foreach(GameObject item in tail)
        {
            if (item.transform.position.x == pos.x && item.transform.position.y == pos.y)
            {
                inTail = true;
            }
        }
        return inHead || inTail;
    }
    void spawnAppler()
    {
        Vector2 randPos = ranPosition();
        Debug.Log(randPos);
        while (conteinedInSkane(randPos))
        {
            randPos = ranPosition();
        }
        apple = Instantiate(block) as GameObject;
        apple.GetComponent<SpriteRenderer>().material = appleMaterial;
        apple.transform.position = new Vector3(randPos.x, randPos.y);
    }
}
