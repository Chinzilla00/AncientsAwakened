using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class SubzeroStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Storm Staff");
            Tooltip.SetDefault(@"Blizzard Staff EX");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.mana = 11;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 220;
            item.useAnimation = 3;
            item.useTime = 3;
            item.width = 62;
            item.height = 62;
            item.shoot = mod.ProjectileType("SubzeroSnowflake");
            item.shootSpeed = 17f;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.magic = true;
            item.rare = ItemRarityID.Purple;
            item.noMelee = true;
            item.expert = true; item.expertOnly = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int num111 = 0; num111 < 2; num111++)
            {
                Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-200, 201);
                vector2.Y -= 100 * num111;
                float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
                float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num82 < 0f)
                {
                    num82 *= -1f;
                }
                if (num82 < 20f)
                {
                    num82 = 20f;
                }
                float num83 = (float)Math.Sqrt(num81 * num81 + num82 * num82);
                num83 = item.shootSpeed / num83;
                num81 *= num83;
                num82 *= num83;
                float speedX4 = num81 + Main.rand.Next(-40, 41) * 0.02f;
                float speedY5 = num82 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockBack, Main.myPlayer, 0f, Main.rand.Next(5));
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BlizzardStaff, 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}