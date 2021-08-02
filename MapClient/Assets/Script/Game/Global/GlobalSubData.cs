//============================================
//作 者:GK
//时 间:2016-11-04 14:35:00
//备 注:游戏中的全局数据
//公 司:杭州白掌网络科技有限公司
//============================================
using UnityEngine;
using System.Collections.Generic;


public class GlobalSubData
{
      
    public string curScene { get; private set; }
    public void SetScene(string _curScene)
    {
        curScene = _curScene;
    }

    #region 当前的区号
    public int cur_zone;
    public int main_zone;
    #endregion
    #region 当前的ClientID
    public string ClientId { get; set; }
    public string GameToken { get; set; }
    public string GamePhpTokenUrl { get; set; }

    public int GameChannelId=0;

    public GlobalSubData()
    {
#if !Sk
        ClientId = SystemInfo.deviceUniqueIdentifier;
#else
        ClientId = PlayerPrefs.GetString("_ClientId", null);
#endif
    }

    #endregion
    #region 表赋值         
    Dictionary<int, float> _MoveTime;
    public bool PlayerRoleCreated { get; set; }

    internal float GetMoveTileTime(short speed, byte dic)
    {
        if (_MoveTime == null)
        {
            _MoveTime = new Dictionary<int, float>();
        }
        int key = dic + speed;
        float result = 0;
        if (_MoveTime.TryGetValue(key,out result))
        {
            return result;
        }
        else
        {
            result = NeedTime(dic, speed);
            _MoveTime.Add(key , result);
            return result;
        }        
    }
    float NeedTime(int key, short speed)
    {
        float sum =(float)GlobalData.tile_width / speed;
        return sum;
    }


    #endregion

}
