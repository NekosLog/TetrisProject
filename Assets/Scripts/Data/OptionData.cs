using UnityEngine;
public class OptionData:MonoBehaviour, IFGetKeyInterval
{
    private float _minoMoveIntervalTime = 0.2f;
    public float MinoMoveInterval()
    {
        return _minoMoveIntervalTime;
    }
}
