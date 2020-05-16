using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Djinn
{
    public class SandstormCrossbow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Crossbow");
            Tooltip.SetDefault("Replaces arrows with desert bolts");
        }

	    public override void SetDefaults()
	    {
	        item.damage = 28;
	        item.ranged = true;
	        item.width = 40;
	        item.height = 26;
	        item.useTime = 19;
	        item.reuseDelay = 0;
	        item.useAnimation = 19;
	        item.useStyle = ItemUseStyleID.HoldingOut;
	        item.noMelee = true;
	        item.knockBack = 2.5f;
	        item.value = 50000;
	        item.rare = ItemRarityID.Orange;
	        item.UseSound = SoundID.Item5;
	        item.autoReuse = true;
	        item.shoot = ProjectileID.PurificationPowder;
	        item.shootSpeed = 8f;
	        item.useAmmo = 40;
	    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int Shoot = Main.rand.Next(2);
            switch (Shoot)
            {
                case 0:
                    Shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Djinn.DesertBolt1>();
                    break;
                default:
                    Shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Djinn.DesertBolt2>();
                    break;
            }
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, Shoot, damage, knockBack, player.whoAmI, 0f, 0f);
        
            return false;
        }
	}
}