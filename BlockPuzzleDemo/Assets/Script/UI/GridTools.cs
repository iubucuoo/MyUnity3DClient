using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GridTools
{
    public static void AddGrids(Transform root,GroupBase data, Image obj)
    {
        float width = obj.rectTransform.rect.width;
        float height = obj.rectTransform.rect.height;
        int _width = 0;
        int _height = data.H_count - 1;

        var info = data.DataArray;
        for (int j = 0; j < info.Length; j++)
        {
            int _id = info[j];
            if (data.Isbg)
            {
                if (data.Grid[j].image == null)
                {
                    var bg = Object.Instantiate(obj);
                    bg.transform.parent = root;
                    bg.transform.localPosition = new Vector3((_width - data.W_count * 0.5f + 0.5f) * width, (_height - data.H_count * 0.5f + 0.5f) * height);
                    data.Grid[j].image = bg;
                }
            }
            else if(_id == 1)
            {
                var bg = Object.Instantiate(obj);
                bg.transform.parent = root;
                bg.transform.localPosition = new Vector3((_width - data.W_count * 0.5f + 0.5f) * width, (_height - data.H_count * 0.5f + 0.5f) * height);
            }
            _width++;
            if (_width == data.W_count)
            {
                _width = 0;
                _height--;
            }
        }
    }

}
