using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class YourBestNightmare : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Your Best Nightmare");
            Tooltip.SetDefault(@"Surrounds your enemies in small white pellets that are difficult to avoid 
Right Clicking will spawn a streak of random nightmare energy
Each nightmare streak inflicts a different debuff
Your Best Friend EX");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.magic = true;
			item.mana = 8;
			item.width = 76;
			item.height = 80;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 1000000;
			item.rare = 11;
            item.expert = true;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Pellet");
			item.shootSpeed = 12f;
		}

        private bool shootPellet = false;

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                shootPellet = false;
                int Shoot = Main.rand.Next(4);

                switch (Shoot)
                {
                    case 0:
                        Shoot = mod.ProjectileType("Nightmare1");
                        break;
                    case 1:
                        Shoot = mod.ProjectileType("Nightmare2");
                        break;
                    case 2:
                        Shoot = mod.ProjectileType("Nightmare3");
                        break;
                    default:
                        Shoot = mod.ProjectileType("Nightmare4");
                        break;
                }
                item.shoot = Shoot;
            }
            else
            {
                shootPellet = true;
                item.shoot = mod.ProjectileType("Pellet");
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            int i = Main.myPlayer;
            float num72 = item.shootSpeed;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            if (shootPellet)
            {
                Projectile.NewProjectile(vector2.X + 150, vector2.Y, num78 - 5, num79, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X + 150, vector2.Y + 150, num78 - 5, num79 - 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X, vector2.Y + 150, num78, num79 - 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X - 150, vector2.Y + 150, num78 + 5, num79 - 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X - 150, vector2.Y, num78 + 5, num79, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X - 150, vector2.Y - 150, num78 + 5, num79 + 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X, vector2.Y - 150, num78, num79 + 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                Projectile.NewProjectile(vector2.X + 150, vector2.Y - 150, num78 - 5, num79 + 5, mod.ProjectileType("Pellet"), num73, num74, i, 0f, 0f);
                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YourBestFriend", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}