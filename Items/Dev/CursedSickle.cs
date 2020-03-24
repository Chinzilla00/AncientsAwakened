using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Dev
{
    public class CursedSickle : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Cursed Sickle");
            Tooltip.SetDefault(@"Spins a cursed scythe around you that shreds through enemies
Left click to swing the scythe");			
		}

		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.rare = 9;
            item.value = BaseUtility.CalcValue(0, 5, 0, 0);
            item.UseSound = SoundID.Item71;
            item.useStyle = 1;
            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 130;
            item.knockBack = 4;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("CursedSickle");
            item.shootSpeed = 0.1f;
            item.melee = true;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(29, 109, 124);
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
                item.noMelee = false;
                item.noUseGraphic = false;
                item.shoot = mod.ProjectileType("CursedSickleProj");
                item.shootSpeed = 7f;
            }
            else
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.shoot = mod.ProjectileType("CursedSickle");
                item.shootSpeed = 0.1f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("CursedSickleEffect"), damage, knockBack, player.whoAmI, k, 0f);
			}
			return true;
		}
    }
}