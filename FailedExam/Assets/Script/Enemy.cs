using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] point = new GameObject[8];
    private GameObject Player;
    public GameObject Bullet;
    private int action, rand = 0;
    public float speed = 1f;
    private float timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Player.transform.position.x, Player.transform.position.y)) < 10f)
        {
            action = 1;

        }
        else {
            action = 0;
        }
            
        if(action == 0)
        {
            if (point[rand].transform.parent != null)
            {
                for(int i = 0; i < point.Length; i++)
                {
                    point[i].transform.parent = null;
                }
            }
            if (transform.position != point[rand].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, point[rand].transform.position, speed * Time.deltaTime);
            }
            else
            {
                rand = Random.Range(0, 8);
            }
        }
        else if(action == 1)
        {
            if (point[rand].transform.parent == null)
            {
                for(int i = 0; i < point.Length; i++)
                {
                    point[i].transform.parent = transform;
                }
            }
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Player.transform.position.x, Player.transform.position.y)) < 3f)  
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position * -1, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }
            if (timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 2;
                Instantiate(Bullet, transform.position, Quaternion.identity);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(action == 0)
        {
            rand = Random.Range(0, 8);
        }
    }
}
