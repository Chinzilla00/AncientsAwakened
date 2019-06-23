using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class SoccStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Socc on a Stick");
            Tooltip.SetDefault(@"Summons a cotton god to fight for you
Only one Socc may exist. 
Any summons after one has been summoned will result in a regular Sock
Sock Puppet Staff EX");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("SoccMinion");
            item.damage = 240;
            item.width = 60;
            item.height = 56;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.knockBack = 5f;
            item.rare = 8;
            item.summon = true;
            item.mana = 20;
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
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 = 0f;
            num79 = 0f;
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            if (player.ownedProjectileCounts[mod.ProjectileType("SoccMinion")] > 0)
            {
                Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, mod.ProjectileType("SockPuppetEX"), num73, num74, i, 0f, 0f);
            }
            else
            {
                Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, mod.ProjectileType("SoccMinion"), (int)(num73 * 1.5f), num74, i, 0f, 0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SockStaff", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}