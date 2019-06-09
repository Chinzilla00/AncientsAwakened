using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    public class ClawBaton : ModItem
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
            item.useAnimation = 25;
            item.useTime = 25;
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
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, shootMe, damage, 5, item.owner, 0f, 0f);
            return true;
        }
    }
}