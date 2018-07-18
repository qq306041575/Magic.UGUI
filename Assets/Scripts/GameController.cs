using UnityEngine;
using XLua;

public class GameController : MonoBehaviour
{
    static LuaEnv _lua = null;
    float _lastGCTime = 0;

    void Awake()
    {
        if (null == _lua)
        {
            _lua = new LuaEnv();
            _lua.AddLoader((ref string filepath) =>
            {
                return Resources.Load<TextAsset>(filepath).bytes;
            });
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            enabled = false;
        }
        _lua.DoString("require('QQ306041575/LuaFacade')()");
    }

    void Update()
    {
        if (Time.time - _lastGCTime > 1)
        {
            _lua.Tick();
            _lastGCTime = Time.time;
        }
    }
}
