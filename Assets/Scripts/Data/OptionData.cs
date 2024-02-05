using UnityEngine;
public class OptionData:MonoBehaviour, IFDropInterval
{
    private float _minoDropInterval = 1f;
    private float _moveInterval = 0.2f;
    private float _levelSpeedUpValue = 0.04f;
    private const float MINO_DROP_INTERVAL_MIN = 0.2f;
    private const float MINO_DROP_INTERVAL_START = 1f;
    
    public float GetMinoMoveInterval()
    {
        return _moveInterval;
    }

    public float GetMinoDropInterval()
    {
        return _minoDropInterval;
    }

    public float GetMinoMoveDownInterval()
    {
        return _minoDropInterval / 2;
    }

    public void LevelUp(int nowLevel)
    {
        _minoDropInterval = MINO_DROP_INTERVAL_START - _levelSpeedUpValue * nowLevel;
        if (_minoDropInterval < MINO_DROP_INTERVAL_MIN)
        {
            _minoDropInterval = MINO_DROP_INTERVAL_MIN;
        }
    }
}
