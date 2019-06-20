using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class FuryFlame : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Flame");
            Tooltip.SetDefault("Allows you to blast explosive flames at your foes");
        }

        public override void SetDefaults()
        {
            item.damage = 140;
            item.noMelee = true;
            item.ranged = true;
            item.width = 64;
            item.height = 46;
            item.useTime = 2;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("FuryFlame");
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 9;
            AARarity = 12;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 7f;
            item.noUseGraphic = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, item.owner, 7f);
            return false;
        }
    }
}
