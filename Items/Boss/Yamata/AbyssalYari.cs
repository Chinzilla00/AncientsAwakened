using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Boss.Yamata
{
    public class AbyssalYari : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Yari");
            Tooltip.SetDefault(@"One of two legendary spears used to divide time into day and night");
        }

        public override void SetDefaults()
        {
            item.damage = 350;
            item.melee = true;
            item.width = 132;
            item.height = 132;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 25;
            item.useAnimation = 25;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("AbyssalYariP");  
            item.autoReuse = true;
            item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		    float spread = 15f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY)) * 3f;
            double startAngle = Math.Atan2(speedX, speedY) - .5d;
		    double deltaAngle = spread;
		    double offsetAngle;
		    for (int i = 0; i < 2; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), mod.ProjectileType("AbyssalYariP3"), damage, knockBack, item.owner);
		    }
		    return true;
		}
    }
}
