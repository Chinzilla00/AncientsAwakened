using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using System;
using System.IO;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace AAMod.Items.Dev.Invoker
{
	public class InvokerPlayer : ModPlayer
	{
		public static InvokerPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<InvokerPlayer>();
		}
		public float BanishDamage;
		public float BanishDamageMult = 1f;
		public int BanishLimit = 1;

		public override void ResetEffects()
		{
			SpringInvoker = false;
			banishing = false;
			InvokedCaligula = false;
			BanishProjClear = false;
			Thebookoflaw = false;
			InvokerMadness = false;
			InvokerShow = false;
			BanishDamage = 0;
			ResetVariables();
		}

		public override void UpdateDead()
		{
			SpringInvoker = false;
			banishing = false;
			InvokedCaligula = false;
			BanishProjClear = false;
			Thebookoflaw = false;
			InvokerMadness = false;
			InvokerShow = false;
			BanishDamage = 0;
			ResetVariables();
		}

		private void ResetVariables()
		{
			BanishDamageMult = 1f;
			BanishLimit = 10;
		}
		public override void Initialize()
		{
			DarkCaligula = false;
			WindRaidjin = false;
			WaterCocytus = false;
			FirePurgatrio = false;
			EarthMagellanica = false;
			LightingMechaba = false;
			FinisherElysium = false;
			CaligulaSoul = new List<int>();
		}

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			if (DarkCaligula)
			{
				list.Add("DarkCaligula");
			}
			if (WindRaidjin)
			{
				list.Add("WindRaidjin");
			}
			if (WaterCocytus)
			{
				list.Add("WaterCocytus");
			}
			if (FirePurgatrio)
			{
				list.Add("FirePurgatrio");
			}
			if (EarthMagellanica)
			{
				list.Add("EarthMagellanica");
			}
			if (LightingMechaba)
			{
				list.Add("LightingMechaba");
			}
			if (FinisherElysium)
			{
				list.Add("FinisherElysium");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("InvokerSummon", list);
			tagCompound.Add("CaligulaSoul", CaligulaSoul);
			return tagCompound;
		}
		public override void Load(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("InvokerSummon");
			DarkCaligula = list.Contains("DarkCaligula");
			WindRaidjin = list.Contains("WindRaidjin");
			WaterCocytus = list.Contains("WaterCocytus");
			FirePurgatrio = list.Contains("FirePurgatrio");
			EarthMagellanica = list.Contains("EarthMagellanica");
			LightingMechaba = list.Contains("LightingMechaba");
			FinisherElysium = list.Contains("FinisherElysium");
			foreach(int k in tag.GetList<int>("CaligulaSoul"))
			{
				CaligulaSoul.Add(k);
			}
		}
		
		public override void LoadLegacy(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if(num == 0)
			{
				BitsByte bitsByte = reader.ReadByte();
				DarkCaligula = bitsByte[0];
				WindRaidjin = bitsByte[1];
				WaterCocytus = bitsByte[2];
				FirePurgatrio = bitsByte[3];
				EarthMagellanica = bitsByte[4];
				LightingMechaba = bitsByte[5];
				FinisherElysium = bitsByte[6];
				return;
			}
		}
		
		public List<int> CaligulaSoul;
		public bool DarkCaligula;
		public bool WindRaidjin;
		public bool WaterCocytus;
		public bool FirePurgatrio;
		public bool EarthMagellanica;
		public bool LightingMechaba;
		public bool FinisherElysium;
		public bool banishing;
		public bool SpringInvoker;
		public bool InvokerShow;
		public bool InvokedCaligula;
		public bool InvokerMadness;
		public bool Thebookoflaw;
		public bool BanishProjClear;
		private int InvokedCaligulaClaw = 0;
		private int ClawDir = 0;


		public override void UpdateLifeRegen()
		{
			if (InvokedCaligula)
			{
				player.statLifeMax2 *= 2;
				player.statDefense *= 2;
			}
		}
		public override void FrameEffects()
		{
			int soulcount = 0;
			foreach(int soul in CaligulaSoul)
			{
				if(soul == mod.NPCType("AkumaA")) soulcount ++;
				if(soul == mod.NPCType("YamataA")) soulcount ++;
				if(soul == mod.NPCType("ZeroProtocol")) soulcount ++;
				if(soul == mod.NPCType("ShenA")) soulcount ++;
				if(soul == mod.NPCType("SupremeRajah")) soulcount ++;
			}
			if(soulcount >= 5)
			{
				DarkCaligula = true;
			}
			if (Thebookoflaw && DarkCaligula)
			{
				player.AddBuff(mod.BuffType("InvokedCaligulaSafe"), 3600);
			}
			if (InvokerShow)
			{
                player.legs = mod.GetEquipSlot("InvokerLegs", EquipType.Legs);
                player.body = mod.GetEquipSlot("InvokerBody", EquipType.Body);
                player.head = mod.GetEquipSlot("InvokerHead", EquipType.Head);
			}
			if (InvokedCaligula)
			{
                player.legs = mod.GetEquipSlot("InvokedCaligulaLegs", EquipType.Legs);
                player.body = mod.GetEquipSlot("InvokedCaligulaBody", EquipType.Body);
                player.head = mod.GetEquipSlot("InvokedCaligulaHead", EquipType.Head);
				
				if(Main.mouseLeft && player.inventory[player.selectedItem].damage > 0)
				{
					InvokedCaligulaClaw ++;
					if(InvokedCaligulaClaw == 1)
					{
						float scaleFactor6 = 15f;
						Vector2 vector20 = Main.MouseWorld - player.RotatedRelativePoint(player.MountedCenter, true);
						vector20.Normalize();
						if (vector20.HasNaNs())
						{
							vector20 = Vector2.UnitX * player.direction;
						}
						vector20 *= scaleFactor6;
						ClawDir = Projectile.NewProjectile(player.position.X, player.position.Y, vector20.X, vector20.Y, mod.ProjectileType("InvokedCaligulaShoot"), (int)((DarkCaligula? 1200 : 600) * (player.minionDamage + player.allDamage - 1)), 4f, player.whoAmI, 0f, 0f);
					}
					else if(InvokedCaligulaClaw > 30)
					{
						player.ChangeDir(Main.projectile[ClawDir].direction);
						InvokedCaligulaClaw = 0;
					}
				}
				
				else
				{
					InvokedCaligulaClaw = 0;
				}
				
			}
		}
		
	}

	public class InvokedCaligulaShoot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("InvokedCaligulaClaw");
			Main.projFrames[projectile.type] = 28;
		}
		public override void SetDefaults()
		{
			projectile.width = 68;
			projectile.height = 64;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.timeLeft = 30;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.IndianRed;
        }

		public override void AI()
        {
			Player player = Main.player[Main.myPlayer];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float position1 = Main.mouseX + Main.screenPosition.X - vector.X;
			float position2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
			projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
			if(player.direction == -1)
			{
				projectile.rotation = (float)Math.Atan2(position2 * player.direction, position1 * player.direction) - player.fullRotation + MathHelper.ToRadians(180f);
			}
			else
			{
				projectile.rotation = (float)Math.Atan2(position2 * player.direction, position1 * player.direction) + player.fullRotation;
			}
			int num1 = projectile.frame + 1;
			projectile.frame = num1;
			if (num1 >= Main.projFrames[projectile.type])
			{
				projectile.frame = 0;
			}
			projectile.soundDelay--;
			if (projectile.soundDelay <= 0)
			{
				Main.PlaySound(SoundID.Item1, projectile.Center);
				projectile.soundDelay = 12;
			}
			if(Main.mouseLeft)
			{
				float scaleFactor6 = 15f;
				Vector2 vector20 = Main.MouseWorld - player.RotatedRelativePoint(player.MountedCenter, true);
				vector20.Normalize();
				if (vector20.HasNaNs())
				{
					vector20 = Vector2.UnitX * player.direction;
				}
				vector20 *= scaleFactor6;
				if (vector20.X != projectile.velocity.X || vector20.Y != projectile.velocity.Y)
				{
					projectile.netUpdate = true;
				}
				projectile.velocity = vector20;
			}
			else
			{
				projectile.Kill();
			}
			Vector2 vector21 = projectile.Center + projectile.velocity * 3f;
			Lighting.AddLight(vector21, 0.8f, 0.8f, 0.8f);
			if (Main.rand.Next(3) == 0)
			{
				int num2 = Dust.NewDust(vector21 - projectile.Size / 2f, projectile.width, projectile.height, 63, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].position -= projectile.velocity;
			}
			player.ChangeDir(Main.projectile[projectile.whoAmI].direction);

			for(int i=0; i < 200; i++)
			{
				if(projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
				{
					Main.npc[i].immune[projectile.owner] = 0;
					Main.npc[i].GetGlobalNPC<InvokedGlobalNPC>().CaligulaSoulClaw = true;
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			
			Player player = Main.player[Main.myPlayer];
			//damage = (int)((player.GetModPlayer<InvokerPlayer>().DarkCaligula? 1000 : 500) * (player.minionDamage + player.allDamage));
			crit = true;
			if(player.GetModPlayer<InvokerPlayer>().DarkCaligula)
			{
                string Lifelength = target.lifeMax + "";
				int regen = (Main.rand.Next(2) == 0 ? 2*(Lifelength.Length + 1) : (Lifelength.Length + 1));
				player.statLife += regen;
				player.HealEffect(regen, true);
				if (player.statLife > player.statLifeMax2)
				{
					player.statLife = player.statLifeMax2;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;
		}
	}
}