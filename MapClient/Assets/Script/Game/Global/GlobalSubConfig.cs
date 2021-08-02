//============================================
//作 者:GK
//时 间:2016-11-04 14:30:31
//备 注:游戏配置文件级别
//公 司:杭州白掌网络科技有限公司
//============================================
using UnityEngine;

public class GlobalSubConfig
{        
    private string mResIP;//资源IP地址
    public string resIP
    {
        get
        {
            return mResIP;
        }set
        {            
            mResIP = StaticTools.Un64String(value);
        }
    }
    public string pt
    {
        get
        {
#if UNITY_ANDROID
            return "Android";
#elif UNITY_IOS
            return "IOS";
#elif UNITY_EDITOR_OSX
             return "OSX";
#else
            return "Windows";
#endif                  
        }
    }
    
    public bool is_editor { get; private set; }//是否使用本地资源

    public bool is_lua_print { get; private set; }
    public string game_url { get; set;}
    public ushort port { get; set; }
    public int LoginVersion { get; set; }
    
    public string LocalPath= Application.streamingAssetsPath+"/";
    public int LocalPathLen;    
    
    public GlobalSubConfig()
    {
        LocalPathLen = LocalPath.Length;
    }
    
    public void SetValue(bool _is_editor,string ip,bool isLuaPrint,int loginVersion)
    {
        is_lua_print = isLuaPrint;
        is_editor = _is_editor;
        resIP = ip;
        LoginVersion = loginVersion;
    }        
}