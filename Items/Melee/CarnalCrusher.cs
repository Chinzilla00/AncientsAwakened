using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.World.Generation;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class CarnalCrusher : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 110;
            item.melee = true;
            item.width = 100;
            item.height = 98;
            item.useTime = 45;
            item.useAnimation = 45;     
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 200000;        
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carnal Crusher");
            Tooltip.SetDefault("Steals enemy life on hit\nReleases bloody flares on enemy hit");
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("DiscordLight"));
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			player.statLife += 2;
			player.HealEffect(2, true);
			{
				int[] numArray1 = new int[200];
				int maxValue1 = 0;
				int[] numArray2 = new int[200];
				int maxValue2 = 0;
				for (int index = 0; index < 200; ++index)
				{
					if (Main.npc[index].CanBeChasedBy((object) this, false))
					{
						float num2 = Math.Abs((float) (Main.npc[index].position.X + (double) (Main.npc[index].width / 2) - target.position.X) + (float) (target.width / 2)) + Math.Abs((float) (Main.npc[index].position.Y + (double) (Main.npc[index].height / 2) - target.position.Y) + (float) (target.height / 2));
						if ((double) num2 < 800.0)
						{
							if (Collision.CanHit(target.position, 1, 1, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height) && (double) num2 > 50.0)
							{
								numArray1[maxValue2] = index;
								++maxValue2;
							}
							else if (maxValue2 == 0)
							{
								numArray1[maxValue1] = index;
								++maxValue1;
							}
						}
					}
				}
				if (maxValue1 == 0 && maxValue2 == 0)
				return;
				for (int l = 0; l < 3; l++)
				{
					int num3 = maxValue2 <= 0 ? numArray1[Main.rand.Next(maxValue1)] : numArray1[Main.rand.Next(maxValue2)];
					double num4 = 4.0;
					float num5 = (float) Main.rand.Next(-100, 101);
					float num6 = (float) Main.rand.Next(-100, 101);
					double num7 = Math.Sqrt((double) num5 * (double) num5 + (double) num6 * (double) num6);
					float num8 = (float) (num4 / num7);
					float SpeedX = num5 * num8;
					float SpeedY = num6 * num8;
					int proj = Projectile.NewProjectile((float) target.position.X, (float) target.position.Y, SpeedX, SpeedY, mod.ProjectileType("BloodyFlare"), damage, 0.0f, Main.myPlayer, (float) num3, 0.0f);
				}
			}
		}
		
        public override void AddRecipes()  //How to craft this sword
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NightsEdge);
			recipe.AddIngredient(ItemID.LunarTabletFragment, 30);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
