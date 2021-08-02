//============================================
//作 者:GK
//时 间:2016-08-31 08:57:23
//备 注:
//公 司:杭州白掌网络科技有限公司
//============================================
using FairyGUI;
using System.Collections.Generic;
public delegate void CallBackInt(int arg1);
public delegate void CallBackIntFloat(int arg1, float arg2);
public delegate void CallBackFloat2(float arg1,float arg2);
public delegate void MsgCallBack(params object[] objs);
public delegate void CallBackGObject(GObject go);
public delegate void CallBackVoid();
public delegate void CallBackString(string tex);
public delegate void CallBackObj(object obj);
public delegate void CallBackObjs(object []obj);
public delegate void CallBackBool(bool status);

