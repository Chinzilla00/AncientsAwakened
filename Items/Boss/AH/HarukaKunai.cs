using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HarukaKunai : BaseAAItem
    {
		public override void SetDefaults()
		{
			item.damage = 140;
			item.ranged = true;
			item.width = 14;
			item.height = 34;
			item.noUseGraphic = true;
			item.useTime = 8;
			item.useAnimation = 8;
			item.shoot = mod.ProjectileType("HarukaKunaiF");
			item.shootSpeed = 15f;
			item.useStyle = 1;
			item.knockBack = 0;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Kunai");
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
            float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                int proj = Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
                Main.projectile[proj].ranged = false;
                Main.projectile[proj].magic = true;
            }
            return false;
        }
    }
}
