﻿#pragma warning disable RECS0018 // 等値演算子による浮動小数点値の比較

using DotFeather.Helpers;
namespace DotFeather.Drawable
{
	/// <summary>
	/// テクスチャを描画します。
	/// </summary>
	public class Sprite : IDrawable
	{
		public Texture2D Texture { get; }

		public Vector Location { get; }

		public float Angle { get; }

		public Vector Scale { get; }

		public int ZOrder { get; set; }
		public string Name { get; set; }

		public Sprite(Texture2D texture, int x, int y, float angle = default, Vector scale = default)
		{
			Texture = texture;
			Location = new Vector(x, y);
			Angle = angle;
			Scale = scale != default ? scale : new Vector(1, 1);
		}

		public void Draw(GameBase game)
		{
			TextureDrawer.Draw(Texture, Location, Scale, Angle);
		}
	}
}