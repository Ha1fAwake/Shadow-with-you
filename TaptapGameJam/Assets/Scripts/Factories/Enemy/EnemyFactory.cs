using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BrickFactory : IBrickFactory
{
    public BrickBase CreateBrick(int type,Vector3 pos,Vector3 rotation,Vector2 scale)
    {
        BrickBase unit = null;

        //创建敌人Prefab
        switch((BrickType)type)
        {
            case BrickType.standard:
                break;
            case BrickType.wall:
                break;
            case BrickType.exit:
                break;
            case BrickType.spike:
                break;
            default:
                Debug.Log("错误的方块类型");
                break;
        }
        GameObject brickGo = FactoryManager.assetFactory.LoadBrick(type);
        brickGo.transform.position = pos;
        brickGo.transform.rotation = Quaternion.Euler(rotation);
        brickGo.transform.localScale = new Vector3(scale.x, scale.y, 1);
        unit = brickGo.GetComponent<BrickBase>();
        return unit;
    }
}
