using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria.Localization;


namespace AAMod.Items.Dev.Invoker
{
	public class InvokerStaff : BanishDamageItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aleister Staff");
			Tooltip.SetDefault("");

            Item.staff[item.type] = true;

        }

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
            string text = "";
			Player player = Main.player[Main.myPlayer];
			if(!player.GetModPlayer<InvokerPlayer>().Thebookoflaw)
			{
				text += Language.GetTextValue("Mods.AAMod.Common.InvokerStaff1");
			}
			else
			{
				text += Language.GetTextValue("Mods.AAMod.Common.InvokerStaff2");
			}
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine != null && tooltipLine.Name == "Damage")
				{
					string[] splitText = tooltipLine.text.Split(' ');
					string damageValue = splitText.First();
					string damageWord = splitText.Last();
					if(Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().Thebookoflaw) 
					{
						tooltipLine.text = damageValue + " " + Language.GetTextValue("Mods.AAMod.Common.InvokerDamage1") + damageWord;
					}
				}
				if (tooltipLine != null && tooltipLine.Name == "Tooltip0")
				{
					tooltipLine.text = text;
				}
			}
		}

        public override void SafeSetDefaults()
        {
			item.scale = 0.65f;
			item.width = 41;
			item.height = 41;
			item.rare = ItemRarityID.Purple;
			item.damage = 200;
			item.noMelee = true;
			item.autoReuse = true;
			item.reuseDelay = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useTime = 16;
			item.useAnimation = 16;
			item.shoot = mod.ProjectileType("InvokerStaffproj"); 
			item.shootSpeed = 40f;
			item.value = Item.buyPrice(10, 36, 0, 0);
        }
		public override bool CanUseItem(Player player)
		{
			if(!player.GetModPlayer<InvokerPlayer>().Thebookoflaw)
			{
				item.noMelee = false;
				Item.staff[item.type] = false;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.damage = (int)(200 * player.minionDamage);
				item.summon = true;
				return true;
			}
			else if(player.GetModPlayer<InvokerPlayer>().Thebookoflaw)
			{
				item.noMelee = true;
				Item.staff[item.type] = true;
				item.useStyle = ItemUseStyleID.HoldingOut;
				return true;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse != 2 && player.GetModPlayer<InvokerPlayer>().Thebookoflaw)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("InvokerStaffproj"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (player.altFunctionUse == 2 && player.GetModPlayer<InvokerPlayer>().SpringInvoker)
			{
				if(!player.GetModPlayer<InvokerPlayer>().InvokerMadness)
				{
					player.AddBuff(mod.BuffType("InvokerofMadness"), player.GetModPlayer<InvokerPlayer>().DarkCaligula? 30:3000);
					player.GetModPlayer<InvokerPlayer>().BanishDamage = item.damage * 5;
					player.GetModPlayer<InvokerPlayer>().banishing = true;
				}
			}
			return false;
		}

        public override Vector2? HoldoutOrigin()
		{
			return new Vector2(38, 42);
		}

		public override bool AltFunctionUse(Player player)
		{
			return !(!player.GetModPlayer<InvokerPlayer>().DarkCaligula && player.GetModPlayer<InvokerPlayer>().InvokedCaligula) && player.GetModPlayer<InvokerPlayer>().SpringInvoker;
		}

    }

	public abstract class BanishDamageItem : BaseAAItem
	{
		public virtual void SafeSetDefaults()
		{
		}
		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			mult *= InvokerPlayer.ModPlayer(player).BanishDamageMult;
		}

		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			knockback = 0;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit = 0;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] splitText = tt.text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				tt.text = damageValue + " banish " + damageWord;
			}
		}
	}
    public class InvokerStaffproj : ModProjectile
	{
        public override void SetDefaults()
        {
           	projectile.width = 41;
           	projectile.height = 41;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.timeLeft = 6000;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.damage = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightBlue;
        }
        
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, (Main.DiscoR -projectile.alpha) * 0.8f / 255f, (Main.DiscoG -projectile.alpha) * 0.4f / 255f, (Main.DiscoB -projectile.alpha) * 0f / 255f);
            Player projOwner = Main.player[projectile.owner];
           	projectile.direction = projOwner.direction;
           	projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);

            if (projectile.spriteDirection == -1)
            {
               projectile.rotation -= MathHelper.ToRadians(90f);
            }

			if (projectile.ai[0] == 0f)
			{
				float[] ai = projectile.ai;
				int num2 = 1;
				float num3 = ai[num2];
				ai[num2] = num3 + 1f;
				if (projectile.ai[1] >= 45f)
				{
					projectile.ai[1] = 45f;
					if (projectile.velocity.X < 0f)
					{
						projectile.spriteDirection = -1;
						projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);
					}
					else
					{
						projectile.spriteDirection = 1;
						projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
					}
				}
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
				int num6 = 15;
				bool flag = false;
				bool flag2 = false;
				float[] localAI = projectile.localAI;
				int num7 = 0;
				float num8 = localAI[num7];
				localAI[num7] = num8 + 1f;
				if (projectile.localAI[0] % 30f == 0f)
				{
					flag2 = true;
				}
				int num9 = (int)projectile.ai[1];
				if (projectile.localAI[0] >= 60 * num6)
				{
					flag = true;
				}
				else if (num9 < 0 || num9 >= 200)
				{
					flag = true;
				}
				else if (Main.npc[num9].active && !Main.npc[num9].dontTakeDamage)
				{
					projectile.Center = Main.npc[num9].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.npc[num9].gfxOffY;
					projectile.alpha = Main.npc[num9].alpha;
					if (flag2)
					{
						Main.npc[num9].HitEffect(0, 1.0);
					}
					if(Main.npc[num9].GetGlobalNPC<InvokedGlobalNPC>().IsBeingBanished)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					projectile.Kill();
				}
			}
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);

			double Realdamage = Main.CalculateDamage(projectile.damage, 0);

			if(Main.player[projectile.owner].GetModPlayer<InvokerPlayer>().SpringInvoker)
			{
				if (target.realLife >= 0)
				{
					if(Main.npc[target.realLife].StrikeNPC((int)(damage * .1f), knockback, hitDirection, crit, false, false) < .01f * projectile.damage && Realdamage < Main.npc[target.realLife].lifeMax * .01f)
					{
						Realdamage = Main.npc[target.realLife].lifeMax * .034f;
					}
				}
				else
				{
					if(target.StrikeNPC(damage, knockback, hitDirection, crit, false, false) < .01f * projectile.damage)
					{
						Realdamage = target.lifeMax * .034f;
					}
				}
			}

			Realdamage = Main.DamageVar((int)Realdamage);

			//Main.player[Main.myPlayer].dpsDamage += (int)Realdamage;
			Main.player[projectile.owner].addDPS((int)Realdamage);

			Color damagecolor = crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;
			CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), damagecolor, (int)Realdamage, false, false);
			
			if (!target.immortal)
			{
				if (target.realLife >= 0)
				{
					Main.npc[target.realLife].life -= (int)Realdamage;
					target.life = Main.npc[target.realLife].life;
					target.lifeMax = Main.npc[target.realLife].lifeMax;
				}
				else
				{
					target.life -= (int)Realdamage;
				}
			}

			/* 
			if(target.life <= Realdamage) target.life -= target.life;
			else target.life -= (int)Realdamage;

			if(target.realLife >= 0)
			{
				if(Main.npc[target.realLife].life <= Realdamage) Main.npc[target.realLife].life -= Main.npc[target.realLife].life;
				else Main.npc[target.realLife].life -= (int)Realdamage;
			}
			*/

			if (target.realLife >= 0)
			{
				Main.npc[target.realLife].checkDead();
			}
			else
			{
				target.checkDead();
			}

			if (projectile.owner == Main.myPlayer)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && ((projectile.friendly && (!Main.npc[i].friendly || projectile.type == 318 || (Main.npc[i].type == NPCID.Guide && projectile.owner < 255 && Main.player[projectile.owner].killGuide) || (Main.npc[i].type == NPCID.Clothier && projectile.owner < 255 && Main.player[projectile.owner].killClothier))) || (projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (projectile.owner < 0 || Main.npc[i].immune[projectile.owner] == 0 || projectile.maxPenetrate == 1) && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHit(Main.npc[i])))
					{
						bool flag;
						if (Main.npc[i].type == NPCID.SolarCrawltipedeTail)
						{
							Rectangle rect = Main.npc[i].getRect();
							int num = 8;
							rect.X -= num;
							rect.Y -= num;
							rect.Width += num * 2;
							rect.Height += num * 2;
							flag = projectile.Colliding(rectangle, rect);
						}
						else
						{
							flag = projectile.Colliding(rectangle, Main.npc[i].getRect());
						}
						if (flag)
						{
							if (Main.npc[i].reflectingProjectiles && projectile.CanReflect())
							{
								Main.npc[i].ReflectProjectile(projectile.whoAmI);
								return;
							}
							projectile.ai[0] = 1f;
							projectile.ai[1] = i;
							projectile.velocity = (Main.npc[i].Center - projectile.Center) * 0.75f;
							projectile.netUpdate = true;
							projectile.StatusNPC(i);
							projectile.damage = 0;
							Point[] array = new Point[10];
							int num2 = 0;
							for (int j = 0; j < 1000; j++)
							{
								if (j != projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == projectile.type && Main.projectile[j].ai[0] == 1f && Main.projectile[j].ai[1] == i)
								{
									array[num2++] = new Point(j, Main.projectile[j].timeLeft);
									if (num2 >= array.Length)
									{
										break;
									}
								}
							}
							if (num2 >= array.Length)
							{
								int num3 = 0;
								for (int k = 1; k < array.Length; k++)
								{
									if (array[k].Y < array[num3].Y)
									{
										num3 = k;
									}
								}
								Main.projectile[array[num3].X].Kill();
							}
						}
					}
				}
			}
		}
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return null;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("Invokedproj"), 3600);
		}
    }

	public class Invokedproj : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Be Banished");
			Description.SetDefault("You are marked by Invoked Magic");
			Main.debuff[Type] = false;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<InvokedGlobalNPC>().Banished = true;

			InvokerPlayer InvokerPlayer = Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>();
			if((InvokerPlayer.banishing && npc.active && (InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokerPlayer.BanishLimit > npc.life)) || npc.GetGlobalNPC<InvokedGlobalNPC>().IsBeingBanished)
			{
				npc.GetGlobalNPC<InvokedGlobalNPC>().IsBeingBanished = true;
			}
        }
	}

	public class InvokedGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public bool Banished;
		public bool IsBeingBanished = false;
		public int BanishCount = 0;
		public bool CaligulaSoulFight = false;

		public override void ResetEffects(NPC npc)
		{
			Banished = false;
		}

		public void BanishAction(NPC npc)
		{
			npc.velocity.X = 0;
			npc.velocity.Y = 0;
			npc.scale -= 0.01f;
			npc.alpha += 4;

			if(BanishCount > 70 || npc.alpha >= 250 || npc.scale < 0.05f)
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedHeal"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, npc.lifeMax * 0.01f);
				
				if(npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand)
				{
					for(int i = 0; i < 200 ; i++)
					{
						if(Main.npc[i].type == NPCID.MoonLordCore || Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand)
						{
							Main.npc[i].active = false;
							Main.npc[i].NPCLoot();
						}
					}
				}

				
				if(npc.realLife >= 0) 
				{
					if(npc.type == NPCID.EaterofWorldsHead) Main.npc[npc.realLife].boss = true;
					Main.npc[npc.realLife].NPCLoot();//This need change in AAMod
					for(int i = 0; i < 200 ; i++)
					{
						if(Main.npc[i].realLife == npc.realLife)
						{
							Main.npc[i].NPCLoot();
							Main.npc[i].active = false;
						}
					}
					NPCLoader.CheckDead(Main.npc[npc.realLife]);
					Main.npc[npc.realLife].checkDead();
					Main.npc[npc.realLife].netUpdate = true;
				}
				npc.NPCLoot();//This need change in AAMod
				NPCLoader.CheckDead(npc);
				npc.checkDead();
				npc.active = false;
				npc.life = 0;
				npc.netUpdate = true;
				BanishCount = 0;
			}
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage) 
		{
			int InvokedCount = 0;
			for (int i = 0; i < 1000; i++) 
			{
				Projectile p = Main.projectile[i];
				int num9 = (int)p.ai[1];
				if (p.active && p.type == ModContent.ProjectileType<InvokerStaffproj>() && p.ai[0] == 1f && npc == Main.npc[num9]) 
				{
					InvokedCount++;
					npc.lifeRegen -= 10 * InvokedCount;
				}
			}

			InvokerPlayer InvokerPlayer = Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>();

			if(npc.boss)
			{
				if(!InvokerPlayer.nohit)
				{
					InvokerPlayer.nohit = false;
				}
				bool flag = (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == mod.ItemType("InvokerStaff") || Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == ItemID.RodofDiscord) && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().SpringInvoker && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().Thebookoflaw;
				if(npc.life/npc.lifeMax > 0.95)
				{
					CaligulaSoulFight = true;
				}
				else if(InvokerPlayer.CaligulaSoul.Contains(npc.type))
				{
					CaligulaSoulFight = false;
				}
				else if(!flag)
				{
					CaligulaSoulFight = false;
				}
				else if(!InvokerPlayer.nohit)
				{
					CaligulaSoulFight = false;
				}
			}


			if(!npc.townNPC && (npc.life < InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokedCount) && InvokerPlayer.banishing && (npc.active || npc.life > 0))
			{
				npc.GetGlobalNPC<InvokedGlobalNPC>().IsBeingBanished = true;
			}
			if((IsBeingBanished && !npc.townNPC && (npc.active || npc.life > 0)) || (!npc.townNPC && (npc.life < InvokerPlayer.BanishDamage) && InvokerPlayer.banishing && (npc.active || npc.life > 0)))
			{
				IsBeingBanished = true;
				BanishCount ++;
				if(BanishCount == 1)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedRune"), 0, 0f, Main.player[Main.myPlayer].whoAmI, 1f, npc.whoAmI);
					
					if(npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand)
					{
						for(int i = 0; i < 200 ; i++)
						{
							if(Main.npc[i].type == NPCID.MoonLordCore || Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand)
							{
								Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedRune"), 0, 0f, Main.player[Main.myPlayer].whoAmI, 1f, npc.whoAmI);
							}
						}
					}
				}
				BanishAction(npc);
			}
			return;
		}
		
		public override bool PreNPCLoot(NPC npc)
		{
			if(Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == mod.ItemType("InvokerStaff") && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().SpringInvoker && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().Thebookoflaw)
			{
            	//Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().BanishProjClear = true; // Just for test.
				float nump7 = 4f;
				float nump8 = Main.rand.Next(-100, 101);
				float nump9 = Main.rand.Next(-100, 101);
				float nump10 = (float)Math.Sqrt(nump8 * nump8 + nump9 * nump9);
				nump10 = nump7 / nump10;
				nump8 *= nump10;
				nump9 *= nump10;
				int[] array = new int[200];
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].CanBeChasedBy(this, false))
					{
						float num5 = Math.Abs(Main.npc[i].position.X + Main.npc[i].width / 2 - npc.position.X + npc.width / 2) + Math.Abs(Main.npc[i].position.Y + Main.npc[i].height / 2 - npc.position.Y + npc.height / 2);
						if (num5 < 800f)
						{
							if (Collision.CanHit(npc.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
							{
								array[num4] = i;
								num4++;
							}
							else if (num4 == 0)
							{
								array[num3] = i;
								num3++;
							}
						}
					}
				}
				if (num3 == 0 && num4 == 0)
				{
					return true;
				}
				int num6;
				if (num4 > 0)
				{
					num6 = array[Main.rand.Next(num4)];
				}
				else
				{
					num6 = array[Main.rand.Next(num3)];
				}
				if(npc.lifeMax >= 1000) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedHeal"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, (npc.life > npc.lifeMax? npc.life : npc.lifeMax) * 0.001f);
				if(npc.damage != 0) 
				{
					if((npc.realLife >= 0 && npc.realLife == npc.whoAmI) || npc.realLife < 0) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, nump8, nump9, mod.ProjectileType("InvokedDamage"), npc.damage * 20, 0f, Main.player[Main.myPlayer].whoAmI, num6, 0f);
				}
				if(npc.GetGlobalNPC<InvokedGlobalNPC>().CaligulaSoulFight && !Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().DarkCaligula && (npc.type == mod.NPCType("ZeroProtocol") || npc.type == mod.NPCType("YamataA") || npc.type == mod.NPCType("AkumaA") || npc.type == mod.NPCType("ShenA") || npc.type == mod.NPCType("SupremeRajah")))
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, nump8, nump9, mod.ProjectileType("InvokedDamage"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, npc.type);
				}
			}
			return true;
		}
		
	}

	public class InvokedDamage : ModProjectile
	{
		public override void SetDefaults()
        {
           	projectile.width = 6;
			projectile.height = 6;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
        }

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.DarkBlue;
        }
		private int time = 0;
		public override void AI()
        {
			time += 1;
			if (time >= 60)
			{
				if(projectile.ai[1] == 0f)
				{
					projectile.friendly = true;
					int num568 = (int)projectile.ai[0];
					if (!Main.npc[num568].active)
					{
						int[] array2 = new int[200];
						int num569 = 0;
						for (int num570 = 0; num570 < 200; num570 ++)
						{
							if (Main.npc[num570].CanBeChasedBy(this, true))
							{
								float num571 = Math.Abs(Main.npc[num570].position.X + Main.npc[num570].width / 2 - projectile.position.X + projectile.width / 2) + Math.Abs(Main.npc[num570].position.Y + Main.npc[num570].height / 2 - projectile.position.Y + projectile.height / 2);
								if (num571 < 800f)
								{
									array2[num569] = num570;
									num569 ++;
								}
							}
						}
						if (num569 == 0)
						{
							projectile.Kill();
							return;
						}
						num568 = array2[Main.rand.Next(num569)];
						projectile.ai[0] = num568;
					}
					float num572 = 4f;
					Vector2 vector44 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
					float num573 = Main.npc[num568].Center.X - vector44.X;
					float num574 = Main.npc[num568].Center.Y - vector44.Y;
					float num575 = (float)Math.Sqrt(num573 * num573 + num574 * num574);
					num575 = num572 / num575;
					num573 *= num575;
					num574 *= num575;
					int num576 = 30;
					projectile.velocity.X = (projectile.velocity.X * (num576 - 1) + num573) / num576;
					projectile.velocity.Y = (projectile.velocity.Y * (num576 - 1) + num574) / num576;
				}
				else
				{

					int num492 = (int)projectile.ai[0];
					float num493 = 4f;
					Vector2 vector39 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
					float num494 = Main.player[num492].Center.X - vector39.X;
					float num495 = Main.player[num492].Center.Y - vector39.Y;
					float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
					if (num496 < 50f && projectile.position.X < Main.player[num492].position.X + Main.player[num492].width && projectile.position.X + projectile.width > Main.player[num492].position.X && projectile.position.Y < Main.player[num492].position.Y + Main.player[num492].height && projectile.position.Y + projectile.height > Main.player[num492].position.Y)
					{
						if (projectile.owner == Main.myPlayer)
						{
							Player player = Main.player[num492];
							player.GetModPlayer<InvokerPlayer>().CaligulaSoul.Add((int)projectile.ai[1]) ;
							CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.DarkGray, Language.GetTextValue("Mods.AAMod.Common.CaligulaSoul"), false, false);
						}
						projectile.Kill();
					}
					num496 = num493 / num496;
					num494 *= num496;
					num495 *= num496;
					projectile.velocity.X = (projectile.velocity.X * 15f + num494) / 16f;
					projectile.velocity.Y = (projectile.velocity.Y * 15f + num495) / 16f;
				}
			}
			for (int num577 = 0; num577 < 5; num577 ++)
			{
				float num578 = projectile.velocity.X * 0.2f * num577;
				float num579 = -(projectile.velocity.Y * 0.2f) * num577;
				int num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 20, Color.DarkBlue, 2f);
				Main.dust[num580].noGravity = true;
				Main.dust[num580].velocity *= 0f;
				Main.dust[num580].position.X = Main.dust[num580].position.X - num578;
				Main.dust[num580].position.Y = Main.dust[num580].position.Y - num579;
			}
			return;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			double Realdamage = Main.CalculateDamage(projectile.damage, 0);

			Main.player[Main.myPlayer].dpsDamage += (int)Realdamage;

			Color damagecolor = crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;
			CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), damagecolor, (int)Realdamage, false, false);
			
			if (!target.immortal)
			{
				if (target.realLife >= 0)
				{
					Main.npc[target.realLife].life -= (int)Realdamage;
					target.life = Main.npc[target.realLife].life;
					target.lifeMax = Main.npc[target.realLife].lifeMax;
				}
				else
				{
					target.life -= (int)Realdamage;
				}
			}

			/* 
			if(target.life <= Realdamage) target.life -= target.life;
			else target.life -= (int)Realdamage;

			if(target.realLife >= 0)
			{
				if(Main.npc[target.realLife].life <= Realdamage) Main.npc[target.realLife].life -= Main.npc[target.realLife].life;
				else Main.npc[target.realLife].life -= (int)Realdamage;
			}
			*/

			if (target.realLife >= 0)
			{
				Main.npc[target.realLife].checkDead();
			}
			else
			{
				target.checkDead();
			}
		}
	}
	public class InvokedHeal : ModProjectile
	{
		public override void SetDefaults()
        {
           	projectile.width = 6;
			projectile.height = 6;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
        }
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.DarkRed;
        }
		public override void AI()
        {
			int num492 = (int)projectile.ai[0];
			float num493 = 4f;
			Vector2 vector39 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
			float num494 = Main.player[num492].Center.X - vector39.X;
			float num495 = Main.player[num492].Center.Y - vector39.Y;
			float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
			if (num496 < 50f && projectile.position.X < Main.player[num492].position.X + Main.player[num492].width && projectile.position.X + projectile.width > Main.player[num492].position.X && projectile.position.Y < Main.player[num492].position.Y + Main.player[num492].height && projectile.position.Y + projectile.height > Main.player[num492].position.Y)
			{
				if (projectile.owner == Main.myPlayer)
				{
					int num497 = (int)projectile.ai[1];
					Main.player[num492].HealEffect(num497, true);
					Player player = Main.player[num492];
					player.statLife += num497;
					if (Main.player[num492].statLife > Main.player[num492].statLifeMax2)
					{
						Main.player[num492].statLife = Main.player[num492].statLifeMax2;
					}
					NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, num492, num497, 0f, 0f, 0, 0, 0);
				}
				projectile.Kill();
			}
			num496 = num493 / num496;
			num494 *= num496;
			num495 *= num496;
			projectile.velocity.X = (projectile.velocity.X * 15f + num494) / 16f;
			projectile.velocity.Y = (projectile.velocity.Y * 15f + num495) / 16f;
			for (int num502 = 0; num502 < 5; num502 ++)
			{
				float num503 = projectile.velocity.X * 0.2f * num502;
				float num504 = -(projectile.velocity.Y * 0.2f) * num502;
				int num505 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 20, Color.OrangeRed, 1.3f);
				Main.dust[num505].noGravity = true;
				Main.dust[num505].velocity *= 0f;
				Main.dust[num505].position.X = Main.dust[num505].position.X - num503;
				Main.dust[num505].position.Y = Main.dust[num505].position.Y - num504;
			}
			return;
		}
	}
	public class InvokedRune : ModProjectile
	{
		public override void SetDefaults()
        {
			projectile.width = 86;
			projectile.height = 86;
			projectile.hostile = false;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 200;
			projectile.damage = 0;
        }

		private int count = 0;

		public override void AI()
        {
			Lighting.AddLight(projectile.Center, (Main.DiscoR -projectile.alpha) * 0.8f / 255f, (Main.DiscoG -projectile.alpha) * 0.4f / 255f, (Main.DiscoB -projectile.alpha) * 0f / 255f);
			
			if(count < 13)
			{
				projectile.alpha -= 20;
			}
			else if(count >= 13)
			{
				projectile.alpha += 3;
				if(projectile.alpha >= 250)
				{
					projectile.Kill();
				}
			}
			
			count ++;

			int numa = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 30, Color.OrangeRed, 1.3f);
			Main.dust[numa].noGravity = true;
			Main.dust[numa].alpha ++;
			
			projectile.damage = 0;
			projectile.netUpdate = true;

			if(projectile.ai[0] == 1f)
			{
				int num9 = (int)projectile.ai[1];
				if(Main.npc[num9].active)
				{
					projectile.velocity = (Main.npc[num9].Center - projectile.Center) * 0.75f;
					projectile.StatusNPC(num9);
					projectile.Center = Main.npc[num9].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.npc[num9].gfxOffY;
				}
				else if (num9 < 0 || num9 >= 200)
				{
					projectile.Kill();
				}
				else
				{
					projectile.Kill();
				}
				
				if(!Main.npc[num9].active && Main.npc[num9].life <= 0)
				{
					projectile.Kill();
				}
			}
			else
			{
				int num9 = (int)projectile.ai[1];
				if(Main.player[num9].active)
				{
					projectile.velocity = (Main.player[num9].Center - projectile.Center) * 0.75f;
					projectile.Center = Main.player[num9].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.player[num9].gfxOffY;
				}
				else if (num9 < 0 || num9 >= 200)
				{
					projectile.Kill();
				}
				else
				{
					projectile.Kill();
				}
				
				if(!Main.player[num9].active)
				{
					projectile.Kill();
				}
			}
			
			
		}
	}
}