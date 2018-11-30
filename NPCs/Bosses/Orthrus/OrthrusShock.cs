using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using BaseMod;

namespace AAMod.NPCs.Bosses.Orthrus
{
	public class OrthrusShock : AAProjectile
	{
		
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgurshock");
        }		

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = 4;
            projectile.timeLeft = 200;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
        }

		public static Color boltColor = new Color(222, 170, 210, 220);
		public static Texture2D mainTex;
		public static Texture2D chainTex;
		public static Texture2D chainEndTex;
		public static Texture2D chainEndTex2;

		public Vector2[] targetPos = new Vector2[0];
		public Vector2[] notargetPos = new Vector2[0];
		public static int[] immuneNPC = new int[Main.npc.Length]; //static so both minions use the same immune timer
		public int lifeTimer = 12;
		public int randomTargetTimer = 10;
		public int masterType = 0;
		public int master = -1;
		public int maxTargets = 5;
		public float minRange = 0f;
		public float maxRange = 120f;
		public bool hasVel = false;
		public float velRot = 0f;
		public float boltVariation = 1f;

        public int setMasterDelay = 3;

		public override void SetMaster(params object[] args)
		{
			masterType = (int)args[0]; //0 == player, 1 == npc, 2 == projectile, 3 == player at center, 4 == stationary, projectile
			if (masterType == 2) { boltVariation = 0.25f; }
			master = (int)args[1];
			maxTargets = (int)args[2];
			minRange = (float)args[3];
			maxRange = (float)args[4];
			if ((bool)args[5] && Main.netMode != 0)
			{
				AANet.SendNetMessage(AANet.SyncShock, (short)projectile.whoAmI, (byte)masterType, (short)master, (byte)maxTargets, minRange, maxRange);
			}
		}

		public override void AI()
		{
			if (master == -2) { projectile.Kill(); return; }
			if (master == -1) { lifeTimer--; if (lifeTimer <= 0) { projectile.Kill(); } return; }
			if (masterType == 2) for (int m = 0; m < immuneNPC.Length; m++) immuneNPC[m] = Math.Max(0, immuneNPC[m] - 1);
			if (Main.myPlayer == projectile.owner && master != -1 && setMasterDelay != -1)
			{
				setMasterDelay--;
				if (setMasterDelay == 0)
				{
					setMasterDelay = -1;
					SetMaster(masterType, master, maxTargets, minRange, maxRange, true);
					projectile.netUpdate = true;
				}
				return;
			}
			if (masterType == 0)
			{
				if (!hasVel) { hasVel = true; velRot = BaseMod.BaseUtility.RotationTo(projectile.Center, projectile.Center + projectile.velocity); }
				GetTargetPos(true, maxRange + 40, velRot);
			}else
			if (masterType == 1)
			{
				if (!hasVel) { hasVel = true; velRot = BaseMod.BaseUtility.RotationTo(projectile.Center, projectile.Center + projectile.velocity); }
				GetTargetPos(true, 0f, velRot);
			}else{ GetTargetPos(false); }
			if (masterType != 4) { projectile.Center = GetMasterCenter(); }
			projectile.velocity = default(Vector2);
			if (masterType == 0 || masterType == 3) { lifeTimer--; if (lifeTimer <= 0 || !Main.player[master].active || Main.player[master].dead) { projectile.Kill(); } }else
			if (masterType == 1 && (!Main.npc[master].active || Main.npc[master].life <= 0)) { projectile.Kill(); }else
			if (masterType == 2 || masterType == 4) { lifeTimer--; if (!Main.projectile[master].active || lifeTimer <= 0) { projectile.Kill(); } }
		}

		public Vector2 GetMasterCenter()
		{
			switch (masterType)
			{
				case 0: return BaseUtility.RotateVector(Main.player[master].Center, Main.player[master].Center + new Vector2(40f, 0f), velRot);
				case 1: return Main.npc[master].Center;
				case 2: return Main.projectile[master].Center;
				case 3: return Main.player[master].Center;
				default: return projectile.Center;
			}
		}

		public void GetTargetPos(bool directional, float distance = 0f, float rotation = 0f)
		{
			Vector2 startPos = !directional ? GetMasterCenter() : BaseMod.BaseUtility.RotateVector(projectile.Center, projectile.Center + new Vector2(distance, 0f), rotation);
			List<Entity> list = new List<Entity>();
			List<Vector2> list1 = new List<Vector2>();
			List<Vector2> list2 = new List<Vector2>();
			if (masterType == 0 && Main.player[master].wet && !Main.player[master].immune) { list.Add(Main.player[master]); list1.Add(Main.player[master].Center); }
			int[] players = BaseMod.BaseAI.GetPlayers(startPos, masterType == 0 ? new int[] { master } : default(int[]), true, maxRange);
			foreach (int i1 in players)
			{
				if (list1.Count >= maxTargets) { break; }
				Player player = Main.player[i1];
				if (CanTarget(player))
				{
					list1.Add(player.Center);
					list.Add(player);
				}
			}
			int[] npcs = BaseMod.BaseAI.GetNPCs(startPos, -1, masterType == 1 ? new int[] { master } : default(int[]), maxRange);
			foreach (int i in npcs)
			{
				if (list1.Count >= maxTargets) { break; }
				NPC npc = Main.npc[i];
				if (CanTarget(npc))
				{
					list1.Add(npc.Center);
					list.Add(npc);
				}
			}
			targetPos = list1.ToArray();
			int totalLength = list1.Count;
			if (randomTargetTimer % 5 == 0 && totalLength == 0 && masterType != 2 && masterType != 4)
			{
				for (int m = 0; m < 2; m++)
				{
					list2.Add(BaseMod.BaseUtility.GetRandomPosNear(startPos, (int)minRange, (int)maxRange, true));
				}
				notargetPos = list2.ToArray();
			}
			randomTargetTimer = Math.Max(0, randomTargetTimer - 1); if (randomTargetTimer == 0) { randomTargetTimer = 10; }

			Vector2 oldPos = projectile.position;
			foreach (Entity codable in list)
			{
				if (codable is NPC)
				{
					NPC npc = (NPC)codable;
					if (projectile.owner == Main.myPlayer && (masterType == 2 && immuneNPC[npc.whoAmI] <= 0) || (masterType != 2 && npc.immune[projectile.owner] <= 0))
					{
						projectile.position = npc.position; projectile.Damage(); projectile.position = oldPos;
						if (masterType == 2)
						{
							//npc.immune[projectile.owner] = 0;
							immuneNPC[npc.whoAmI] = 10;
						}
					}
				}else
				if (codable is Player)
				{
					Player player = (Player)codable;
					if (player.whoAmI == Main.myPlayer && !player.immune)
					{
						if (player.whoAmI == master) { projectile.friendly = false; projectile.hostile = true; }
						projectile.position = player.position; projectile.Damage(); projectile.position = oldPos;
						if (player.whoAmI == master) { projectile.friendly = true; projectile.hostile = false; }
					}
				}
			}
		}

		public bool CanTarget(Entity codable)
		{
			if (codable is NPC)
			{
				NPC npc = (NPC)codable;
				return !npc.friendly && !npc.dontTakeDamage && (npc.lifeMax == 1 || npc.lifeMax > 5) && (masterType != 2 || BaseMod.BaseUtility.CanHit(projectile.Hitbox, npc.Hitbox));
			}else
			if (codable is Player)
			{
				Player player = (Player)codable;
				return masterType != 2 && ((masterType != 0 && masterType != 3 && masterType != 4) || (player.hostile && (Main.player[master].team == 0 || player.team == 0 || Main.player[master].team != player.team)));
			}
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
		{
			if(!Main.instance.IsActive) { return false; }
			BaseMod.BaseDrawing.AddLight(projectile.Center, boltColor);
			if (mainTex == null)
			{
				mainTex = mod.GetTexture("NPCs/Bosses/Orthrus/ElectricShockChainEnd3");
				chainTex = mod.GetTexture("NPCs/Bosses/Orthrus/ElectricShockChain");
				chainEndTex = mod.GetTexture("NPCs/Bosses/Orthrus/ElectricShockChainEnd");
				chainEndTex2 = mod.GetTexture("NPCs/Bosses/Orthrus/ElectricShockChainEnd2");
			}
			for (int m = 0; m < targetPos.Length; m++)
			{
				Vector2 target = targetPos[m];
				DrawArc(spriteBatch, new Texture2D[] { chainEndTex, chainTex, mainTex }, projectile.Center, target);
			}
			for (int m1 = 0; m1 < notargetPos.Length; m1++)
			{
				Vector2 target = notargetPos[m1];
				DrawArc(spriteBatch, new Texture2D[] { chainEndTex, chainTex, chainEndTex2 }, projectile.Center, target);
			}
			return false;
		}

		public void DrawArc(SpriteBatch sb, Texture2D[] texs, Vector2 startPos, Vector2 endPos)
		{
			float Jump = texs[1].Height * 5;
			float length = Vector2.Distance(startPos, endPos);
			float Way = 0f;
			Vector2 currentPoint = startPos;
			while (Way < length)
			{
				Vector2 dir = endPos - currentPoint; dir.Normalize();
				Vector2 vstart = currentPoint;
				Vector2 vend = currentPoint + (dir * Jump); vend = BaseMod.BaseUtility.RotateVector(vstart, vend, (float)(Math.PI / (5f + Main.rand.Next(5))) * boltVariation * (Main.rand.Next(2) == 0 ? -1f : 1f));
				Texture2D[] textures = new Texture2D[] { null, texs[1], texs[0] };
				if (Way + Jump >= length) { textures[2] = texs[2]; vend = endPos; }
				DrawArcSegment(sb, textures, vstart, vend, mod, projectile);
				Way += Jump; currentPoint = vend;
				BaseMod.BaseDrawing.AddLight(vend, boltColor, vend == endPos ? 1f : 2f);
			}
		}

		public static void DrawArcSegment(SpriteBatch sb, Texture2D[] textures, Vector2 start, Vector2 end, Mod mod, Projectile projectile)
		{
			bool drawEndsUnder = true;
			Color boltColor = new Color(160, 219, 249);
			Color? overrideColor = new Color(255, 255, 255, 0);
			float scale = 0.8f;
			float Jump = (textures[1].Height - 2f) * scale;
			Vector2 dir = end - start;
			dir.Normalize();
			float length = Vector2.Distance(start, end);
			float Way = 0f;
			float rotation = BaseMod.BaseUtility.RotationTo(start, end) - 1.57f;
			while (Way < length)
			{
				float texWidth;
				float texHeight;
				Vector2 texCenter;
				Vector2 v;
				Color lightColor;
				Action drawEnds = () =>
				{
					if (textures[0] != null && Way == 0f)
					{
						texWidth = (float)textures[0].Width;
						texHeight = (float)textures[0].Height;
						texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;
						v = start - Main.screenPosition + texCenter;
						lightColor = (overrideColor != null ? (Color)overrideColor : BaseMod.BaseDrawing.GetLightColor(start + texCenter));
						sb.Draw(textures[0], v - texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0f);
					}
					if (textures[2] != null && Way + Jump >= length)
					{
						texWidth = (float)textures[2].Width;
						texHeight = (float)textures[2].Height;
						texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;
						v = end - Main.screenPosition + texCenter;
						lightColor = (overrideColor != null ? (Color)overrideColor : BaseMod.BaseDrawing.GetLightColor(end + texCenter));
						sb.Draw(textures[2], v - texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0f);
					}
				};
				if (drawEndsUnder) { drawEnds(); }
				texWidth = (float)textures[1].Width;
				texHeight = (float)textures[1].Height;
				texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;
				v = (start + dir * Way) - Main.screenPosition + texCenter;
				lightColor = (overrideColor != null ? (Color)overrideColor : BaseMod.BaseDrawing.GetLightColor((start + dir * Way) + texCenter));
				sb.Draw(textures[1], v - texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0f);

				if (Main.rand.Next(15) == 0)
				{
					v += Main.screenPosition;
                    int dustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 0, boltColor, 1f);
					Main.dust[dustID].rotation = (Main.rand.Next(5) * (float)(Math.PI / 8f));
					Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
					Main.dust[dustID].velocity *= 3f;
				}
				if (!drawEndsUnder) { drawEnds(); }
				Way += Jump;
			}
		}
	}
}
