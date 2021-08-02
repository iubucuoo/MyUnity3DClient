//============================================
//作 者:GK
//时 间:2016-08-31 09:29:31
//备 注:全局数据
//公 司:杭州白掌网络科技有限公司
//============================================
using UnityEngine;
using System.Collections.Generic;
using System;
//using CustomDataStruct;
using FairyGUI;
//using LuaInterface;

public class GlobalData
{
    #region 定义
    static GlobalData _inst;
    internal static GlobalData inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new GlobalData();
                _inst._Data = new GlobalSubData();
                _inst._Config = new GlobalSubConfig();
                _inst._Params = new GlobalParams();
            }
            return _inst;
        }
    }
    GlobalData()
    {
#if UNITY_EDITOR
        //guo bag dizhi
#endif
        CommitURL = StaticTools.Un64String("aHR0cDovL2h0LndqbGoueHlvdTY2Ni5jb20vL2tpbmdhcGk=");
    }
    internal GlobalSubData _Data;
    internal GlobalSubConfig _Config;
    internal GlobalParams _Params;
    #endregion
    internal const byte rongcuo = 34;
    public static float DoubleClickTime = 0.35f;
    public static Vector2 Zero = -Vector2.one * 5000;
    internal static Vector3 ZeroPoint = Vector3.zero;
    public static int _MiniMapLayer = -10000;
    public static string wg_url;
    public static ushort wg_prot;
    public static int _MyPEntityId
    {
        get
        {
            return inst._Params.myPlayerEntityId;
        }
        set
        {
            inst._Params.myPlayerEntityId = value;
        }
    }
    public static int _MyHEntityId
    {
        get
        {
            return inst._Params.myHeroEntityId;
        }
        set
        {
            inst._Params.myHeroEntityId = value;
        }
    }

    public static bool IsInSafeArea
    {
        get
        {
            return inst._Params.IsInSafeArea;
        }
        set
        {
            inst._Params.IsInSafeArea = value;
        }
    }
    internal static float splity
    {
        get
        {
            return inst._Params.splity;
        }
    }
    internal static byte tile_width
    {
        get
        {
            return GlobalParams.tile_width;
        }
    }

    public static byte isDungeon;    
    public static bool is_touch_skill
    {
        get
        {
            return inst._Params.is_touch_skill;
        }
        set
        {
            inst._Params.is_touch_skill = value;
        }
    }
    internal static short[] touch_tile = new short[2];
    public static int cid
    {
        get
        {
            return inst._Params.cid;
        }
        set
        {
            inst._Params.cid = value;
        }
    }
    public static int sid
    {
        get
        {
            return inst._Params.sid;
        }
        set
        {
            inst._Params.sid = value;
            SetBuglyUserId();
        }
    }
    //public static int InPD
    //{
    //    get
    //    {
    //        return UnitsManager.Inst.InPD();
    //    }
    //}
    internal static byte tile_height
    {
        get
        {
            return GlobalParams.tile_height;
        }
    }
    internal static ushort img_width
    {
        get
        {
            return GlobalParams.img_width;
        }
    }
    internal static ushort img_height
    {
        get
        {
            return GlobalParams.img_height;
        }
    }
    internal static byte tile_helf_width
    {
        get
        {
            return GlobalParams.tile_helf_width;
        }
    }
    internal static byte tile_helf_height
    {
        get
        {
            return GlobalParams.tile_helf_height;
        }
    }
    //[NoToLua]
    public static byte big_width
    {
        get
        {
            return GlobalParams.big_width;
        }
    }
    //[NoToLua]
    public static byte big_height
    {
        get
        {
            return GlobalParams.big_height;
        }
    }

    public static string GameUrl
    {
        get
        {
            if (wg_prot != 0)
            {
                return wg_url;
            }
            return inst._Config.game_url;
        }
        set
        {
            inst._Config.game_url = value;
        }
    }

    internal volatile static bool IsLoadMapRD;
 
    internal static float map_world_height
    {
        get
        {
            return inst._Params.world_heigth;
        }
    }
    internal static string LocalPath
    {
        get
        {
            return _inst._Config.LocalPath;
        }
    }
    internal static int LocalPathLen
    {
        get
        {
            return _inst._Config.LocalPathLen;
        }
    }

    internal const int SidMultiple = 10000;
    internal static string GetEquipRes(byte act, sbyte dir, out byte key)
    {

        key = (byte)(dir + act * 10);
        if (act <= 6)
        {
            act -= 1;
        }
        else if (act == 9)
        {
            act = 6;
        }
        else if (act == 13)
        {
            act = 7;
        }
        else if (act == 21)
        {
            act = 8;
        }
        else if (act == 22)
        {
            act = 9;
        }
        dir -= 1;

        return inst._Params.EquipRes[act, dir];
    }

    #region 是否Editor模式
    public static bool IsEditor
    {
        get
        {
            return inst._Config.is_editor;
        }
    }
    #endregion

    #region 资源路径    

    public static string RESIP_CONFIG
    {
        get
        {
            System.Random random = new System.Random((int)DateTime.Now.Ticks);
            return string.Concat(inst._Config.resIP, "?v=", (random.Next(0, 999999)));
        }
    }
    #endregion
    #region 平台
    public static string PT
    {
        get
        {
            return inst._Config.pt;
        }
    }
    #endregion
   
    public static void SetResConfig(bool _isUsedLocal, string ip, bool isLuaPrint, int loginVersion = 0)
    {
        inst._Config.SetValue(_isUsedLocal, ip, isLuaPrint, loginVersion);
    }
    public static void Set_timp_time(object timp_time)
    {
        double _lt = Convert.ToDouble(timp_time);
        TimeMgr.Instance.SetTimesTamp(_lt);
    }
    internal static float GetMoveTileTime(short speed, byte dic)
    {
        return inst._Data.GetMoveTileTime(speed, dic);
    }
    public static bool IsLuaPrint
    {
        get
        {
            return inst._Config.is_lua_print;
        }
    }
    public static int LoginVersion
    {
        get
        {
            return inst._Config.LoginVersion;
        }
        internal set
        {
            inst._Config.LoginVersion = value;
        }
    }
    public static string mobile_type = "";
    public static string ClientID
    {
        get
        {
            return inst._Data.ClientId;
        }
        set
        {
            inst._Data.ClientId = value;
            SetBuglyUserId();
            PlayerPrefs.SetString("_ClientId", value);
        }
    }
    static void SetBuglyUserId()
    {
#if UNITY_ANDROID
        BuglyAgent.SetUserId("cid=" + ClientID + ";v=" + NewArtMgr.inst.versionNum + "-(" + MainScripts.inst.version + ");sid=" + sid);
#endif
    }
    public static int main_zone
    {
        get
        {
            return inst._Data.main_zone;
        }
        set
        {
            inst._Data.main_zone = value;
        }
    }

    internal static string loginStr;
    public static int cur_zone
    {
        get
        {
            return inst._Data.cur_zone;
        }
        set
        {
            inst._Data.cur_zone = value;
        }
    }

    public static string sk_zone
    {
        get
        {
            return "连击" + cur_zone;
        }
    }

    internal static string zone_name = "glz_zone";
    public static string GameToken
    {
        get
        {
            return inst._Data.GameToken;
        }
        set
        {
            if (value == null && wg_prot != 0)
            {
                //跨服活动出来 过场中断线 token 不清空
            }
            else
            {
                inst._Data.GameToken = value;
            }
        }
    }
    internal static string GamePhpTokenUrl
    {
        get
        {
            return inst._Data.GamePhpTokenUrl;
        }
        set
        {
            inst._Data.GamePhpTokenUrl = value;
        }
    }
    public static int GameChannelId
    {
        get
        {
            return inst._Data.GameChannelId;
        }
        set
        {
            inst._Data.GameChannelId = value;
        }
    }
    public static string prefix;//1个前缀对应多个渠道
    public static bool PlayerRoleCreated
    {
        get
        {
            return inst._Data.PlayerRoleCreated;
        }
        set
        {
            inst._Data.PlayerRoleCreated = value;
        }
    }
    public static ushort port
    {
        get
        {
            if (wg_prot != 0)
            {
                return wg_prot;
            }
            return inst._Config.port;
        }
        set
        {
            inst._Config.port = value;
        }
    }
    public static bool logout;

    public static bool IsIphoneX = false;
    public static string AIRoot;
    public static int isAutoFight = 0;
    public static int ToAngleMax = 0;
    static bool _OpenMain = false;

    public static Vector2 TransPosFitIphoneX(Vector2 Old_Pos)//适配类似iphonex这种左右特殊裁切情况下input类中坐标偏差
    {
        if (IsIphoneX == false)
        {
            return Old_Pos;
        }
        Vector2 Pos = new Vector2();
        Pos.x = Old_Pos.x - (Screen.width - Screen_width) * 0.5f;
        Pos.y = Old_Pos.y;
        return Pos;
    }
    public static float FguiscaleFactor
    {
        get { return UIContentScaler.scaleFactor; } 
    }
    
    internal static int talknpcid;
    internal static int talkKuangid;
    internal static int talkMonsterId;
    internal static int[] talkMs = new int[3];
    private static ushort downLoadProgress;//下载进度
    private static bool allowDownLoad;//是否允许流量下载
    internal static int SoundIndex = 4;
    public static ushort DownLoadProgress
    {
        get { return downLoadProgress; }
        set { downLoadProgress = value; }
    }

    public static bool AllowDownLoad
    {
        get { return allowDownLoad; }
        set { allowDownLoad = value; }
    }


    public static volatile byte _NetStatus = 2;

    static int _pkModel;
   
    //是否是防沉迷用户
    public static bool isAdult = false;

    //当日在线时长 (秒)
    public static float onlineTime = 0;

    //防沉迷限制等级  1. 游客状态，2. 年龄8周岁一下，3. 年龄16周岁一下 ，4. 年龄18周岁一下
    public static float addictLevel = 0;
    
    internal static int footNum = 8000000;
    internal static ushort UnitUIKey;

    internal static float _JoystickDistance;

    internal static bool _MoveStatus
    {
        get { return _JoystickDistance == 0 || _JoystickDistance > 33; }
    }
  
    internal static ushort dsxy01;
    internal static ushort dsxy02;
    internal static ushort zsxy01;
    internal static ushort zsxy02;
    internal static ushort fsxy01;
    internal static ushort fsxy02;
    internal static ushort Defult_MAN;
    internal static ushort FS_MFD_GROUND;
    internal static ushort FS_MFD_BLOW;
    internal static ushort Defult_WONMAN;
    internal static ushort Defult_Monster;
    internal static ushort Defult_HairMan;
    internal static ushort Defult_HairWoman;
    internal static ushort Defult_JgMB;
    internal static ushort hzc101;
    internal static ushort Wk_ssd;
    public static int client_version;
    public static int JoyStickState=0;//摇杆状态，0动态摇杆，1固定摇杆
    public static int TouchMove;
    internal static float radian = 180 / Mathf.PI;
    static bool _islogin;
   
    internal static bool InChoseServer = false;
    public static int Screen_width;
    public static int Screen_height;
    public static int IOSReview = 0;
    public static int _SmallMapFindPath;

    internal static string CommitURL;
    internal static bool InIOSPkg = true;//隐藏登录界面火的特效    
    public static string CreateTime;//创角时间     
    internal const string params_a = "a";
 

}


