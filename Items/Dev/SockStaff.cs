using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class SockStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sock Puppet Staff");
            Tooltip.SetDefault(@"Summons a sock puppet to fight with you");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("SockPuppet");
            item.damage = 130;
            item.width = 60;
            item.height = 56;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.knockBack = 5f;
            item.rare = ItemRarityID.Yellow;
            item.summon = true;
            item.mana = 20;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(89, 119, 71);
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, mod.ProjectileType("SockPuppet"), damage, num74, i, 0f, 0f);
            return false;
        }
    }
}