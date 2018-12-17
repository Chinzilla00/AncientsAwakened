using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.Localization;
using BaseMod;
using Terraria.GameContent;

namespace AAMod
{
	public class DustHandler
	{
		public static List<Action> spawnDustSoon = new List<Action>();
		public static void OnUpdate()
		{
			foreach(Action action in spawnDustSoon)
			{
				action();
			}
			spawnDustSoon.Clear();
		}
		public static void SpawnDustASAP(Action action)
		{
			spawnDustSoon.Add(action);
		}
			
		public static int QuickSpecialDust(Mod mod, Vector2 position, Color? color = null, float scale = 1f, Func<GDust, GDust> gdustVar = null)
		{
			Color drawColor = (color != null ? (Color)color : Color.Red);
			Color lightColor = (color != null ? (Color)color : Color.Red);
			int dustID = SpawnSpecialDust(position, 1, 1, default(Vector2), scale, mod.GetTexture("Dusts/dust_Rainbow"), lightColor, drawColor, 12, 1, gdustVar);
			Main.dust[dustID].position = position;
			Main.dust[dustID].velocity = default(Vector2);
			Main.dust[dustID].color = drawColor;
			return dustID;
		}
		
        

		public static int SpawnSpecialDust(Entity entity, float scale = 1.2f, Texture2D tex = null, Color? color = null, int overrideAI = -1, int dusttype = 39, Func<GDust, GDust> gdustVar = null)
		{
			return SpawnSpecialDust(entity.position, entity.width, entity.height, entity.velocity, scale, tex, color, null, overrideAI, dusttype);
		}		
		
        public static int SpawnSpecialDust(Vector2 position, int width, int height, Vector2 velocity, float scale = 1.2f, Texture2D tex = null, Color? color = null, int overrideAI = -1, int dusttype = 39, Func<GDust, GDust> gdustVar = null)
        {
			return SpawnSpecialDust(position, width, height, velocity, scale, tex, color, null, overrideAI, dusttype);
		}

        public static int SpawnSpecialDust(Vector2 position, int width, int height, Vector2 velocity, float scale = 1.2f, Texture2D tex = null, Color? color = null, Color? drawColor = null, int overrideAI = -1, int dusttype = 39, Func<GDust, GDust> gdustVar = null)
        {
            try
            {
                if (Main.netMode == 2 || Main.dedServ) { return 0; }
                if (overrideAI == 8)
                {
                    scale += MathHelper.Lerp(-0.2f, 0.2f, (float)Main.rand.NextDouble());
                }
				int dustID = 0;
				if(width <= 1 && height <= 1)
				{
					dustID = Dust.NewDustPerfect(position, AAMod.instance.DustType("Omnidust"), velocity * 0.2f, 0, Color.White, scale).dustIndex;
				}else
				{
					dustID = Dust.NewDust(position, width, height, AAMod.instance.DustType("Omnidust"), velocity.X * 0.2f, velocity.Y * 0.2f, 0, Color.White, scale);
				}
				if(dustID == 0 || dustID == Main.dust.Length - 1) return dustID;				
                Main.dust[dustID].customData = new GDust(dusttype, tex, color, overrideAI);
                Dust dust = Main.dust[dustID];
				if(dustID != 5999 && dust.active) ModDust.GetDust(dust.type).OnSpawn(dust); //call it again!
                GDust gdust = (GDust)(dust.customData);
				if(drawColor != null) gdust.overrideDrawColor = (Color)drawColor;
                dust.noGravity = true;
                if (tex != null)
                {
                    dust.frame = new Rectangle(0, 0, tex.Width, tex.Height);
                    if (overrideAI == 7)
                    {
                        dust.frame = BaseMod.BaseDrawing.GetFrame(Main.rand.Next(3), 32, 30, 0, 2);
                    }
                }
                if (overrideAI != -1)
                {
                    object[] data = (color != null ? new object[] { (byte)overrideAI, (short)dustID, color } : new object[] { (byte)overrideAI, (short)dustID });
                    if (overrideAI == 8)
                    {
                        data = new object[] { (byte)overrideAI, (short)dustID, color, 0f, 0f, 0f, 0f };
                        data[3] = (float)Main.rand.NextDouble() * 80f;
                        data[4] = Main.rand.Next(2) == 0 ? 0.2f : 0.3f;
                        data[5] = (float)Main.rand.Next(2);
                        data[6] = 0f;
                        gdust.overrideTex = AAMod.instance.GetTexture("dust_Petal" + ((float)data[4] == 1f ? "" : "2"));
                    }else
					if (overrideAI == 10 || overrideAI == 11 || overrideAI == 18)
                    {
                        data = new object[] { (byte)overrideAI, (short)dustID, color, 0f, 0f, 0f, 0f };
                        data[3] = (float)0f;
                    }else
					if (overrideAI == 16)
					{
                        data = new object[] { (byte)overrideAI, (short)dustID, color, 0f, 0f, 0f, 0f };	
						data[5] = dust.scale;
					}
                    gdust.customData = data;
                    gdust.overrideUpdate = Omnidust.DustAI;
                }
				if(overrideAI == 13) dust.alpha = 254;
				if(gdustVar != null) gdust = gdustVar(gdust);
                dust.customData = gdust;
                Main.dust[dustID] = dust;
                return dustID;
            }
            catch (Exception e) { BaseMod.BaseUtility.Chat("DUST SPAWN ERROR: " + e.Message, Color.Red, true); ErrorLogger.Log(e.Message); ErrorLogger.Log(e.StackTrace); ErrorLogger.Log("-------"); }
            return 0;
        }

		//OLD SPAWN CODE
      /*  public static int SpawnSpecialDust_OLD(Vector2 position, int width, int height, Vector2 velocity, float scale = 1.2f, Texture2D tex = null, Color? color = null, int overrideAI = -1, int dusttype = 39)
		{
			if (Main.netMode == 2 || Main.dedServ){ return 0; } //TODO: HANDLE!
			if (overrideAI == 8) 
			{
				scale += MathHelper.Lerp(-0.2f, 0.2f, (float)Main.rand.NextDouble()); 
			}
			int dustID = Dust.NewDust(position, width, height, dusttype, velocity.X * 0.2f, velocity.Y * 0.2f, 100, Color.White, scale);
			Main.dust[dustID].noGravity = true;
			if (tex != null)
			{
				Main.dust[dustID].OverrideTexture = tex;
				Main.dust[dustID].frame = new Rectangle(0, 0, tex.Width, tex.Height);
				if (overrideAI == 3 || overrideAI == 4)
				{
					Main.dust[dustID].frame = BaseMod.BaseDrawing.GetFrame((overrideAI == 4 && Main.dust[dustID].scale < 0.6f ? 0 : overrideAI - 3), 12, 12, 0, 2);
				}
				if (overrideAI == 7)
				{
					Main.dust[dustID].frame = BaseMod.BaseDrawing.GetFrame(Main.rand.Next(3), 32, 30, 0, 2);
				}
			}
			if (overrideAI != -1)
			{
				object[] data = (color != null ? new object[] { (byte)overrideAI, (short)dustID, color } : new object[] { (byte)overrideAI, (short)dustID });
				if(overrideAI == 8)
				{
					data = new object[] { (byte)overrideAI, (short)dustID, color, 0f, 0f, 0f, 0f };
					data[3] = (float)Main.rand.NextDouble() * 80f;
					data[4] = Main.rand.Next(2) == 0 ? 0.2f : 0.3f;
					data[5] = (float)Main.rand.Next(2);
					data[6] = 0f;
					Main.dust[dustID].OverrideTexture = GRealm.GetTexture("dust_Petal" + ((float)data[4] == 1f ? "" : "2"));
				}
				Main.dust[dustID].customData = data;
				Main.dust[dustID].OverrideUpdate = DustAI;
			}
			return dustID;
		}
       */		
	}

    public class AADust
    {
        public int fakeType = 0;
        public Texture2D overrideTex = null;
        public Color? overrideColor = null, overrideDrawColor = null;
        public Func<Dust, object[], object[]> overrideUpdate = null;
        public int overrideAI = -1;
        public object[] customData = null;

        public AADust(int fType, Texture2D tex = null, Color? color = null, int ai = -1)
        {
            fakeType = fType;
            overrideTex = tex;
            overrideColor = color;
            overrideAI = ai;
        }

        #region VanillaDustInit
        public static void VanillaDustInit(Dust dust)
        {
            dust.fadeIn = 0f;
            dust.active = true;
            dust.noGravity = false;
            dust.frame.X = 10 * dust.type;
            dust.frame.Y = 10 * Main.rand.Next(3);
            dust.shader = null;
            int j = dust.type;
            while (j >= 100)
            {
                j -= 100;
                Dust expr_2FA_cp_0 = dust;
                expr_2FA_cp_0.frame.X = expr_2FA_cp_0.frame.X - 1000;
                Dust expr_312_cp_0 = dust;
                expr_312_cp_0.frame.Y = expr_312_cp_0.frame.Y + 30;
            }
            dust.frame.Width = 8;
            dust.frame.Height = 8;
            dust.rotation = 0f;
            dust.noLight = false;
            dust.firstFrame = true;
            if (dust.type == 228 || dust.type == 269 || dust.type == 135 || dust.type == 6 || dust.type == 242 || dust.type == 75 || dust.type == 169 || dust.type == 29 || (dust.type >= 59 && dust.type <= 65) || dust.type == 158)
            {
                dust.velocity.Y = (float)Main.rand.Next(-10, 6) * 0.1f;
                Dust expr_43F_cp_0 = dust;
                expr_43F_cp_0.velocity.X = expr_43F_cp_0.velocity.X * 0.3f;
                dust.scale *= 0.7f;
            }
            if (dust.type == 127 || dust.type == 187)
            {
                dust.velocity *= 0.3f;
                dust.scale *= 0.7f;
            }
            if (dust.type == 33 || dust.type == 52 || dust.type == 266 || dust.type == 98 || dust.type == 99 || dust.type == 100 || dust.type == 101 || dust.type == 102 || dust.type == 103 || dust.type == 104 || dust.type == 105)
            {
                dust.alpha = 170;
                dust.velocity *= 0.5f;
                Dust expr_54C_cp_0 = dust;
                expr_54C_cp_0.velocity.Y = expr_54C_cp_0.velocity.Y + 1f;
            }
            if (dust.type == 41)
            {
                dust.velocity *= 0f;
            }
            if (dust.type == 80)
            {
                dust.alpha = 50;
            }
            if (dust.type != 34 && dust.type != 35 && dust.type != 152)
            {
                return;
            }
            dust.velocity *= 0.1f;
            dust.velocity.Y = -0.5f;
            if (dust.type == 34 && !Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
            {
                dust.active = false;
                return;
            }
        }
        #endregion

        #region VanillaDustAI (oooh boy.)
        public static void VanillaDustAI(Dust dust)
        {
            int num = 0;
            bool flag = Sandstorm.Happening && Main.player[Main.myPlayer].ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
            if (dust.active)
            {
                if (dust.scale > 10f)
                {
                    dust.active = false;
                }
                if (dust.firstFrame && !ChildSafety.Disabled && ChildSafety.DangerousDust(dust.type))
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        dust.firstFrame = false;
                        dust.type = 16;
                        dust.scale = Main.rand.NextFloat() * 1.6f + 0.3f;
                        dust.color = Color.Transparent;
                        dust.frame.X = 10 * dust.type;
                        dust.frame.Y = 10 * Main.rand.Next(3);
                        dust.shader = null;
                        dust.customData = null;
                        int num2 = dust.type / 100;
                        Dust expr_14D_cp_0 = dust;
                        expr_14D_cp_0.frame.X = expr_14D_cp_0.frame.X - 1000 * num2;
                        Dust expr_167_cp_0 = dust;
                        expr_167_cp_0.frame.Y = expr_167_cp_0.frame.Y + 30 * num2;
                        dust.noGravity = true;
                    }
                    else
                    {
                        dust.active = false;
                    }
                }
                #region huge-ass AI clusterfuck
                if (dust.type == 35)
                {
                    Dust.lavaBubbles++;
                }
                dust.position += dust.velocity;
                if (dust.type == 258)
                {
                    dust.noGravity = true;
                    dust.scale += 0.015f;
                }
                if (dust.type >= 86 && dust.type <= 92 && !dust.noLight)
                {
                    float num3 = dust.scale * 0.6f;
                    if (num3 > 1f)
                    {
                        num3 = 1f;
                    }
                    int num4 = dust.type - 85;
                    float num5 = num3;
                    float num6 = num3;
                    float num7 = num3;
                    if (num4 == 3)
                    {
                        num5 *= 0f;
                        num6 *= 0.1f;
                        num7 *= 1.3f;
                    }
                    else if (num4 == 5)
                    {
                        num5 *= 1f;
                        num6 *= 0.1f;
                        num7 *= 0.1f;
                    }
                    else if (num4 == 4)
                    {
                        num5 *= 0f;
                        num6 *= 1f;
                        num7 *= 0.1f;
                    }
                    else if (num4 == 1)
                    {
                        num5 *= 0.9f;
                        num6 *= 0f;
                        num7 *= 0.9f;
                    }
                    else if (num4 == 6)
                    {
                        num5 *= 1.3f;
                        num6 *= 1.3f;
                        num7 *= 1.3f;
                    }
                    else if (num4 == 2)
                    {
                        num5 *= 0.9f;
                        num6 *= 0.9f;
                        num7 *= 0f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num3 * num5, num3 * num6, num3 * num7);
                }
                if (dust.type >= 86 && dust.type <= 92)
                {
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Player player = (Player)dust.customData;
                        dust.position += player.position - player.oldPosition;
                    }
                    else if (dust.customData != null && dust.customData is Projectile)
                    {
                        Projectile projectile = (Projectile)dust.customData;
                        if (projectile.active)
                        {
                            dust.position += projectile.position - projectile.oldPosition;
                        }
                    }
                }
                if (dust.type == 262 && !dust.noLight)
                {
                    Vector3 rgb = new Vector3(0.9f, 0.6f, 0f) * dust.scale * 0.6f;
                    Lighting.AddLight(dust.position, rgb);
                }
                if (dust.type == 240 && dust.customData != null && dust.customData is Projectile)
                {
                    Projectile projectile2 = (Projectile)dust.customData;
                    if (projectile2.active)
                    {
                        dust.position += projectile2.position - projectile2.oldPosition;
                    }
                }
                if ((dust.type == 259 || dust.type == 6 || dust.type == 158) && dust.customData != null && dust.customData is int)
                {
                    if ((int)dust.customData == 0)
                    {
                        if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                        {
                            dust.scale *= 0.9f;
                            dust.velocity *= 0.25f;
                        }
                    }
                    else if ((int)dust.customData == 1)
                    {
                        dust.scale *= 0.98f;
                        Dust expr_585_cp_0 = dust;
                        expr_585_cp_0.velocity.Y = expr_585_cp_0.velocity.Y * 0.98f;
                        if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                        {
                            dust.scale *= 0.9f;
                            dust.velocity *= 0.25f;
                        }
                    }
                }
                if (dust.type == 263 || dust.type == 264)
                {
                    if (!dust.noLight)
                    {
                        Vector3 rgb2 = dust.color.ToVector3() * dust.scale * 0.4f;
                        Lighting.AddLight(dust.position, rgb2);
                    }
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Player player2 = (Player)dust.customData;
                        dust.position += player2.position - player2.oldPosition;
                        dust.customData = null;
                    }
                    else if (dust.customData != null && dust.customData is Projectile)
                    {
                        Projectile projectile3 = (Projectile)dust.customData;
                        dust.position += projectile3.position - projectile3.oldPosition;
                    }
                }
                if (dust.type == 230)
                {
                    float num8 = dust.scale * 0.6f;
                    float num9 = num8;
                    float num10 = num8;
                    float num11 = num8;
                    num9 *= 0.5f;
                    num10 *= 0.9f;
                    num11 *= 1f;
                    dust.scale += 0.02f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num8 * num9, num8 * num10, num8 * num11);
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Vector2 center = ((Player)dust.customData).Center;
                        Vector2 value = dust.position;
                        Vector2 vector = value - center;
                        float num12 = vector.Length();
                        vector /= num12;
                        dust.scale = Math.Min(dust.scale, num12 / 24f - 1f);
                        dust.velocity -= vector * (100f / Math.Max(50f, num12));
                    }
                }
                if (dust.type == 154 || dust.type == 218)
                {
                    dust.rotation += dust.velocity.X * 0.3f;
                    dust.scale -= 0.03f;
                }
                if (dust.type == 172)
                {
                    float num13 = dust.scale * 0.5f;
                    if (num13 > 1f)
                    {
                        num13 = 1f;
                    }
                    float num14 = num13;
                    float num15 = num13;
                    float num16 = num13;
                    num14 *= 0f;
                    num15 *= 0.25f;
                    num16 *= 1f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num13 * num14, num13 * num15, num13 * num16);
                }
                if (dust.type == 182)
                {
                    dust.rotation += 1f;
                    if (!dust.noLight)
                    {
                        float num17 = dust.scale * 0.25f;
                        if (num17 > 1f)
                        {
                            num17 = 1f;
                        }
                        float num18 = num17;
                        float num19 = num17;
                        float num20 = num17;
                        num18 *= 1f;
                        num19 *= 0.2f;
                        num20 *= 0.1f;
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num17 * num18, num17 * num19, num17 * num20);
                    }
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Player player3 = (Player)dust.customData;
                        dust.position += player3.position - player3.oldPosition;
                        dust.customData = null;
                    }
                }
                if (dust.type == 261)
                {
                    if (!dust.noLight)
                    {
                        float num21 = dust.scale * 0.3f;
                        if (num21 > 1f)
                        {
                            num21 = 1f;
                        }
                        Lighting.AddLight(dust.position, new Vector3(0.4f, 0.6f, 0.7f) * num21);
                    }
                    if (dust.noGravity)
                    {
                        dust.velocity *= 0.93f;
                        if (dust.fadeIn == 0f)
                        {
                            dust.scale += 0.0025f;
                        }
                    }
                    dust.velocity *= new Vector2(0.97f, 0.99f);
                    dust.scale -= 0.0025f;
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Player player4 = (Player)dust.customData;
                        dust.position += player4.position - player4.oldPosition;
                    }
                }
                if (dust.type == 254)
                {
                    float num22 = dust.scale * 0.35f;
                    if (num22 > 1f)
                    {
                        num22 = 1f;
                    }
                    float num23 = num22;
                    float num24 = num22;
                    float num25 = num22;
                    num23 *= 0.9f;
                    num24 *= 0.1f;
                    num25 *= 0.75f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num22 * num23, num22 * num24, num22 * num25);
                }
                if (dust.type == 255)
                {
                    float num26 = dust.scale * 0.25f;
                    if (num26 > 1f)
                    {
                        num26 = 1f;
                    }
                    float num27 = num26;
                    float num28 = num26;
                    float num29 = num26;
                    num27 *= 0.9f;
                    num28 *= 0.1f;
                    num29 *= 0.75f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num26 * num27, num26 * num28, num26 * num29);
                }
                if (dust.type == 211 && dust.noLight && Collision.SolidCollision(dust.position, 4, 4))
                {
                    dust.active = false;
                }
                if (dust.type == 213 || dust.type == 260)
                {
                    dust.rotation = 0f;
                    float num30 = dust.scale / 2.5f * 0.2f;
                    Vector3 value2 = Vector3.Zero;
                    int num31 = dust.type;
                    if (num31 != 213)
                    {
                        if (num31 == 260)
                        {
                            value2 = new Vector3(255f, 48f, 48f);
                        }
                    }
                    else
                    {
                        value2 = new Vector3(255f, 217f, 48f);
                    }
                    value2 /= 255f;
                    if (num30 > 1f)
                    {
                        num30 = 1f;
                    }
                    value2 *= num30;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), value2.X, value2.Y, value2.Z);
                }
                if (dust.type == 157)
                {
                    float num32 = dust.scale * 0.2f;
                    float num33 = num32;
                    float num34 = num32;
                    float num35 = num32;
                    num33 *= 0.25f;
                    num34 *= 1f;
                    num35 *= 0.5f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num32 * num33, num32 * num34, num32 * num35);
                }
                if (dust.type == 206)
                {
                    dust.scale -= 0.1f;
                    float num36 = dust.scale * 0.4f;
                    float num37 = num36;
                    float num38 = num36;
                    float num39 = num36;
                    num37 *= 0.1f;
                    num38 *= 0.6f;
                    num39 *= 1f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num36 * num37, num36 * num38, num36 * num39);
                }
                if (dust.type == 163)
                {
                    float num40 = dust.scale * 0.25f;
                    float num41 = num40;
                    float num42 = num40;
                    float num43 = num40;
                    num41 *= 0.25f;
                    num42 *= 1f;
                    num43 *= 0.05f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num40 * num41, num40 * num42, num40 * num43);
                }
                if (dust.type == 205)
                {
                    float num44 = dust.scale * 0.25f;
                    float num45 = num44;
                    float num46 = num44;
                    float num47 = num44;
                    num45 *= 1f;
                    num46 *= 0.05f;
                    num47 *= 1f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num44 * num45, num44 * num46, num44 * num47);
                }
                if (dust.type == 170)
                {
                    float num48 = dust.scale * 0.5f;
                    float num49 = num48;
                    float num50 = num48;
                    float num51 = num48;
                    num49 *= 1f;
                    num50 *= 1f;
                    num51 *= 0.05f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num48 * num49, num48 * num50, num48 * num51);
                }
                if (dust.type == 156)
                {
                    float num52 = dust.scale * 0.6f;
                    int arg_FD3_0 = dust.type;
                    float num53 = num52;
                    float num54 = num52;
                    float num55 = num52;
                    num53 *= 0.5f;
                    num54 *= 0.9f;
                    num55 *= 1f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num52 * num53, num52 * num54, num52 * num55);
                }
                if (dust.type == 234)
                {
                    float num56 = dust.scale * 0.6f;
                    int arg_1057_0 = dust.type;
                    float num57 = num56;
                    float num58 = num56;
                    float num59 = num56;
                    num57 *= 0.95f;
                    num58 *= 0.65f;
                    num59 *= 1.3f;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num56 * num57, num56 * num58, num56 * num59);
                }
                if (dust.type == 175)
                {
                    dust.scale -= 0.05f;
                }
                if (dust.type == 174)
                {
                    dust.scale -= 0.01f;
                    float num60 = dust.scale * 1f;
                    if (num60 > 0.6f)
                    {
                        num60 = 0.6f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num60, num60 * 0.4f, 0f);
                }
                if (dust.type == 235)
                {
                    Vector2 value3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    value3.Normalize();
                    value3 *= 15f;
                    dust.scale -= 0.01f;
                }
                else if (dust.type == 228 || dust.type == 229 || dust.type == 6 || dust.type == 242 || dust.type == 135 || dust.type == 127 || dust.type == 187 || dust.type == 75 || dust.type == 169 || dust.type == 29 || (dust.type >= 59 && dust.type <= 65) || dust.type == 158)
                {
                    if (!dust.noGravity)
                    {
                        Dust expr_1256_cp_0 = dust;
                        expr_1256_cp_0.velocity.Y = expr_1256_cp_0.velocity.Y + 0.05f;
                    }
                    if (dust.type == 229 || dust.type == 228)
                    {
                        if (dust.customData != null && dust.customData is NPC)
                        {
                            NPC nPC = (NPC)dust.customData;
                            dust.position += nPC.position - nPC.oldPos[1];
                        }
                        else if (dust.customData != null && dust.customData is Player)
                        {
                            Player player5 = (Player)dust.customData;
                            dust.position += player5.position - player5.oldPosition;
                        }
                        else if (dust.customData != null && dust.customData is Vector2)
                        {
                            Vector2 vector2 = (Vector2)dust.customData - dust.position;
                            if (vector2 != Vector2.Zero)
                            {
                                vector2.Normalize();
                            }
                            dust.velocity = (dust.velocity * 4f + vector2 * dust.velocity.Length()) / 5f;
                        }
                    }
                    if (!dust.noLight)
                    {
                        float num61 = dust.scale * 1.4f;
                        if (dust.type == 29)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.1f, num61 * 0.4f, num61);
                        }
                        else if (dust.type == 75)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.7f, num61, num61 * 0.2f);
                        }
                        else if (dust.type == 169)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 1.1f, num61 * 1.1f, num61 * 0.2f);
                        }
                        else if (dust.type == 135)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.2f, num61 * 0.7f, num61);
                        }
                        else if (dust.type == 158)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 1f, num61 * 0.5f, 0f);
                        }
                        else if (dust.type == 228)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.7f, num61 * 0.65f, num61 * 0.3f);
                        }
                        else if (dust.type == 229)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.3f, num61 * 0.65f, num61 * 0.7f);
                        }
                        else if (dust.type == 242)
                        {
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61, 0f, num61);
                        }
                        else if (dust.type >= 59 && dust.type <= 65)
                        {
                            if (num61 > 0.8f)
                            {
                                num61 = 0.8f;
                            }
                            int num62 = dust.type - 58;
                            float num63 = 1f;
                            float num64 = 1f;
                            float num65 = 1f;
                            if (num62 == 1)
                            {
                                num63 = 0f;
                                num64 = 0.1f;
                                num65 = 1.3f;
                            }
                            else if (num62 == 2)
                            {
                                num63 = 1f;
                                num64 = 0.1f;
                                num65 = 0.1f;
                            }
                            else if (num62 == 3)
                            {
                                num63 = 0f;
                                num64 = 1f;
                                num65 = 0.1f;
                            }
                            else if (num62 == 4)
                            {
                                num63 = 0.9f;
                                num64 = 0f;
                                num65 = 0.9f;
                            }
                            else if (num62 == 5)
                            {
                                num63 = 1.3f;
                                num64 = 1.3f;
                                num65 = 1.3f;
                            }
                            else if (num62 == 6)
                            {
                                num63 = 0.9f;
                                num64 = 0.9f;
                                num65 = 0f;
                            }
                            else if (num62 == 7)
                            {
                                num63 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
                                num64 = 0.3f;
                                num65 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * num63, num61 * num64, num61 * num65);
                        }
                        else if (dust.type == 127)
                        {
                            num61 *= 1.3f;
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61, num61 * 0.45f, num61 * 0.2f);
                        }
                        else if (dust.type == 187)
                        {
                            num61 *= 1.3f;
                            if (num61 > 1f)
                            {
                                num61 = 1f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61 * 0.2f, num61 * 0.45f, num61);
                        }
                        else
                        {
                            if (num61 > 0.6f)
                            {
                                num61 = 0.6f;
                            }
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num61, num61 * 0.65f, num61 * 0.4f);
                        }
                    }
                }
                else if (dust.type == 269)
                {
                    if (!dust.noLight)
                    {
                        float num66 = dust.scale * 1.4f;
                        if (num66 > 1f)
                        {
                            num66 = 1f;
                        }
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num66 * 0.7f, num66 * 0.65f, num66 * 0.3f);
                    }
                    if (dust.customData != null && dust.customData is Vector2)
                    {
                        Vector2 vector3 = (Vector2)dust.customData - dust.position;
                        Dust expr_19F2_cp_0 = dust;
                        expr_19F2_cp_0.velocity.X = expr_19F2_cp_0.velocity.X + 1f * (float)Math.Sign(vector3.X) * dust.scale;
                    }
                }
                else if (dust.type == 159)
                {
                    float num67 = dust.scale * 1.3f;
                    if (num67 > 1f)
                    {
                        num67 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num67, num67, num67 * 0.1f);
                    if (dust.noGravity)
                    {
                        if (dust.scale < 0.7f)
                        {
                            dust.velocity *= 1.075f;
                        }
                        else if (Main.rand.Next(2) == 0)
                        {
                            dust.velocity *= -0.95f;
                        }
                        else
                        {
                            dust.velocity *= 1.05f;
                        }
                        dust.scale -= 0.03f;
                    }
                    else
                    {
                        dust.scale += 0.005f;
                        dust.velocity *= 0.9f;
                        Dust expr_1B2D_cp_0 = dust;
                        expr_1B2D_cp_0.velocity.X = expr_1B2D_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
                        Dust expr_1B54_cp_0 = dust;
                        expr_1B54_cp_0.velocity.Y = expr_1B54_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
                        if (Main.rand.Next(5) == 0)
                        {
                            int num68 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num68].noGravity = true;
                            Main.dust[num68].scale = dust.scale * 2.5f;
                        }
                    }
                }
                else if (dust.type == 164)
                {
                    float num69 = dust.scale;
                    if (num69 > 1f)
                    {
                        num69 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num69, num69 * 0.1f, num69 * 0.8f);
                    if (dust.noGravity)
                    {
                        if (dust.scale < 0.7f)
                        {
                            dust.velocity *= 1.075f;
                        }
                        else if (Main.rand.Next(2) == 0)
                        {
                            dust.velocity *= -0.95f;
                        }
                        else
                        {
                            dust.velocity *= 1.05f;
                        }
                        dust.scale -= 0.03f;
                    }
                    else
                    {
                        dust.scale -= 0.005f;
                        dust.velocity *= 0.9f;
                        Dust expr_1CF0_cp_0 = dust;
                        expr_1CF0_cp_0.velocity.X = expr_1CF0_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
                        Dust expr_1D17_cp_0 = dust;
                        expr_1D17_cp_0.velocity.Y = expr_1D17_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
                        if (Main.rand.Next(5) == 0)
                        {
                            int num70 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num70].noGravity = true;
                            Main.dust[num70].scale = dust.scale * 2.5f;
                        }
                    }
                }
                else if (dust.type == 173)
                {
                    float num71 = dust.scale;
                    if (num71 > 1f)
                    {
                        num71 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num71 * 0.4f, num71 * 0.1f, num71);
                    if (dust.noGravity)
                    {
                        dust.velocity *= 0.8f;
                        Dust expr_1E2A_cp_0 = dust;
                        expr_1E2A_cp_0.velocity.X = expr_1E2A_cp_0.velocity.X + (float)Main.rand.Next(-20, 21) * 0.01f;
                        Dust expr_1E51_cp_0 = dust;
                        expr_1E51_cp_0.velocity.Y = expr_1E51_cp_0.velocity.Y + (float)Main.rand.Next(-20, 21) * 0.01f;
                        dust.scale -= 0.01f;
                    }
                    else
                    {
                        dust.scale -= 0.015f;
                        dust.velocity *= 0.8f;
                        Dust expr_1EB7_cp_0 = dust;
                        expr_1EB7_cp_0.velocity.X = expr_1EB7_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.005f;
                        Dust expr_1EDE_cp_0 = dust;
                        expr_1EDE_cp_0.velocity.Y = expr_1EDE_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.005f;
                        if (Main.rand.Next(10) == 10)
                        {
                            int num72 = Dust.NewDust(dust.position, 4, 4, dust.type, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num72].noGravity = true;
                            Main.dust[num72].scale = dust.scale;
                        }
                    }
                }
                else if (dust.type == 184)
                {
                    if (!dust.noGravity)
                    {
                        dust.velocity *= 0f;
                        dust.scale -= 0.01f;
                    }
                }
                else if (dust.type == 160 || dust.type == 162)
                {
                    float num73 = dust.scale * 1.3f;
                    if (num73 > 1f)
                    {
                        num73 = 1f;
                    }
                    if (dust.type == 162)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num73, num73 * 0.7f, num73 * 0.1f);
                    }
                    else
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num73 * 0.1f, num73, num73);
                    }
                    if (dust.noGravity)
                    {
                        dust.velocity *= 0.8f;
                        Dust expr_208A_cp_0 = dust;
                        expr_208A_cp_0.velocity.X = expr_208A_cp_0.velocity.X + (float)Main.rand.Next(-20, 21) * 0.04f;
                        Dust expr_20B1_cp_0 = dust;
                        expr_20B1_cp_0.velocity.Y = expr_20B1_cp_0.velocity.Y + (float)Main.rand.Next(-20, 21) * 0.04f;
                        dust.scale -= 0.1f;
                    }
                    else
                    {
                        dust.scale -= 0.1f;
                        Dust expr_2101_cp_0 = dust;
                        expr_2101_cp_0.velocity.X = expr_2101_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
                        Dust expr_2128_cp_0 = dust;
                        expr_2128_cp_0.velocity.Y = expr_2128_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
                        if ((double)dust.scale > 0.3 && Main.rand.Next(50) == 0)
                        {
                            int num74 = Dust.NewDust(new Vector2(dust.position.X - 4f, dust.position.Y - 4f), 1, 1, dust.type, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num74].noGravity = true;
                            Main.dust[num74].scale = dust.scale * 1.5f;
                        }
                    }
                }
                else if (dust.type == 168)
                {
                    float num75 = dust.scale * 0.8f;
                    if ((double)num75 > 0.55)
                    {
                        num75 = 0.55f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num75, 0f, num75 * 0.8f);
                    dust.scale += 0.03f;
                    Dust expr_226E_cp_0 = dust;
                    expr_226E_cp_0.velocity.X = expr_226E_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.02f;
                    Dust expr_2295_cp_0 = dust;
                    expr_2295_cp_0.velocity.Y = expr_2295_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.02f;
                    dust.velocity *= 0.99f;
                }
                else if (dust.type >= 139 && dust.type < 143)
                {
                    Dust expr_22F7_cp_0 = dust;
                    expr_22F7_cp_0.velocity.X = expr_22F7_cp_0.velocity.X * 0.98f;
                    Dust expr_230E_cp_0 = dust;
                    expr_230E_cp_0.velocity.Y = expr_230E_cp_0.velocity.Y * 0.98f;
                    if (dust.velocity.Y < 1f)
                    {
                        Dust expr_2337_cp_0 = dust;
                        expr_2337_cp_0.velocity.Y = expr_2337_cp_0.velocity.Y + 0.05f;
                    }
                    dust.scale += 0.009f;
                    dust.rotation -= dust.velocity.X * 0.4f;
                    if (dust.velocity.X > 0f)
                    {
                        dust.rotation += 0.005f;
                    }
                    else
                    {
                        dust.rotation -= 0.005f;
                    }
                }
                else if (dust.type == 14 || dust.type == 16 || dust.type == 31 || dust.type == 46 || dust.type == 124 || dust.type == 186 || dust.type == 188)
                {
                    Dust expr_240D_cp_0 = dust;
                    expr_240D_cp_0.velocity.Y = expr_240D_cp_0.velocity.Y * 0.98f;
                    Dust expr_2424_cp_0 = dust;
                    expr_2424_cp_0.velocity.X = expr_2424_cp_0.velocity.X * 0.98f;
                    if (dust.type == 31 && dust.noGravity)
                    {
                        dust.velocity *= 1.02f;
                        dust.scale += 0.02f;
                        dust.alpha += 4;
                        if (dust.alpha > 255)
                        {
                            dust.scale = 0.0001f;
                            dust.alpha = 255;
                        }
                    }
                }
                else if (dust.type == 32)
                {
                    dust.scale -= 0.01f;
                    Dust expr_24D0_cp_0 = dust;
                    expr_24D0_cp_0.velocity.X = expr_24D0_cp_0.velocity.X * 0.96f;
                    if (!dust.noGravity)
                    {
                        Dust expr_24F2_cp_0 = dust;
                        expr_24F2_cp_0.velocity.Y = expr_24F2_cp_0.velocity.Y + 0.1f;
                    }
                }
                else if (dust.type >= 244 && dust.type <= 247)
                {
                    dust.rotation += 0.1f * dust.scale;
                    Color color = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
                    byte b = (byte)((color.R + color.G + color.B) / 3);
                    float num76 = ((float)b / 270f + 1f) / 2f;
                    float num77 = ((float)b / 270f + 1f) / 2f;
                    float num78 = ((float)b / 270f + 1f) / 2f;
                    num76 *= dust.scale * 0.9f;
                    num77 *= dust.scale * 0.9f;
                    num78 *= dust.scale * 0.9f;
                    if (dust.alpha < 255)
                    {
                        dust.scale += 0.09f;
                        if (dust.scale >= 1f)
                        {
                            dust.scale = 1f;
                            dust.alpha = 255;
                        }
                    }
                    else
                    {
                        if ((double)dust.scale < 0.8)
                        {
                            dust.scale -= 0.01f;
                        }
                        if ((double)dust.scale < 0.5)
                        {
                            dust.scale -= 0.01f;
                        }
                    }
                    float num79 = 1f;
                    if (dust.type == 244)
                    {
                        num76 *= 0.8862745f;
                        num77 *= 0.4627451f;
                        num78 *= 0.298039228f;
                        num79 = 0.9f;
                    }
                    else if (dust.type == 245)
                    {
                        num76 *= 0.5137255f;
                        num77 *= 0.6745098f;
                        num78 *= 0.6784314f;
                        num79 = 1f;
                    }
                    else if (dust.type == 246)
                    {
                        num76 *= 0.8f;
                        num77 *= 0.709803939f;
                        num78 *= 0.282352954f;
                        num79 = 1.1f;
                    }
                    else if (dust.type == 247)
                    {
                        num76 *= 0.6f;
                        num77 *= 0.6745098f;
                        num78 *= 0.7254902f;
                        num79 = 1.2f;
                    }
                    num76 *= num79;
                    num77 *= num79;
                    num78 *= num79;
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num76, num77, num78);
                }
                else if (dust.type == 43)
                {
                    dust.rotation += 0.1f * dust.scale;
                    Color color2 = Lighting.GetColor((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));
                    float num80 = (float)color2.R / 270f;
                    float num81 = (float)color2.G / 270f;
                    float num82 = (float)color2.B / 270f;
                    float num83 = (float)(dust.color.R / 255);
                    float num84 = (float)(dust.color.G / 255);
                    float num85 = (float)(dust.color.B / 255);
                    num80 *= dust.scale * 1.07f * num83;
                    num81 *= dust.scale * 1.07f * num84;
                    num82 *= dust.scale * 1.07f * num85;
                    if (dust.alpha < 255)
                    {
                        dust.scale += 0.09f;
                        if (dust.scale >= 1f)
                        {
                            dust.scale = 1f;
                            dust.alpha = 255;
                        }
                    }
                    else
                    {
                        if ((double)dust.scale < 0.8)
                        {
                            dust.scale -= 0.01f;
                        }
                        if ((double)dust.scale < 0.5)
                        {
                            dust.scale -= 0.01f;
                        }
                    }
                    if ((double)num80 < 0.05 && (double)num81 < 0.05 && (double)num82 < 0.05)
                    {
                        dust.active = false;
                    }
                    else
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num80, num81, num82);
                    }
                }
                else if (dust.type == 15 || dust.type == 57 || dust.type == 58)
                {
                    Dust expr_29C3_cp_0 = dust;
                    expr_29C3_cp_0.velocity.Y = expr_29C3_cp_0.velocity.Y * 0.98f;
                    Dust expr_29DA_cp_0 = dust;
                    expr_29DA_cp_0.velocity.X = expr_29DA_cp_0.velocity.X * 0.98f;
                    float num86 = dust.scale;
                    if (dust.type != 15)
                    {
                        num86 = dust.scale * 0.8f;
                    }
                    if (dust.noLight)
                    {
                        dust.velocity *= 0.95f;
                    }
                    if (num86 > 1f)
                    {
                        num86 = 1f;
                    }
                    if (dust.type == 15)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num86 * 0.45f, num86 * 0.55f, num86);
                    }
                    else if (dust.type == 57)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num86 * 0.95f, num86 * 0.95f, num86 * 0.45f);
                    }
                    else if (dust.type == 58)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num86, num86 * 0.55f, num86 * 0.75f);
                    }
                }
                else if (dust.type == 204)
                {
                    if (dust.fadeIn > dust.scale)
                    {
                        dust.scale += 0.02f;
                    }
                    else
                    {
                        dust.scale -= 0.02f;
                    }
                    dust.velocity *= 0.95f;
                }
                else if (dust.type == 110)
                {
                    float num87 = dust.scale * 0.1f;
                    if (num87 > 1f)
                    {
                        num87 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num87 * 0.2f, num87, num87 * 0.5f);
                }
                else if (dust.type == 111)
                {
                    float num88 = dust.scale * 0.125f;
                    if (num88 > 1f)
                    {
                        num88 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num88 * 0.2f, num88 * 0.7f, num88);
                }
                else if (dust.type == 112)
                {
                    float num89 = dust.scale * 0.1f;
                    if (num89 > 1f)
                    {
                        num89 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num89 * 0.8f, num89 * 0.2f, num89 * 0.8f);
                }
                else if (dust.type == 113)
                {
                    float num90 = dust.scale * 0.1f;
                    if (num90 > 1f)
                    {
                        num90 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num90 * 0.2f, num90 * 0.3f, num90 * 1.3f);
                }
                else if (dust.type == 114)
                {
                    float num91 = dust.scale * 0.1f;
                    if (num91 > 1f)
                    {
                        num91 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num91 * 1.2f, num91 * 0.5f, num91 * 0.4f);
                }
                else if (dust.type == 66)
                {
                    if (dust.velocity.X < 0f)
                    {
                        dust.rotation -= 1f;
                    }
                    else
                    {
                        dust.rotation += 1f;
                    }
                    Dust expr_2DE1_cp_0 = dust;
                    expr_2DE1_cp_0.velocity.Y = expr_2DE1_cp_0.velocity.Y * 0.98f;
                    Dust expr_2DF8_cp_0 = dust;
                    expr_2DF8_cp_0.velocity.X = expr_2DF8_cp_0.velocity.X * 0.98f;
                    dust.scale += 0.02f;
                    float num92 = dust.scale;
                    if (dust.type != 15)
                    {
                        num92 = dust.scale * 0.8f;
                    }
                    if (num92 > 1f)
                    {
                        num92 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num92 * ((float)dust.color.R / 255f), num92 * ((float)dust.color.G / 255f), num92 * ((float)dust.color.B / 255f));
                }
                else if (dust.type == 267)
                {
                    if (dust.velocity.X < 0f)
                    {
                        dust.rotation -= 1f;
                    }
                    else
                    {
                        dust.rotation += 1f;
                    }
                    Dust expr_2F06_cp_0 = dust;
                    expr_2F06_cp_0.velocity.Y = expr_2F06_cp_0.velocity.Y * 0.98f;
                    Dust expr_2F1D_cp_0 = dust;
                    expr_2F1D_cp_0.velocity.X = expr_2F1D_cp_0.velocity.X * 0.98f;
                    dust.scale += 0.02f;
                    float num93 = dust.scale * 0.8f;
                    if (num93 > 1f)
                    {
                        num93 = 1f;
                    }
                    if (dust.noLight)
                    {
                        dust.noLight = false;
                    }
                    if (!dust.noLight)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num93 * ((float)dust.color.R / 255f), num93 * ((float)dust.color.G / 255f), num93 * ((float)dust.color.B / 255f));
                    }
                }
                else if (dust.type == 20 || dust.type == 21 || dust.type == 231)
                {
                    dust.scale += 0.005f;
                    Dust expr_3021_cp_0 = dust;
                    expr_3021_cp_0.velocity.Y = expr_3021_cp_0.velocity.Y * 0.94f;
                    Dust expr_3038_cp_0 = dust;
                    expr_3038_cp_0.velocity.X = expr_3038_cp_0.velocity.X * 0.94f;
                    float num94 = dust.scale * 0.8f;
                    if (num94 > 1f)
                    {
                        num94 = 1f;
                    }
                    if (dust.type == 21)
                    {
                        num94 = dust.scale * 0.4f;
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94 * 0.8f, num94 * 0.3f, num94);
                    }
                    else if (dust.type == 231)
                    {
                        num94 = dust.scale * 0.4f;
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94, num94 * 0.5f, num94 * 0.3f);
                    }
                    else
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94 * 0.3f, num94 * 0.6f, num94);
                    }
                }
                else if (dust.type == 27 || dust.type == 45)
                {
                    if (dust.type == 27 && dust.fadeIn >= 100f)
                    {
                        if ((double)dust.scale >= 1.5)
                        {
                            dust.scale -= 0.01f;
                        }
                        else
                        {
                            dust.scale -= 0.05f;
                        }
                        if ((double)dust.scale <= 0.5)
                        {
                            dust.scale -= 0.05f;
                        }
                        if ((double)dust.scale <= 0.25)
                        {
                            dust.scale -= 0.05f;
                        }
                    }
                    dust.velocity *= 0.94f;
                    dust.scale += 0.002f;
                    float num95 = dust.scale;
                    if (dust.noLight)
                    {
                        num95 *= 0.1f;
                        dust.scale -= 0.06f;
                        if (dust.scale < 1f)
                        {
                            dust.scale -= 0.06f;
                        }
                        if (Main.player[Main.myPlayer].wet)
                        {
                            dust.position += Main.player[Main.myPlayer].velocity * 0.5f;
                        }
                        else
                        {
                            dust.position += Main.player[Main.myPlayer].velocity;
                        }
                    }
                    if (num95 > 1f)
                    {
                        num95 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num95 * 0.6f, num95 * 0.2f, num95);
                }
                else if (dust.type == 55 || dust.type == 56 || dust.type == 73 || dust.type == 74)
                {
                    dust.velocity *= 0.98f;
                    float num96 = dust.scale * 0.8f;
                    if (dust.type == 55)
                    {
                        if (num96 > 1f)
                        {
                            num96 = 1f;
                        }
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num96, num96, num96 * 0.6f);
                    }
                    else if (dust.type == 73)
                    {
                        if (num96 > 1f)
                        {
                            num96 = 1f;
                        }
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num96, num96 * 0.35f, num96 * 0.5f);
                    }
                    else if (dust.type == 74)
                    {
                        if (num96 > 1f)
                        {
                            num96 = 1f;
                        }
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num96 * 0.35f, num96, num96 * 0.5f);
                    }
                    else
                    {
                        num96 = dust.scale * 1.2f;
                        if (num96 > 1f)
                        {
                            num96 = 1f;
                        }
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num96 * 0.35f, num96 * 0.5f, num96);
                    }
                }
                else if (dust.type == 71 || dust.type == 72)
                {
                    dust.velocity *= 0.98f;
                    float num97 = dust.scale;
                    if (num97 > 1f)
                    {
                        num97 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num97 * 0.2f, 0f, num97 * 0.1f);
                }
                else if (dust.type == 76)
                {
                    Main.snowDust++;
                    dust.scale += 0.009f;
                    float y = Main.player[Main.myPlayer].velocity.Y;
                    if (y > 0f && dust.fadeIn == 0f && dust.velocity.Y < y)
                    {
                        dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y, 0.04f);
                    }
                    if (!dust.noLight && y > 0f)
                    {
                        Dust expr_360E_cp_0 = dust;
                        expr_360E_cp_0.position.Y = expr_360E_cp_0.position.Y + Main.player[Main.myPlayer].velocity.Y * 0.2f;
                    }
                    if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                    {
                        dust.scale *= 0.9f;
                        dust.velocity *= 0.25f;
                    }
                }
                else if (dust.type == 268)
                {
                    Dust.SandStormCount++;
                    dust.velocity *= 1.00502515f;
                    dust.scale += 0.01f;
                    if (!flag)
                    {
                        dust.scale -= 0.05f;
                    }
                    dust.rotation = 0f;
                    float y2 = Main.player[Main.myPlayer].velocity.Y;
                    if (y2 > 0f && dust.fadeIn == 0f && dust.velocity.Y < y2)
                    {
                        dust.velocity.Y = MathHelper.Lerp(dust.velocity.Y, y2, 0.04f);
                    }
                    if (!dust.noLight && y2 > 0f)
                    {
                        Dust expr_3773_cp_0 = dust;
                        expr_3773_cp_0.position.Y = expr_3773_cp_0.position.Y + y2 * 0.2f;
                    }
                    if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
                    {
                        dust.scale *= 0.9f;
                        dust.velocity *= 0.25f;
                    }
                    else
                    {
                        dust.velocity.Y = (float)Math.Sin((double)(dust.position.X * 0.00439822953f)) * 2f;
                        Dust expr_3815_cp_0 = dust;
                        expr_3815_cp_0.velocity.Y = expr_3815_cp_0.velocity.Y + 3f;
                    }
                }
                else if (!dust.noGravity && dust.type != 41 && dust.type != 44)
                {
                    if (dust.type == 107)
                    {
                        dust.velocity *= 0.9f;
                    }
                    else
                    {
                        Dust expr_386C_cp_0 = dust;
                        expr_386C_cp_0.velocity.Y = expr_386C_cp_0.velocity.Y + 0.1f;
                    }
                }
                if (dust.type == 5 && dust.noGravity)
                {
                    dust.scale -= 0.04f;
                }
                if (dust.type == 33 || dust.type == 52 || dust.type == 266 || dust.type == 98 || dust.type == 99 || dust.type == 100 || dust.type == 101 || dust.type == 102 || dust.type == 103 || dust.type == 104 || dust.type == 105 || dust.type == 123)
                {
                    if (dust.velocity.X == 0f)
                    {
                        if (Collision.SolidCollision(dust.position, 2, 2))
                        {
                            dust.scale = 0f;
                        }
                        dust.rotation += 0.5f;
                        dust.scale -= 0.01f;
                    }
                    bool flag2 = Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y), 4, 4);
                    if (flag2)
                    {
                        dust.alpha += 20;
                        dust.scale -= 0.1f;
                    }
                    dust.alpha += 2;
                    dust.scale -= 0.005f;
                    if (dust.alpha > 255)
                    {
                        dust.scale = 0f;
                    }
                    if (dust.velocity.Y > 4f)
                    {
                        dust.velocity.Y = 4f;
                    }
                    if (dust.noGravity)
                    {
                        if (dust.velocity.X < 0f)
                        {
                            dust.rotation -= 0.2f;
                        }
                        else
                        {
                            dust.rotation += 0.2f;
                        }
                        dust.scale += 0.03f;
                        Dust expr_3A69_cp_0 = dust;
                        expr_3A69_cp_0.velocity.X = expr_3A69_cp_0.velocity.X * 1.05f;
                        Dust expr_3A80_cp_0 = dust;
                        expr_3A80_cp_0.velocity.Y = expr_3A80_cp_0.velocity.Y + 0.15f;
                    }
                }
                if (dust.type == 35 && dust.noGravity)
                {
                    dust.scale += 0.03f;
                    if (dust.scale < 1f)
                    {
                        Dust expr_3ACE_cp_0 = dust;
                        expr_3ACE_cp_0.velocity.Y = expr_3ACE_cp_0.velocity.Y + 0.075f;
                    }
                    Dust expr_3AE5_cp_0 = dust;
                    expr_3AE5_cp_0.velocity.X = expr_3AE5_cp_0.velocity.X * 1.08f;
                    if (dust.velocity.X > 0f)
                    {
                        dust.rotation += 0.01f;
                    }
                    else
                    {
                        dust.rotation -= 0.01f;
                    }
                    float num98 = dust.scale * 0.6f;
                    if (num98 > 1f)
                    {
                        num98 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f + 1f), num98, num98 * 0.3f, num98 * 0.1f);
                }
                else if (dust.type == 152 && dust.noGravity)
                {
                    dust.scale += 0.03f;
                    if (dust.scale < 1f)
                    {
                        Dust expr_3BD2_cp_0 = dust;
                        expr_3BD2_cp_0.velocity.Y = expr_3BD2_cp_0.velocity.Y + 0.075f;
                    }
                    Dust expr_3BE9_cp_0 = dust;
                    expr_3BE9_cp_0.velocity.X = expr_3BE9_cp_0.velocity.X * 1.08f;
                    if (dust.velocity.X > 0f)
                    {
                        dust.rotation += 0.01f;
                    }
                    else
                    {
                        dust.rotation -= 0.01f;
                    }
                }
                else if (dust.type == 67 || dust.type == 92)
                {
                    float num99 = dust.scale;
                    if (num99 > 1f)
                    {
                        num99 = 1f;
                    }
                    if (dust.noLight)
                    {
                        num99 *= 0.1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), 0f, num99 * 0.8f, num99);
                }
                else if (dust.type == 185)
                {
                    float num100 = dust.scale;
                    if (num100 > 1f)
                    {
                        num100 = 1f;
                    }
                    if (dust.noLight)
                    {
                        num100 *= 0.1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num100 * 0.1f, num100 * 0.7f, num100);
                }
                else if (dust.type == 107)
                {
                    float num101 = dust.scale * 0.5f;
                    if (num101 > 1f)
                    {
                        num101 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num101 * 0.1f, num101, num101 * 0.4f);
                }
                else if (dust.type == 34 || dust.type == 35 || dust.type == 152)
                {
                    if (!Collision.WetCollision(new Vector2(dust.position.X, dust.position.Y - 8f), 4, 4))
                    {
                        dust.scale = 0f;
                    }
                    else
                    {
                        dust.alpha += Main.rand.Next(2);
                        if (dust.alpha > 255)
                        {
                            dust.scale = 0f;
                        }
                        dust.velocity.Y = -0.5f;
                        if (dust.type == 34)
                        {
                            dust.scale += 0.005f;
                        }
                        else
                        {
                            dust.alpha++;
                            dust.scale -= 0.01f;
                            dust.velocity.Y = -0.2f;
                        }
                        Dust expr_3E8A_cp_0 = dust;
                        expr_3E8A_cp_0.velocity.X = expr_3E8A_cp_0.velocity.X + (float)Main.rand.Next(-10, 10) * 0.002f;
                        if ((double)dust.velocity.X < -0.25)
                        {
                            dust.velocity.X = -0.25f;
                        }
                        if ((double)dust.velocity.X > 0.25)
                        {
                            dust.velocity.X = 0.25f;
                        }
                    }
                    if (dust.type == 35)
                    {
                        float num102 = dust.scale * 0.3f + 0.4f;
                        if (num102 > 1f)
                        {
                            num102 = 1f;
                        }
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num102, num102 * 0.5f, num102 * 0.3f);
                    }
                }
                if (dust.type == 68)
                {
                    float num103 = dust.scale * 0.3f;
                    if (num103 > 1f)
                    {
                        num103 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num103 * 0.1f, num103 * 0.2f, num103);
                }
                if (dust.type == 70)
                {
                    float num104 = dust.scale * 0.3f;
                    if (num104 > 1f)
                    {
                        num104 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num104 * 0.5f, 0f, num104);
                }
                if (dust.type == 41)
                {
                    Dust expr_4038_cp_0 = dust;
                    expr_4038_cp_0.velocity.X = expr_4038_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.01f;
                    Dust expr_405F_cp_0 = dust;
                    expr_405F_cp_0.velocity.Y = expr_405F_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.01f;
                    if ((double)dust.velocity.X > 0.75)
                    {
                        dust.velocity.X = 0.75f;
                    }
                    if ((double)dust.velocity.X < -0.75)
                    {
                        dust.velocity.X = -0.75f;
                    }
                    if ((double)dust.velocity.Y > 0.75)
                    {
                        dust.velocity.Y = 0.75f;
                    }
                    if ((double)dust.velocity.Y < -0.75)
                    {
                        dust.velocity.Y = -0.75f;
                    }
                    dust.scale += 0.007f;
                    float num105 = dust.scale * 0.7f;
                    if (num105 > 1f)
                    {
                        num105 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num105 * 0.4f, num105 * 0.9f, num105);
                }
                else if (dust.type == 44)
                {
                    Dust expr_419F_cp_0 = dust;
                    expr_419F_cp_0.velocity.X = expr_419F_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.003f;
                    Dust expr_41C6_cp_0 = dust;
                    expr_41C6_cp_0.velocity.Y = expr_41C6_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.003f;
                    if ((double)dust.velocity.X > 0.35)
                    {
                        dust.velocity.X = 0.35f;
                    }
                    if ((double)dust.velocity.X < -0.35)
                    {
                        dust.velocity.X = -0.35f;
                    }
                    if ((double)dust.velocity.Y > 0.35)
                    {
                        dust.velocity.Y = 0.35f;
                    }
                    if ((double)dust.velocity.Y < -0.35)
                    {
                        dust.velocity.Y = -0.35f;
                    }
                    dust.scale += 0.0085f;
                    float num106 = dust.scale * 0.7f;
                    if (num106 > 1f)
                    {
                        num106 = 1f;
                    }
                    Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num106 * 0.7f, num106, num106 * 0.8f);
                }
                else
                {
                    Dust expr_42F6_cp_0 = dust;
                    expr_42F6_cp_0.velocity.X = expr_42F6_cp_0.velocity.X * 0.99f;
                }
                if (dust.type != 79 && dust.type != 268)
                {
                    dust.rotation += dust.velocity.X * 0.5f;
                }
                if (dust.fadeIn > 0f && dust.fadeIn < 100f)
                {
                    if (dust.type == 235)
                    {
                        dust.scale += 0.007f;
                        int num107 = (int)dust.fadeIn - 1;
                        if (num107 >= 0 && num107 <= 255)
                        {
                            Vector2 vector4 = dust.position - Main.player[num107].Center;
                            float num108 = vector4.Length();
                            num108 = 100f - num108;
                            if (num108 > 0f)
                            {
                                dust.scale -= num108 * 0.0015f;
                            }
                            vector4.Normalize();
                            float num109 = (1f - dust.scale) * 20f;
                            vector4 *= -num109;
                            dust.velocity = (dust.velocity * 4f + vector4) / 5f;
                        }
                    }
                    else if (dust.type == 46)
                    {
                        dust.scale += 0.1f;
                    }
                    else if (dust.type == 213 || dust.type == 260)
                    {
                        dust.scale += 0.1f;
                    }
                    else
                    {
                        dust.scale += 0.03f;
                    }
                    if (dust.scale > dust.fadeIn)
                    {
                        dust.fadeIn = 0f;
                    }
                }
                else if (dust.type == 213 || dust.type == 260)
                {
                    dust.scale -= 0.2f;
                }
                else
                {
                    dust.scale -= 0.01f;
                }
                if (dust.type >= 130 && dust.type <= 134)
                {
                    float num110 = dust.scale;
                    if (num110 > 1f)
                    {
                        num110 = 1f;
                    }
                    if (dust.type == 130)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num110 * 1f, num110 * 0.5f, num110 * 0.4f);
                    }
                    if (dust.type == 131)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num110 * 0.4f, num110 * 1f, num110 * 0.6f);
                    }
                    if (dust.type == 132)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num110 * 0.3f, num110 * 0.5f, num110 * 1f);
                    }
                    if (dust.type == 133)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num110 * 0.9f, num110 * 0.9f, num110 * 0.3f);
                    }
                    if (dust.noGravity)
                    {
                        dust.velocity *= 0.93f;
                        if (dust.fadeIn == 0f)
                        {
                            dust.scale += 0.0025f;
                        }
                    }
                    else if (dust.type == 131)
                    {
                        dust.velocity *= 0.98f;
                        Dust expr_46CF_cp_0 = dust;
                        expr_46CF_cp_0.velocity.Y = expr_46CF_cp_0.velocity.Y - 0.1f;
                        dust.scale += 0.0025f;
                    }
                    else
                    {
                        dust.velocity *= 0.95f;
                        dust.scale -= 0.0025f;
                    }
                }
                else if (dust.type >= 219 && dust.type <= 223)
                {
                    float num111 = dust.scale;
                    if (num111 > 1f)
                    {
                        num111 = 1f;
                    }
                    if (!dust.noLight)
                    {
                        if (dust.type == 219)
                        {
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num111 * 1f, num111 * 0.5f, num111 * 0.4f);
                        }
                        if (dust.type == 220)
                        {
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num111 * 0.4f, num111 * 1f, num111 * 0.6f);
                        }
                        if (dust.type == 221)
                        {
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num111 * 0.3f, num111 * 0.5f, num111 * 1f);
                        }
                        if (dust.type == 222)
                        {
                            Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num111 * 0.9f, num111 * 0.9f, num111 * 0.3f);
                        }
                    }
                    if (dust.noGravity)
                    {
                        dust.velocity *= 0.93f;
                        if (dust.fadeIn == 0f)
                        {
                            dust.scale += 0.0025f;
                        }
                    }
                    dust.velocity *= new Vector2(0.97f, 0.99f);
                    dust.scale -= 0.0025f;
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Player player6 = (Player)dust.customData;
                        dust.position += player6.position - player6.oldPosition;
                    }
                }
                else if (dust.type == 226)
                {
                    float num112 = dust.scale;
                    if (num112 > 1f)
                    {
                        num112 = 1f;
                    }
                    if (!dust.noLight)
                    {
                        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num112 * 0.2f, num112 * 0.7f, num112 * 1f);
                    }
                    if (dust.noGravity)
                    {
                        dust.velocity *= 0.93f;
                        if (dust.fadeIn == 0f)
                        {
                            dust.scale += 0.0025f;
                        }
                    }
                    dust.velocity *= new Vector2(0.97f, 0.99f);
                    if (dust.customData != null && dust.customData is Player)
                    {
                        Player player7 = (Player)dust.customData;
                        dust.position += player7.position - player7.oldPosition;
                    }
                    dust.scale -= 0.01f;
                }
                else if (dust.noGravity)
                {
                    dust.velocity *= 0.92f;
                    if (dust.fadeIn == 0f)
                    {
                        dust.scale -= 0.04f;
                    }
                }
                if (dust.position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
                {
                    dust.active = false;
                }
                float num113 = 0.1f;
                if ((double)Dust.dCount == 0.5)
                {
                    dust.scale -= 0.001f;
                }
                if ((double)Dust.dCount == 0.6)
                {
                    dust.scale -= 0.0025f;
                }
                if ((double)Dust.dCount == 0.7)
                {
                    dust.scale -= 0.005f;
                }
                if ((double)Dust.dCount == 0.8)
                {
                    dust.scale -= 0.01f;
                }
                if ((double)Dust.dCount == 0.9)
                {
                    dust.scale -= 0.02f;
                }
                if ((double)Dust.dCount == 0.5)
                {
                    num113 = 0.11f;
                }
                if ((double)Dust.dCount == 0.6)
                {
                    num113 = 0.13f;
                }
                if ((double)Dust.dCount == 0.7)
                {
                    num113 = 0.16f;
                }
                if ((double)Dust.dCount == 0.8)
                {
                    num113 = 0.22f;
                }
                if ((double)Dust.dCount == 0.9)
                {
                    num113 = 0.25f;
                }
                if (dust.scale < num113)
                {
                    dust.active = false;
                }
                #endregion
            }
            int num114 = num;
            if ((double)num114 > (double)Main.maxDustToDraw * 0.9)
            {
                Dust.dCount = 0.9f;
                return;
            }
            if ((double)num114 > (double)Main.maxDustToDraw * 0.8)
            {
                Dust.dCount = 0.8f;
                return;
            }
            if ((double)num114 > (double)Main.maxDustToDraw * 0.7)
            {
                Dust.dCount = 0.7f;
                return;
            }
            if ((double)num114 > (double)Main.maxDustToDraw * 0.6)
            {
                Dust.dCount = 0.6f;
                return;
            }
            if ((double)num114 > (double)Main.maxDustToDraw * 0.5)
            {
                Dust.dCount = 0.5f;
                return;
            }
            Dust.dCount = 0f;
        }
        #endregion
    }
}