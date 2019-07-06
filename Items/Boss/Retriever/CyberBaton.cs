using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Retriever
{
    public class CyberBaton : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyber Baton");
            Tooltip.SetDefault(@"Summons a cyber claw to fight with you");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("CyberBaton");
            item.damage = 40;
            item.width = 52;
            item.height = 52;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 25;
            item.useTime = 25;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.knockBack = 5f;
            item.rare = 3;
            item.summon = true;
            item.mana = 5;
            item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int shootMe = mod.ProjectileType("CyberClaw");
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, shootMe, damage, 5, item.owner, 0f, 0f);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ClawBaton", 1);
            recipe.AddIngredient(null, "FulguriteBar", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}