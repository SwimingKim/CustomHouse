using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour
{
    public static CGameManager instance = null;

    public enum DIRECTION { SHOW, HIDE, TOP, CAMERA };
    public DIRECTION dir = DIRECTION.HIDE;
    public DIRECTION nextDir;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public void ChangeDirection(DIRECTION dir)
    {
        this.dir = dir;
    }

    public void ChangeTopDirection(DIRECTION nextDir)
    {
        this.dir = DIRECTION.TOP;
        this.nextDir = nextDir;
    }

}
