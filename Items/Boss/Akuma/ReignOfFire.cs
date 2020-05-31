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
            item.useTime = 60;
            item.useAnimation = 60;     
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6.5f;
            item.value = Item.sellPrice(0, 30, 0, 0);
			item.UseSound = SoundID.Item20;
            item.autoReuse = true;
			item.useTurn = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
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
			if (Main.rand.NextBool(10))
			{
				Main.PlaySound(SoundID.Item, player.Center, 124);
				Vector2 vector12 = new Vector2(0,0);
				vector12 = new Vector2(Main.mouseX + Main.screenPosition.X, player.Center.Y);
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
				float speedY6 = num83 + Main.rand.Next(-30, 30) * 0.02f;
				int p = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("FireProj"), item.damage/2, item.knockBack, Main.myPlayer);
				switch (Main.rand.Next(5))
				{
					case 0: Main.projectile[p].ai[0] = 1f;
					break;
					case 1: Main.projectile[p].ai[0] = 2f;
					break;
					case 2: Main.projectile[p].ai[0] = 3f;
					break;
					case 3: Main.projectile[p].ai[0] = 4f;
					break;
					case 4: Main.projectile[p].ai[0] = 5f;
					break;
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
