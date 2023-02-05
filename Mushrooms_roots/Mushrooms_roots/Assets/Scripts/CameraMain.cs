using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public int minX, minZ, maxX, maxZ;

    private Main _main;
    private GameObject _sector_now;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _main = GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_main.IsPaused())
        {
            MoveCamera();
            if (Input.GetMouseButtonDown(0))
            {
                RayClick();
            }
        }
    }

    private void MoveCamera()
    {
        var pos = transform.position;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        transform.Translate(Input.GetAxis("Horizontal") * speed, 0, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed);
        transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);

        if (pos.x > maxX || pos.x < minX)
            transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        if (pos.z > maxZ || pos.z < minZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, pos.z);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKey("q")) transform.RotateAround(hit.point, Vector3.up, 1);
            if (Input.GetKey("e")) transform.RotateAround(hit.point, Vector3.down, 1);
        }
    }

    private void RayClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject selectedSector = hit.collider.gameObject;
            Sector sector = selectedSector.GetComponent<Sector>();
            if (sector)
            {
                if (_sector_now)
                    _sector_now.GetComponent<Sector>().EndChoose();
                if (selectedSector == _sector_now)
                {
                    _sector_now = null;
                }
                else
                {
                    sector.Choose();
                    _sector_now = selectedSector;
                }
            }
        }
    }
}