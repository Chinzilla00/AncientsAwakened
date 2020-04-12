using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class Windfury : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windfury");
            Tooltip.SetDefault("Replaces wooden arrows with gale arrows with high knockback and infinite piercing");
        }

        public override void SetDefaults()
        {
            item.damage = 140; 
            item.noMelee = true;
            item.ranged = true;
            item.width = 26;
            item.height = 50;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 0;
            item.UseSound = SoundID.Item5;
            item.autoReuse = false;
            item.shootSpeed = 10f;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.Athena.GaleArrow>(), damage, knockBack * 5, player.whoAmI, 0f, 0f);
                return false;
            }
            return true;
        }
    }
}