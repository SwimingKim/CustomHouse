using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainCameraMovement : MonoBehaviour
{
    // hide - top - camera - hide
    // 카메라
    // 한 손가락 클릭 - 회전
    // 두 손가락 클릭 - 줌인아웃
    // 특정 오브젝트 클릭 - 타겟 변경

    public Transform target;
    public float edgeBorder = 0.1f;
    public float horizontalSpeed = 3.0f; // 360
    public float verticalSpeed = 1.0f; // 120
    public float minVertical = 20.0f;
    public float maxVertical = 85.0f;
    private float x, y, distance = 0.0f;

    public Vector3 upPos, downPos;
    Vector3 startPos;

    float cameraValue = 1.2f;

    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    float initOri, initFind;

    float dist = 100;

    bool isCamera;

    public Vector3 offset;
    Vector3 toPos;

    void Start()
    {
        startPos = transform.eulerAngles;

        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;
        distance = (transform.position - target.position).magnitude;
        
        initFind = Camera.main.fieldOfView;
        initOri = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        if (isCamera)
        {
            CameraModeMove();
        }
    }

    IEnumerator CameraMoveCoroutine()
    {
        yield return new WaitForFixedUpdate();
        if (dist > 0.01)
        {
            CameraMove();
            StartCoroutine("CameraMoveCoroutine");
            yield return null;
        }
        else
        {
            dist = 100;

            if (CGameManager.instance.dir == CGameManager.DIRECTION.HIDE)
            {
                transform.SetPositionAndRotation(upPos, Quaternion.Euler(startPos.x, startPos.y, 0));
            }
            StopAllCoroutines();
        }
    }

    void CameraMove()
    {
        switch (CGameManager.instance.dir)
        {
            case CGameManager.DIRECTION.SHOW:
                dist = Vector3.Distance(transform.position, downPos);
                if (dist < 0.01) break;
                PosMove(downPos);
                break;
            case CGameManager.DIRECTION.HIDE:
                dist = Vector3.Distance(transform.position, upPos);
                if (dist < 0.01) break;
                SoftMove(upPos, Quaternion.Euler(startPos.x, startPos.y, 0));
                break;
            case CGameManager.DIRECTION.TOP:
                if (CGameManager.instance.nextDir == CGameManager.DIRECTION.HIDE)
                {
                    x = startPos.y;
                    y = startPos.x;

                    Camera.main.fieldOfView = initFind;
                    SoftMove(upPos, Quaternion.Euler(startPos.x, startPos.y, 0));
                    if (dist < 0.01) break;
                    dist = Vector3.Distance(transform.position, upPos);
                }
                else
                {
                    Quaternion rotation = Quaternion.Euler(y, x, 0);
                    Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distance * cameraValue)) + target.position;
                    SoftMove(position, rotation);
                }
                break;
            case CGameManager.DIRECTION.CAMERA:
                dist = 0;
                break;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    void CameraModeMove()
    {
        float dt = Time.deltaTime;

        if (Application.platform != RuntimePlatform.Android)
        {
            x -= Input.GetAxis("Horizontal") * 10f * horizontalSpeed * dt;
            y -= Input.GetAxis("Vertical") * 10f * verticalSpeed * dt;
        }
        else
        {
            x += Input.GetTouch(0).deltaPosition.x * horizontalSpeed * dt;
            y += Input.GetTouch(0).deltaPosition.y * verticalSpeed * dt;
        }
        y = ClampAngle(y, minVertical, maxVertical);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distance * cameraValue)) + target.position;
        SoftMove(position, rotation);
        dist = Vector2.Distance(transform.position, position);

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDelatMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDelatMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagintudeDiff = prevTouchDelatMag - touchDelatMag;
            if (Camera.main.orthographic)
            {
                Camera.main.orthographicSize += deltaMagintudeDiff * orthoZoomSpeed;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                Camera.main.fieldOfView += deltaMagintudeDiff * perspectiveZoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10f, 50f);
            }


        }
    }

    void PosMove(Vector3 pos)
    {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime + 0.1f);
    }

    void SoftMove(Vector3 pos, Quaternion rot)
    {
        PosMove(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime + 0.1f);
    }

    public void ChangeCameraMode(bool isCamera)
    {
        this.isCamera = isCamera;

        if (isCamera == false)
        {
            StartCoroutine("CameraMoveCoroutine");
        }
    }

    IEnumerator FollowPrefabCoroutine(Vector3 pos)
    {
        yield return new WaitForFixedUpdate();

        dist = Vector3.Distance(transform.position, pos + offset);

        if (dist >= 0.01)
        {
            PosMove(pos + offset);
            StartCoroutine("FollowPrefabCoroutine", pos);
        }
        else
        {
            dist = 100;

            StopCoroutine("FollowPrefabCoroutine");
            StartCoroutine("CameraMoveCoroutine");
        }
    }

}
