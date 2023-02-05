using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject summer, winter;

    private int _count;
    private Main _main;
    private EventMaker _eventMaker;
    private Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
        transform.position = new Vector3(757, _position.y, _position.z);
        Debug.Log(_position.x);
        _main = Camera.main.GetComponent<Main>();
        _eventMaker = Camera.main.GetComponent<EventMaker>();
    }

    //Update is called once per frame
    void Update()
    {
        _position = transform.position;
        Debug.Log(_position.x);
        var _events = _eventMaker.GetListEvents();
        if (_events.Count > _count)
        {
            for (int i = _count; i < _events.Count; i++)
            {
                if (_events[i].Name().Equals("Summer"))
                    Instantiate(summer, new Vector3(_position.x + 50 * i, _position.y, 0), Quaternion.identity, transform);
                if (_events[i].Name().Equals("Winter"))
                    Instantiate(winter, new Vector3(_position.x + 50 * i, _position.y, 0), Quaternion.identity, transform);
            }
            _count = _events.Count;
        }
        transform.position = new Vector3(757 - ((float)_main.TimeNow() / (_main.GetTimeS() * 10000000) * 50), _position.y, _position.z);
    }
}
