using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ChaosSlayerEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ikari");
            Tooltip.SetDefault(@"Unleashes explosive blades of chaos to smite your foes
blades go through tiles
Chaos Slayer EX");
        }

        public override void SetDefaults()
        {
            item.width = 85;
            item.height = 85;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 40;
            item.useTime = 40;
            item.UseSound = SoundID.Item103;
            item.damage = 666;
            item.knockBack = 12;
            item.melee = true;
            item.expert = true; item.expertOnly = true;
            item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChaosSlayerSwordEX");
			item.shootSpeed = 7;
            item.useTurn = true;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity14;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 shootPos, ref float speedX, ref float speedY, ref int projType, ref int damage, ref float knockback)
        {
			Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX, speedY, projType, damage, knockback, player.whoAmI);
			for (int m = 0; m < 2; m++)
			{
				Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX * 1f, speedY * 1f, m == 0 ? mod.ProjectileType("ChaosSlayerSwordRedEX") : mod.ProjectileType("ChaosSlayerSwordBlueEX"), damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosSlayer");
            recipe.AddIngredient(null, "PerfectChaos");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}