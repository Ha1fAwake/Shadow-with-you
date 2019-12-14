using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class FactoryManager
{
    private static IAssetFactory mAssetFactory = null;
    private static IBrickFactory mBrickFactory = null; 

    public static IAssetFactory assetFactory
    {
        get
        {
            if(mAssetFactory == null)
            {
                mAssetFactory = new ResourceAssetFactory();
            }
            return mAssetFactory;
        }
    }
    public static IBrickFactory brickFactory
    {
        get
        {
            if (mBrickFactory == null)
            {
                mBrickFactory = new BrickFactory();
            }
            return mBrickFactory;
        }
    }
}
