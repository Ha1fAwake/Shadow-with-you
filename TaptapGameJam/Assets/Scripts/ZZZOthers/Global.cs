using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    /// <summary>
    /// 将0~360的角转化为-180~180的角度
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float ChangeAngle(float angle)
    {
        if (angle < 180)
        {
            return angle;
        }
        else
        {
            return angle - 540;
        }
    }

    /// <summary>
    /// 根据向量方向得到角度
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static float Vector3ToAngle(Vector3 dir)
    {
        float angle = 0;
        if (dir.x > 0)
        {
            angle = Mathf.Atan(dir.y / dir.x) * 180 / Mathf.PI - 90;
        }
        else if (dir.x < 0)
        {
            angle = Mathf.Atan(dir.y / dir.x) * 180 / Mathf.PI + 90;
        }
        else if (dir.y != 0)
        {
            angle = 90 - Mathf.Sign(dir.y) * 90;
        }
        return angle;
    }

    /// <summary>
    /// 获取随机三维向量
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="z1"></param>
    /// <param name="z2"></param>
    /// <returns></returns>
    public static Vector3 GetRandomV3(float x1,float y1,float x2, float y2, float z1, float z2)
    {
        float x = Random.Range(x1, x2);
        float y = Random.Range(y1, y2);
        float z = Random.Range(z1, z2);
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// 获取随机二维向量
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <returns></returns>
    public static Vector2 GetRandomV2(float x1, float y1, float x2, float y2)
    {
        float x = Random.Range(x1, x2);
        float y = Random.Range(y1, y2);
        return new Vector2(x, y);
    }
}
