using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class AmphibianLongswordS : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amphibious Longsword");
			Tooltip.SetDefault(@"So I heard you like getting hosed. 
-Alphakip");
		}
		public override void SetDefaults()
		{
			item.damage = 290;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 300000;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AmphibiousProjectileS");
            item.shootSpeed = 9f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(39, 115, 189);
                }
            }
        }
        
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Wet, 1000);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AmphibianLongsword");
            recipe.AddIngredient(null, "ShinyCharm");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
