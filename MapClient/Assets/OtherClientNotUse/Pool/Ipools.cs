using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bherry
{
    public interface IPools
    {
        bool IsUsing { get; set; }
        void Reset();//重置
        void Dispose();//回收
    }
}