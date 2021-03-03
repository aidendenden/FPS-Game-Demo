
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [Header("地图相关坐标")]
    public Transform scene_max, scene_min;//最远点和最近点
    float scene_Long, scene_High; //地图的实际 宽 高
    Vector3 scene_Map_Point;

    [Header("地图和遮罩的Sprite")]
    public RectTransform map_Mask;//地图遮罩
    public RectTransform mini_Map;//地图背景
    Image map_Image;
    public string path;//地图图片路径和名字前缀，需要在Resources下
    public float[] floor;//每个楼层的高度，需要多加一个极限高度比如1000等

    [Header("玩家的Sprite")]
    public RectTransform _playerIcon;//玩家锚点

    [Header("缩放比例值")]
    public float zoom_Value = 5;

    public Transform _player;//玩家
    public Transform xxx;
    public RectTransform xxxIcon;

    Vector3 temp_Zoom_Value, temp_Spin_Value, temp_map_pos,
            temp_player_pos_1, temp_player_pos_2;


    private void Awake()
    {
        map_Image = mini_Map.GetComponent<Image>();

        PosInitialization(scene_max, scene_min, out scene_Long, out scene_High, out scene_Map_Point);

    }

    private void Update()
    {
        Use_Player_MiniMapFunction(_player, _playerIcon);
        //Other_Object_MiniMapFunction(xxx, xxxIcon);

        if (Input.GetKey(KeyCode.O))
        {
            MapZoom(zoom_Value);
            return;
        }
        if (Input.GetKey(KeyCode.P))
        {
            MapZoom(-zoom_Value);
            return;
        }
    }


  //玩家在地图上定位
    public void Use_Player_MiniMapFunction(Transform player, RectTransform player_Icon)
    {
        MapPosSet(scene_Long, scene_High, scene_Map_Point, player, mini_Map, map_Mask);
        IconPosSet(scene_Long, scene_High, scene_Map_Point, mini_Map, player, player_Icon);
        IconSpin(player, player_Icon);
        MapImageSwitch(floor, path, player, map_Image);
    }

    //其他物体在地图上定位
    public void Other_Object_MiniMapFunction(Transform item, RectTransform item_Icon)
    {
        IconPosSet(scene_Long, scene_High, scene_Map_Point, mini_Map, item, item_Icon);
        IconSpin(item, item_Icon);
    }

    //坐标初始化
    public virtual void PosInitialization(Transform scene_max, Transform scene_min, out float scene_Long, out float scene_High, out Vector3 scene_Map_Point)
    {
        scene_High = scene_max.localPosition.z - scene_min.localPosition.z;
        scene_Long = scene_max.localPosition.x - scene_min.localPosition.x;

            scene_Map_Point = scene_max.localPosition + scene_min.localPosition;
            scene_Map_Point = scene_Map_Point / 2;

        //scene_Map_Point = scene_max.localPosition - scene_min.localPosition;
       
        //if (scene_Map_Point.y != 0 && scene_Map_Point.x != 0 && scene_Map_Point.z != 0)
        //{
        //    print(scene_Map_Point);
        //    return;
        //}
        //else
        //{
        //    return;
        //}

    }

    //旋转
    public virtual void IconSpin(Transform player, RectTransform playerIcon)
    {
        temp_Spin_Value.x = 0;
        temp_Spin_Value.y = 0;
        temp_Spin_Value.z = -player.eulerAngles.y;
        playerIcon.rotation = Quaternion.Euler(temp_Spin_Value);
    }

    //缩放
    public virtual void MapZoom(float _offset)
    {
        temp_Zoom_Value.x = Mathf.Clamp(mini_Map.rect.width + _offset, map_Mask.rect.width, map_Mask.rect.width * 5);
        temp_Zoom_Value.y = Mathf.Clamp(mini_Map.rect.height + _offset, map_Mask.rect.height, map_Mask.rect.height * 5);
        mini_Map.sizeDelta = temp_Zoom_Value;
    }

    //图标定位
    public virtual void IconPosSet(float scene_Long, float scene_High, Vector3 scene_Map_Point, RectTransform mini_Map, Transform player, RectTransform playerIcon)
    {
        temp_player_pos_2 = player.position - scene_Map_Point;
        temp_player_pos_1.x = Mathf.Clamp((temp_player_pos_2.x / scene_Long * mini_Map.rect.width), -mini_Map.rect.width / 2, mini_Map.rect.width / 2);
        temp_player_pos_1.y = Mathf.Clamp((temp_player_pos_2.z / scene_High * mini_Map.rect.height), -mini_Map.rect.height / 2, mini_Map.rect.height / 2);
        playerIcon.localPosition = temp_player_pos_1;
        //print(playerIcon.localPosition);
    }

    //地图定位
    public virtual void MapPosSet(float scene_Long, float scene_High, Vector3 scene_Map_Point, Transform player, RectTransform mini_Map, RectTransform map_Mask)
    {
        temp_player_pos_2 = player.position - scene_Map_Point;
        temp_map_pos.x = Mathf.Clamp((-temp_player_pos_2.x / scene_Long * mini_Map.rect.width) - map_Mask.rect.width / 2, -(mini_Map.rect.width / 2), (mini_Map.rect.width / 2) - map_Mask.rect.width);
        temp_map_pos.y = Mathf.Clamp((-temp_player_pos_2.z / scene_High * mini_Map.rect.height) - map_Mask.rect.height / 2, -(mini_Map.rect.height / 2), (mini_Map.rect.height / 2) - map_Mask.rect.height);
        mini_Map.localPosition = temp_map_pos;
        //print(mini_Map.localPosition);
    }

    //地图楼层切换
    public virtual void MapImageSwitch(float[] floor, string path, Transform player, Image map_Image)
    {
        for (int i = 0; i < floor.Length - 1; i++)
        {
            if (player.position.y >= floor[i] && player.position.y <= floor[i + 1])
            {
                map_Image.sprite = Resources.Load<Sprite>(path + i.ToString());
            }
        }
    }

}