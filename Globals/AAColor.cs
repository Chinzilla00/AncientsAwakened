using Microsoft.Xna.Framework;
using Terraria;
using BaseMod;
using System;

namespace AAMod
{
	public class AAColor
    {
        public static Color Lantern //Blaze Lantern
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(224, 213, 94), new Color(177, 144, 61), new Color(224, 213, 94));
            }
        }
        public static Color Akuma //Akuma
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(146, 37, 30), new Color(102, 20, 48), new Color(146, 37, 30));
			}
		}
		public static Color Yamata //Yamata
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(53, 57, 126), new Color(38, 36, 75),  new Color(53, 57, 126));
			}
		}
		public static Color Zero //Zero
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(113, 37, 55), new Color(67, 22, 37), new Color(113, 37, 55));
			}
		}
		public static Color Shen //Shen Doragon
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Magenta, Color.DarkMagenta, Color.Magenta);
			}
		}
        public static Color Shen2
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.OrangeRed, Color.Indigo, Color.OrangeRed);
            }
        }
        public static Color Shen3
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.OrangeRed, Color.Magenta, Color.Indigo, Color.Magenta, Color.OrangeRed);
            }
        }
        public static Color IZ //Infinity Zero
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(169, 34, 73), new Color(146, 37, 50), new Color(169, 34, 73));
			}
		}
		public static Color Oblivion //Oblivion
		{
			get
			{
                float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
                float Pie = 1f * (float)Math.Sin(Eggroll);
                Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
                return color1;
            }
		}

        public static Color Snow
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.LightSeaGreen, Color.SeaGreen, Color.LightSeaGreen);
            }
        }

        public static Color Desert
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Goldenrod, Color.DarkGoldenrod, Color.Goldenrod);
            }
        }

        public static Color Ocean
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Cyan, Color.DarkCyan, Color.Cyan);
            }
        }

        public static Color Hell
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Orange, Color.DarkOrange, Color.Orange);
            }
        }

        public static Color Inferno
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.OrangeRed, Color.DarkOrange, Color.OrangeRed);
            }
        }

        public static Color Mire
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Indigo, Color.DarkBlue, Color.Indigo);
            }
        }

        public static Color Dungeon
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DarkBlue, Color.Navy, Color.DarkBlue);
            }
        }

        public static Color Corruption
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Violet, Color.DarkViolet, Color.Violet);
            }
        }

        public static Color Crimson
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Red, Color.DarkRed, Color.Red);
            }
        }

        public static Color Jungle
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Green, Color.DarkGreen, Color.Green);
            }
        }

        public static Color Sky
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.CornflowerBlue, Color.CadetBlue, Color.CornflowerBlue);
            }
        }

        public static Color Cavern
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.SaddleBrown, Color.Brown, Color.SaddleBrown);
            }
        }

        public static Color Hallow
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Cyan, Color.Pink, Color.Cyan);
            }
        }

        public static Color TerraGlow
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Yellow, Color.YellowGreen, Color.Yellow);
            }
        }

        public static Color COLOR_WHITEFADE1
		{
			get
			{
				Color c = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.White, Color.White * 0.7f, Color.White);
				c.A = 255;
				return c;
			}
		}
	}
}