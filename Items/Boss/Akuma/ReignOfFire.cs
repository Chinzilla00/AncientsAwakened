using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma   //where is located
{
    public class ReignOfFire : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Reign of Fire");
            Tooltip.SetDefault(@"Rains fire and fury upon your foes
Inflicts Daybroken");
        }

        
        public override void SetDefaults()
        {
            item.damage = 380;
            item.melee = true;
            item.width = 86;
            item.height = 86;
            item.useTime = 120;
            item.useAnimation = 120;     
            item.useStyle = 1;
            item.knockBack = 6.5f;
            item.value = Item.sellPrice(0, 30, 0, 0);
			item.UseSound = SoundID.Item20;
            item.autoReuse = true;
			item.useTurn = true;
            item.rare = 9;
            AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 46, new Color(255, 75, 0), 1.381579f)];
                dust.noGravity = true;
            }
        }

		public override bool UseItem(Player player)
		{
			if (Main.rand.NextBool(5))
			{
				Main.PlaySound(2, player.Center, 124);
				for (int num120 = 0; num120 < 1; num120++)
				{
					Vector2 vector12 = new Vector2(0,0);
					if (player.direction == 1)
					{
						vector12 = new Vector2(player.Center.X + Main.rand.Next(150,300), player.Center.Y);
					}
					if (player.direction == -1)
					{
						vector12 = new Vector2(player.Center.X - Main.rand.Next(150,300), player.Center.Y);
					}
					Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
					float num75 = 20f;
					float num119 = vector12.Y;
					if (num119 > player.Center.Y - 200f)
					{
						num119 = player.Center.Y - 200f;
					}
					vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
					vector2.Y -= 100;
					Vector2 vector13 = vector12 - vector2;
					if (vector13.Y < 0f)
					{
						vector13.Y *= -1f;
					}
					if (vector13.Y < 20f)
					{
						vector13.Y = 20f;
					}
					vector13.Normalize();
					vector13 *= num75;
					float num82 = vector13.X;
					float num83 = vector13.Y;
					float speedX5 = num82;
					float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
					Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("FireProj"), item.damage/5, item.knockBack, Main.myPlayer);
				}
			}
			return base.UseItem(player);
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
        
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.StarWrath);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
