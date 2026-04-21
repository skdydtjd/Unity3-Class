using System;
using System.Collections.Generic;
using UnityEngine;

// 스프라이트 카테고리 구분
public enum SpriteCategory
{
    None,
    Backgrounds,
    Characters,
    Enemies,
    Tiles
}

[Serializable]
public class SpriteData : IValidation
{
    public int id;              // 고유 ID
    public string name;        // 스프라이트 이름 (혹은 리소스 경로)
    public SpriteCategory category; // 분류 (Background, Character 등)
    public string resourcePath; // Resources 폴더 내의 상세 경로

    // 실제 스프라이트 객체 (JSON에는 없지만 로드 후 캐싱용)
    private Sprite _sprite;
    public Sprite Sprite
    {
        get
        {
            if (_sprite == null)
                _sprite = ResourceManager.Instance.Get<Sprite>(resourcePath);
            return _sprite;
        }
    }

    public bool Validate()
    {
        // ID가 음수거나 경로가 비어있으면 유효하지 않음
        if (id < 0 || string.IsNullOrEmpty(resourcePath))
            return false;
        return true;
    }

    [Serializable]
    public class SpriteDataLoader : IDataLoader<int, SpriteData>
    {
        // JSON의 배열 필드 이름과 일치해야 함 (예: { "sprites": [...] })
        public List<SpriteData> sprites = new List<SpriteData>();

        public Dictionary<int, SpriteData> MakeDict()
        {
            Dictionary<int, SpriteData> dict = new Dictionary<int, SpriteData>();
            foreach (SpriteData data in sprites)
            {
                dict.Add(data.id, data);
            }
            return dict;
        }
    }
}
