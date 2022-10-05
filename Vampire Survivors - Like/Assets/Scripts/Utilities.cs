using UnityEngine;

public static class Utilities
{
    public static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var angle = Vector2.Angle(v1, v2);
        if (v2.y < 0)
        {
            if (v2.x < 0)
            {
                angle += 90;
            }
            else if (v2.x > 0)
            {
                angle += 270;
            }
            else
            {
                angle += 180;
            }
        }
        return angle;
    }

    public static int IncrementInRange(ref int i, int min = 0, int max = 2)
    {
        i += 1;

        if (i == max)
        {
            i = 0;
        }

        return i;
    }
}
