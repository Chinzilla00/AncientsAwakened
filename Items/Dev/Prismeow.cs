using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class Prismeow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Prismeow");
            Tooltip.SetDefault(@"Fires rainbow cats
'Godly'
-Hallam");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 180;
			item.magic = true;
			item.mana = 6;
			item.width = 58;
			item.height = 58;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 1000000;
			item.rare = 11;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ProjectileID.Meowmere;
			item.shootSpeed = 10f;
		}


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = Main.rand.Next(20, 30) * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                float randomSpeed = baseSpeed + Main.rand.NextFloat() * 1.5f;
                offsetAngle = startAngle + (deltaAngle * i);
                int shoot = Projectile.NewProjectile(position.X, position.Y, randomSpeed * (float)Math.Sin(offsetAngle), randomSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
                Main.projectile[shoot].melee = false;
                Main.projectile[shoot].magic = true;
            }
            return false;
        }
		
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 8, 251);
                }
            }
        }
	}
}