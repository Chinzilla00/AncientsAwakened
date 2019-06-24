using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    public class ClawBaton : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Claw Baton");
            Tooltip.SetDefault(@"Summons a chaos claw to fight with you");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("ClawBaton");
            item.damage = 14;
            item.width = 52;
            item.height = 52;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.knockBack = 5f;
            item.rare = 3;
            item.summon = true;
            item.mana = 5;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int shootMe = Main.rand.Next(2);
            {
                switch (shootMe)
                {
                    case 0:
                        shootMe = mod.ProjectileType("HydraClaw");
                        break;
                    default:
                        shootMe = mod.ProjectileType("DragonClaw");
                        break;
                }
            }
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
            Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, shootMe, num73, num74, i, 0f, 0f);
            return false;
        }
    }
}