using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static int GetCameraDiscreteLimit(Camera camera, Direction side)
    {
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;
        int dir = (int) side;
        if (side == Direction.LEFT || side == Direction.RIGHT)
        {
            return (int) (dir * halfWidth + camera.transform.position.x);
        }
        else if (side == Direction.DOWN || side == Direction.UP)
        {
            return (int) (halfHeight + camera.transform.position.y);
        }
        return 0;
    }

}
