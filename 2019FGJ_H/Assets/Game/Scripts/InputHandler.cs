using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public int inputNum;
    public InputEvent[] moveEve;
    public PlayerControll[] _PlayerControll;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _PlayerControll.Length; i++)
        {
            moveEve[i].AddListener(_PlayerControll[i].moveCall);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <= inputNum; i++)
        {
            if(Input.GetAxisRaw("Move" + i.ToString()) > 0)
            {
                moveEve[i - 1].Invoke(0);
            }
            if (Input.GetAxisRaw("Move" + i.ToString()) < 0)
            {
                moveEve[i - 1].Invoke(1);
            }
            if (Input.GetAxisRaw("Move" + i.ToString()) == 0)
            {
                moveEve[i - 1].Invoke(4);
            }
            if (Input.GetAxisRaw("Rotate" + i.ToString()) > 0)
            {
                moveEve[i - 1].Invoke(2);
            }
            if (Input.GetAxisRaw("Rotate" + i.ToString()) < 0)
            {
                moveEve[i - 1].Invoke(3);
            }
            if (Input.GetAxisRaw("Rotate" + i.ToString()) == 0)
            {
                moveEve[i - 1].Invoke(5);
            }
        }
    }
}
public class InputEvent: UnityEvent<int> { }
