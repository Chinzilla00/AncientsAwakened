using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class StarStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Staff");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 170;
            item.magic = true;
            item.mana = 6;
            item.width = 64;
            item.height = 64;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 100000;
            item.rare = 11;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Stars");
            item.shootSpeed = 7f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int num111 = 0; num111 < 2; num111++)
            {
                Vector2 vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
                vector2.Y -= (float)(100 * num111);
                float num81 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
                float num82 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num82 < 0f)
                {
                    num82 *= -1f;
                }
                if (num82 < 20f)
                {
                    num82 = 20f;
                }
                float num83 = (float)Math.Sqrt((double)(num81 * num81 + num82 * num82));
                num83 = item.shootSpeed / num83;
                num81 *= num83;
                num82 *= num83;
                float speedX4 = num81 + (float)Main.rand.Next(-40, 41) * 0.02f;
                float speedY5 = num82 + (float)Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX4, speedY5, mod.ProjectileType("Stars"), damage, knockBack, item.owner, 0f, (float)Main.rand.Next(5));
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}