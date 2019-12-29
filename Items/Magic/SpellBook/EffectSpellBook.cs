using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.DataStructures;
using Terraria.GameInput;
using AAMod.Items;

namespace AAMod.Items.Magic.SpellBook
{
    public abstract class EffectSpellBook : BaseAAItem
	{
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if(ExtraShoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack))
			{
				int k = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				Main.projectile[k].magic = true;
				Main.projectile[k].noDropItem = true;
				Main.projectile[k].friendly = true;
				Main.projectile[k].hostile = false;
			}
			return false;
		}
		public virtual bool ExtraShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return true;
		}
		public virtual void Instanteffect(Player player, Vector2 position, Vector2 velocity, int damage)
		{
		}
		public virtual void Sustainedeffect(Player player)
		{
		}
		public virtual void Instanteffect(Projectile projectile)
		{
		}
		public virtual void Sustainedeffect(Projectile projectile)
		{
		}
	}

	public class spellbookplayer : ModPlayer
	{
		public bool effectSpellBook = false;
		public bool effectRagnarok = false;
		public List<Item> ShootSustainedeffect = new List<Item>();
		public int spellbooknum = 0;
		public bool SpellBookofIridescen = false;

		public override void ResetEffects()
        {
			effectSpellBook = false;
			effectRagnarok = false;
		}

		public override void PostUpdate()
		{
			if(effectSpellBook)
			{
				foreach(Item item in ShootSustainedeffect)
				{
					if(item.modItem != null)
					{
						ModItem moditem = ItemLoader.GetItem(item.type);
						MethodInfo[] methods = moditem.GetType().GetMethods();
						try
						{
							foreach(MethodInfo method in methods)
							{
								if(method.Name == "Sustainedeffect")
								{
									MethodInfo Sustainedeffect = moditem.GetType().GetMethod("Sustainedeffect", BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Any, new Type[]{typeof(Player)}, null);
									Sustainedeffect.Invoke(moditem, new object[]{player});
								}
							}
						}
						catch
						{

						}
					}
				}
			}
			else
			{
				ShootSustainedeffect.Clear();
			}
			if(!effectRagnarok)
			{
				spellbooknum = 0;
			}
		}

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if(SpellBookofIridescen)
			{
				if(damage > player.statLife)
				{
					SpellBookofIridescen = false;
					player.immune = true;
					player.immuneTime = 30;
					return false;
				}
			}
			return true;
		}
	}
	public class spellbookproj : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public Vector2 vanillaoldvelocity = Vector2.Zero;
		private Vector2 oldvelocity = Vector2.Zero;
		private int oldspriteDirection = 1;
		public float ExtraAI0 = 0f;
		public float ExtraAI1 = 1f;
		public int SpellbookprojCounter = 0;
		public int SplitCounter = 0;
		public bool SpellBookofNecro = false;
		public bool SpellBookofCursim = false;
		public bool SpellbookofAlman = false;

		public override bool PreAI(Projectile projectile)
		{
			if(vanillaoldvelocity == Vector2.Zero) vanillaoldvelocity = projectile.velocity;
			return base.PreAI(projectile);
		}
		public override void PostAI(Projectile projectile)
		{
			if(projectile.owner == Main.myPlayer && projectile.magic && projectile.friendly)
			{
				oldvelocity = projectile.velocity;
				oldspriteDirection = projectile.spriteDirection;
				Player player = Main.player[projectile.owner];
				if(player.GetModPlayer<spellbookplayer>().effectSpellBook)
				{
					foreach(Item item in player.GetModPlayer<spellbookplayer>().ShootSustainedeffect)
					{
						if(item.modItem != null)
						{
							ModItem moditem = ItemLoader.GetItem(item.type);
							MethodInfo[] methods = moditem.GetType().GetMethods();
							try
							{
								foreach(MethodInfo method in methods)
								{
									if(method.Name == "Sustainedeffect")
									{
										MethodInfo Sustainedeffect = moditem.GetType().GetMethod("Sustainedeffect", BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.Any, new Type[]{typeof(Projectile)}, null);
										projectile.rotation += projectile.velocity.ToRotation() - oldvelocity.ToRotation();
										if(oldspriteDirection != projectile.spriteDirection)
										{
											projectile.rotation -= MathHelper.ToRadians(90f);
										}
										Sustainedeffect.Invoke(moditem, new object[]{projectile});
									}
								}
							}
							catch
							{
							}
						}
					}
					SpellbookprojCounter++;
				}
				else
				{
					SpellbookprojCounter = 0;
				}
			}
			if(SpellBookofCursim)
			{
				Player player = Main.player[projectile.owner];
				if(player.GetModPlayer<spellbookplayer>().effectSpellBook)
				{
					SpellBookofCursim = false;
				}
			}
			vanillaoldvelocity = projectile.velocity;
		}

		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if(SpellBookofNecro && projectile.type != 356)
			{
				int num = Main.rand.Next(2, 4);
				for(int i = 0; i < num; i++)
				{
					projectile.ghostHurt(projectile.damage, new Vector2(target.Center.X, target.Center.Y));
				}
			}
			if(SpellBookofCursim)
			{
				damage = (int)(damage * 1.5f + target.defense * (Main.expertMode? 0.75f:0.5f));
			}
		}
	}
}
