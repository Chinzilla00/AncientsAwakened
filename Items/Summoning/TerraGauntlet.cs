using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Summoning
{
    public class TerraGauntlet : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 80;
            item.noMelee = true;
            item.summon = true;
            item.width = 18;
            item.height = 42;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("Minion1");
            item.buffType = mod.BuffType("TerraSummon");
            item.knockBack = 2;
            item.rare = 8;
            item.UseSound = SoundID.Item44;
            item.autoReuse = false;
            item.shootSpeed = 1f;
            item.mana = 10;
        }
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Gauntlet");
            Tooltip.SetDefault(@"Summons a Terra Squid, Terra Sphere, Terra Crawler, or Terra Weaver to Fight for you");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
			
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			modPlayer.TerraSummon = true;
			player.AddBuff(mod.BuffType("TerraSummon"), 2, true);

			Vector2 point = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);

            int shootMe = Main.rand.Next(3);
            switch (shootMe)
            {
                case 0:
                    shootMe = mod.ProjectileType("Minion1");
                    break;
                case 1:
                    shootMe = mod.ProjectileType("Minion2");
                    break;
                case 2:
                    shootMe = mod.ProjectileType("Minion3");
                    break;
            }

            int i = Main.myPlayer;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            int num78 = 0;
            int num79 = 0;
            Projectile.NewProjectile(point.X + Main.rand.Next(-50, 50), point.Y + Main.rand.Next(-50, 50), num78, num79, shootMe, num73, num74, i, 0f, 0f);

            return false;
        }
    }
}
