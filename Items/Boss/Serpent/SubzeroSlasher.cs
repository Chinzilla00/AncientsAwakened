using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class SubzeroSlasher : BaseAAItem
    {
        private static int shoot;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Slasher");
            Tooltip.SetDefault("Has a chance to shoot a subzero projectile on swing");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.melee = true;
            item.width = 56;
            item.height = 56;
            item.useTime = 21;
            item.useAnimation = 21;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ProjectileID.CrystalBullet;
            item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
        {
            shoot++;
            if (shoot % 2 != 0) return false;
            shoot = 0;
            Main.projectile[type].melee = true;
            Main.projectile[type].ranged = false;
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockback);
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 120);
        }
    }
}