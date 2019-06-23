using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah
{
    public class CottonCane : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cotton Cane");
            Tooltip.SetDefault(@"Summons a Rabbitcopter Soldier to fight with you");
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.summon = true;
            item.mana = 10;
            item.width = 26;
            item.height = 28;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.rare = 8;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType<Projectiles.Rajah.RabbitcopterSoldier>();
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType<Buffs.RabbitcopterSoldier>();
            item.buffTime = 3600;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
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
            num78 = 0f;
            num79 = 0f;
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, item.shoot, num73, num74, i, 0f, 0f);
            return false;
        }
    }
}