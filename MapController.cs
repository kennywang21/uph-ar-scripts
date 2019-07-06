using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{

    public GameObject fromToUI;
    public GameObject btnOptionsUI;

    public GameObject resetZoomUI;
    public GameObject markerGroupUI;
    private bool isHold = false;
    private bool isMoveMap = false;

    GameObject holdBtn;
    MeshRenderer mr;

    public GameObject mainCamera;
    public GameObject arCamera;

    public static MapController instance;

    public GameObject moveBtnUI;
    public Image moveBtnImage;
    public Image moveBtnIconImage;
    public Collider moveBoxCollider;


    void Awake()
    {
        holdBtn = btnOptionsUI.transform.GetChild(0).gameObject;
        mr = GetComponent<MeshRenderer>();
        instance = this;
    }

    void Update()
    {
        if (mr.enabled && !fromToUI.activeInHierarchy)
        {
            ActiveState();
        }
        else if (!mr.enabled && fromToUI.activeInHierarchy)
        {
            NonActiveState();
        }
    }

    public bool GetIsMoveMap()
    {
        return isMoveMap;
    }

    public void SetIsMoveMap()
    {
        isMoveMap = !isMoveMap;

        if (isMoveMap)
        {
           
            moveBoxCollider.enabled = true;
        }
        else
        {
       
            moveBoxCollider.enabled = false;
        }
    }

    void ActiveState()
    {
        markerGroupUI.SetActive(false);
        SetMapIsHold(false);

        fromToUI.SetActive(true);
        btnOptionsUI.SetActive(true);
        holdBtn.SetActive(true);

        resetZoomUI.SetActive(false);
        moveBoxCollider.enabled = false;
    }

    void NonActiveState()
    {
        markerGroupUI.SetActive(true);

        fromToUI.SetActive(false);
        btnOptionsUI.SetActive(false);
        holdBtn.SetActive(false);

        moveBtnUI.SetActive(false);
        resetZoomUI.SetActive(false);
    }

    public void SetMapIsHold(bool b)
    {
        isHold = b;

        if (isHold)
        {
            mainCamera.SetActive(true);
            moveBtnUI.SetActive(true);

            holdBtn.SetActive(false);
            resetZoomUI.SetActive(false);

            arCamera.SetActive(false);
            holdBtn.SetActive(false);

            GetComponent<Renderer>().enabled = true;
            Transform imageTarget = transform.parent.GetComponent<Transform>();
            imageTarget.position = Vector3.zero;
            imageTarget.rotation = Quaternion.EulerAngles(Vector3.zero);

            Renderer[] rs = GetComponentsInChildren<Renderer>(true); 
            foreach (Renderer r in rs)
            {
                r.enabled = true;
            }

            Collider[] cols = GetComponentsInChildren<Collider>();
            foreach (var col in cols)
            {
                if (string.Equals(col.name, "map_group_e1"))
                {
                    continue;
                }

                if (string.Equals(col.name, "map_group_e2"))
                {
                    continue;
                }

                col.enabled = true;
            }

            Canvas[] cvss = GetComponentsInChildren<Canvas>();
            foreach (var cvs in cvss)
                cvs.enabled = true;
        }
        else
        {
            mainCamera.SetActive(false);
            resetZoomUI.SetActive(false);
            moveBtnUI.SetActive(false);

            holdBtn.SetActive(true);
            arCamera.SetActive(true);
        }
    }

    public bool GetMapIsHold()
    {
        return isHold;
    }
}
