using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class HorusCane : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horus' Cane");
            Tooltip.SetDefault(@"Summons a Horus Eye sentry");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("HorusEye");
            item.damage = 250;
            item.width = 50;
            item.height = 50;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 0, 27, 0);
            item.knockBack = 7.5f;
            item.summon = true;
            item.mana = 30;
            item.sentry = true;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            int num74 = item.shoot;
            int num76 = item.damage;
            float num77 = item.knockBack;
            int num154 = (int)(Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)(Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + Main.screenHeight - Main.mouseY) / 16;
            }
			if (player.ownedProjectileCounts[num74] < player.maxTurrets)
			{
				Projectile.NewProjectile(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y, 0f, 0f, num74, num76, num77, i, 0f, 0f);
			}
			if (player.ownedProjectileCounts[num74] == player.maxTurrets)
			{
				for(int g = 0; g < 1000; ++g)
				{
					if(Main.projectile[g].active && Main.projectile[g].type == num74)
					{
						Main.projectile[g].Kill();
						break;
					}
				}
				 Projectile.NewProjectile(Main.mouseX + Main.screenPosition.X, num155 * 16 - 24, 0f, 0f, num74, num76, num77, i, 0f, 0f);
			}

            return false;
        }
    }
}