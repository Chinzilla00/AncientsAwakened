using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class UmbralReaper : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Umbral Reaper");
            Tooltip.SetDefault("Right shoots homing spears. \n" + "Left clicking shoots a wave of void energy. \n" + "'I never touched Valkyrie' \n'" + "-CMD");
		}

		public override void SetDefaults()
		{
			item.damage = 130;
            item.mana = 8;
			item.magic = true;
			item.width = 72;
			item.height = 72;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 100000;
			item.rare = 9;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
            item.noMelee = true;
            item.shootSpeed = 12f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(55, 20, 122);
                }
            }
        }

        public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{

            if (player.altFunctionUse == 2)
            {
                Item.staff[item.type] = false;
                item.useStyle = 1;
                item.shoot = mod.ProjectileType("VoidWave");
            }
            else
            {
                Item.staff[item.type] = true;
                item.useStyle = 5;
                item.shoot = mod.ProjectileType("VoidSpear");
            }
            return base.CanUseItem(player);
		}

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            float spread = 20f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, item.owner);
            }
            return false;
        }
    }
}