using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class StarStaff : ModItem
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
            for (int i = 0; i < 3; i++)
            {
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                float MouseX = Main.mouseX + Main.screenPosition.X + vector2.X;
                float MouseY = Main.mouseY + Main.screenPosition.Y + vector2.Y;
                vector2 = new Vector2(MouseX + Main.rand.Next(-200, 200), MouseY + Main.rand.Next(-200, 200));
                Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, type, damage, knockBack, Main.myPlayer, 0f, (float)Main.rand.Next(3));
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