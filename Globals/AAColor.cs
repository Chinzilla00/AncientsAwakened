using Microsoft.Xna.Framework;
using Terraria;
using BaseMod;
using System;

namespace AAMod
{
	public class AAColor
    {
        public static Color Lantern
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(224, 213, 94), new Color(130, 104, 41), new Color(224, 213, 94));
            }
        }
        
        public static Color Mushroom
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.LightCoral, Color.Coral, Color.LightCoral);
            }
        }

        public static Color Broodmother
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Orange, Color.DarkOrange, Color.Orange);
            }
        }

        public static Color Hydra
        {
            get
            {
                return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DarkSlateBlue, Color.Indigo, Color.DarkSlateBlue);
            }
        }

        public static Color Glow
        {
            get
            {
                return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.White, Color.Transparent, Color.Transparent, Color.White);
            }
        }

        public static Color Heartstone
        {
            get
            {
                return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.HotPink, Color.Transparent, Color.Transparent, Color.HotPink);
            }
        }

        public static Color Starcrystal
        {
            get
            {
                return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DeepSkyBlue, Color.Transparent, Color.Transparent, Color.DeepSkyBlue);
            }
        }

        public static Color Flash
        {
            get
            {
                return BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Transparent, Color.White, Color.Transparent, Color.White, Color.Transparent, Color.Transparent, Color.Transparent);
            }
        }

        public static Color Akuma
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(146, 37, 30), new Color(102, 20, 48), new Color(146, 37, 30));
            }
        }

        public static Color Yamata
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(53, 57, 126), new Color(38, 36, 75), new Color(53, 57, 126));
            }
        }

        public static Color AkumaA
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.LightSkyBlue, Color.DeepSkyBlue, Color.LightSkyBlue);
            }
        }

        public static Color YamataA 
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(146, 30, 68), new Color(102, 20, 80), new Color(146, 30, 68));
            }
        }

        public static Color Zero
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Red, Color.DarkRed, Color.Red);
			}
		}

		public static Color Shen
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
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.OrangeRed, Color.Indigo, Color.Indigo, Color.OrangeRed);
            }
        }

        public static Color Shen3
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.OrangeRed, Color.Magenta, Color.Indigo, Color.Indigo, Color.Magenta, Color.OrangeRed);
            }
        }

        public static Color IZ
        { 
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DarkGray, Color.Gray, Color.DarkGray);
			}
		}

        public static Color Cthulhu
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Cyan, Color.DarkCyan, Color.Cyan);
            }
        }

        public static Color Cthulhu2 
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(10, 22, 28), Color.White, new Color(10, 22, 28));
            }
        }

        public static Color Oblivion
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
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(0, 140, 128), new Color(0, 117, 128), new Color(0, 140, 128));
            }
        }

        public static Color Desert
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(227, 159, 0), new Color(176, 88, 0), new Color(227, 159, 0));
            }
        }

        public static Color Ocean
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(112, 255, 243), new Color(27, 178, 191), new Color(112, 255, 243));
            }
        }

        public static Color Hell
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(179, 75, 0), new Color(128, 43, 0), new Color(179, 75, 0));
            }
        }

        public static Color Inferno
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(214, 36, 0), new Color(153, 13, 0), new Color(214, 36, 0));
            }
        }

        public static Color Mire
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(57, 0, 156), new Color(26, 0, 128), new Color(57, 0, 156));
            }
        }

        public static Color Dungeon
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(232, 36, 102), new Color(33, 31, 77), new Color(232, 36, 102));
            }
        }

        public static Color Corruption
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(117, 0, 140), new Color(75, 0, 102), new Color(117, 0, 140));
            }
        }

        public static Color Crimson
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(140, 0, 2), new Color(102, 0, 2), new Color(140, 0, 2));
            }
        }

        public static Color Jungle
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(124, 140, 2), new Color(56, 102, 0), new Color(124, 140, 2));
            }
        }

        public static Color Sky
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(0, 138, 166), new Color(0, 99, 148), new Color(0, 138, 166));
            }
        }

        public static Color Cavern
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color(122, 47, 24), new Color(77, 22, 15), new Color(122, 47, 24));
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
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DarkGreen, Color.Green, Color.DarkGreen);
            }
        }

        public static Color Daybringer
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Turquoise, Color.DarkTurquoise, Color.Turquoise);
            }
        }

        public static Color Nightcrawler
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Blue, Color.DarkBlue, Color.Blue);
            }
        }

        public static Color Storm
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DarkViolet, Color.DarkViolet, Color.DarkViolet, Color.DarkViolet, Color.DarkViolet, Color.Violet, Color.DarkViolet, Color.Violet, Color.DarkViolet);
            }
        }
        
        public static Color Djinn
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.SandyBrown, Color.Sienna, Color.SandyBrown);
            }
        }
        
        public static Color Serpent
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.CornflowerBlue, Color.CadetBlue, Color.CornflowerBlue);
            }
        }
        
        public static Color TODE
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.DodgerBlue, Color.CornflowerBlue, Color.DodgerBlue);
            }
        }

        public static Color Jevil
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Violet, Color.ForestGreen, Color.ForestGreen, Color.Violet);
            }
        }

        public static Color CthulhuItemRarity
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.SeaGreen, Color.DarkSeaGreen, Color.SeaGreen);
            }
        }

        public static Color ZeroShield
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Red, Color.DarkRed, Color.DarkRed, Color.Red);
            }
        }

        public static Color CursedInferno
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Lime, Color.LimeGreen, Color.Lime);
            }
        }

        public static Color Ichor
        {
            get
            {
                return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Gold, Color.DarkGoldenrod, Color.Gold);
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