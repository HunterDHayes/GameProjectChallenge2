using UnityEngine;
using System.Collections;

public class GamestrapHelper {

    public static Color ColorRGBInt(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

}
