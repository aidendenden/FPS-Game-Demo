using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniMap : MonoBehaviour
{
    public RectTransform mapMask;//地图遮罩
    public RectTransform miniMapBg;//地图背景
    public RectTransform _playerIcon;//玩家锚点

    public Transform _player;//玩家
    public Transform max, min;//最远点和最近点
    float h, v; //地图的实际 宽 高

    private Vector3[] mapPos = new Vector3[4];
    private Vector3[] maskPos = new Vector3[4];

    Vector2 temp, temp_p, temp_m, temp_Zoom;

    private void Awake()
    {
        mapMask.GetWorldCorners(mapPos);
        miniMapBg.GetWorldCorners(maskPos);

        print(mapPos[0]); print(mapPos[1]); print(mapPos[2]); print(mapPos[3]);

        h = max.position.z - min.position.z;
        v = max.position.x - min.position.x;

    }

    private void Update()
    {
        MiniMapFunction();
    }

    public void MiniMapFunction()
    {
        IconRot();
        if (Input.GetKey(KeyCode.O))
        {
            MapZoom(5);
            PosChange();
            return;
        }
        if (Input.GetKey(KeyCode.P))
        {
            MapZoom(-5);
            PosChange();
            return;
        }


        temp_p.x = Mathf.Clamp((_player.position.x / v * miniMapBg.rect.width), -miniMapBg.rect.width / 2, miniMapBg.rect.width / 2);
        temp_p.y = Mathf.Clamp((_player.position.z / h * miniMapBg.rect.height), -miniMapBg.rect.height / 2, miniMapBg.rect.height / 2);

        //_Picon.Set((_player.position.x / v * miniMapBg.rect.width), (_player.position.z / h * miniMapBg.rect.height));
        //print(_Picon);

        temp_m.x = (-_player.position.x / v * miniMapBg.rect.width) - mapMask.rect.width / 2;
        temp_m.y = (-_player.position.z / h * miniMapBg.rect.height) - mapMask.rect.height / 2;

        //_Micon.Set((-_player.position.x / v * miniMapBg.rect.width) - mapMask.rect.width / 2, (-_player.position.z / h * miniMapBg.rect.height) - mapMask.rect.height / 2);
        //print(_Micon);


        if ((_playerIcon.localPosition.x <= ((mapMask.rect.width - miniMapBg.rect.width) / 2) && _playerIcon.localPosition.y <= ((mapMask.rect.height - miniMapBg.rect.height) / 2))
            || (_playerIcon.localPosition.x <= ((mapMask.rect.width - miniMapBg.rect.width) / 2) && _playerIcon.localPosition.y >= -((mapMask.rect.height - miniMapBg.rect.height) / 2))
            || (_playerIcon.localPosition.x >= -((mapMask.rect.width - miniMapBg.rect.width) / 2) && _playerIcon.localPosition.y <= ((mapMask.rect.height - miniMapBg.rect.height) / 2))
            || (_playerIcon.localPosition.x >= -((mapMask.rect.width - miniMapBg.rect.width) / 2) && _playerIcon.localPosition.y >= -((mapMask.rect.height - miniMapBg.rect.height) / 2)))
        {
            print("四角");
            _playerIcon.localPosition = temp_p;
        }
        else if (_playerIcon.localPosition.x <= ((mapMask.rect.width - miniMapBg.rect.width) / 2) || _playerIcon.localPosition.x >= -((mapMask.rect.width - miniMapBg.rect.width) / 2))
        {
            print("左边和右边");
            _playerIcon.localPosition = temp_p;
            temp.x = miniMapBg.localPosition.x;
            temp.y = temp_m.y;
            miniMapBg.localPosition = temp;
        }
        else if (_playerIcon.localPosition.y <= ((mapMask.rect.height - miniMapBg.rect.height) / 2) || _playerIcon.localPosition.y >= -((mapMask.rect.height - miniMapBg.rect.height) / 2))
        {
            print("上方和下方");
            _playerIcon.localPosition = temp_p;
            temp.x = temp_m.x;
            temp.y = miniMapBg.localPosition.y;
            miniMapBg.localPosition = temp;
        }
        else
        {
            print("普通");
            _playerIcon.localPosition = temp_p;
            miniMapBg.localPosition = temp_m;
        }

    }

    public void IconRot()
    {
        //print("玩家的坐标" + _player.eulerAngles);
        //print("玩家图标的坐标" + _playerIcon.rotation);
        _playerIcon.rotation = Quaternion.Euler(new Vector3(0, 0, -_player.eulerAngles.y));
    }

    public void MapZoom(float _offset)
    {
        temp_Zoom.x = Mathf.Clamp(miniMapBg.rect.width + _offset, mapMask.rect.width, mapMask.rect.width * 5);
        temp_Zoom.y = Mathf.Clamp(miniMapBg.rect.height + _offset, mapMask.rect.height, mapMask.rect.height * 5);
        miniMapBg.sizeDelta = temp_Zoom;
    }

    public void PosChange()
    {
        temp_p.x = Mathf.Clamp((_player.position.x / v * miniMapBg.rect.width), -miniMapBg.rect.width / 2, miniMapBg.rect.width / 2);
        temp_p.y = Mathf.Clamp((_player.position.z / h * miniMapBg.rect.height), -miniMapBg.rect.height / 2, miniMapBg.rect.height / 2);

        temp_m.x = (-_player.position.x / v * miniMapBg.rect.width) - mapMask.rect.width / 2;
        temp_m.y = (-_player.position.z / h * miniMapBg.rect.height) - mapMask.rect.height / 2;

        _playerIcon.localPosition = temp_p;
        






        if (miniMapBg.sizeDelta == mapMask.sizeDelta)
        {
            miniMapBg.localPosition = new Vector2( -mapMask.rect.width / 2, -mapMask.rect.height / 2);
            return;
        }
        else
        {
            miniMapBg.localPosition = temp_m;
        }
    }

   
}