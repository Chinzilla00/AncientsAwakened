using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Melee
{
	public class YtriumHalberd : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yttrium Halberd");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Strikes foes in an arc, then stabs in the direction of the cursor"});			
		}
		
        public override void SetDefaults()
        {
            item.width = 35;
            item.height = 35;
            item.maxStack = 1;
            item.rare = 5;
            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);

            item.useStyle = 5;
            item.useAnimation = 35;
            item.useTime = 35;
            item.UseSound = SoundID.Item1;
            item.damage = 29;
            item.knockBack = 5;
            item.melee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = mod.ProjType("YtriumHalberd");
            item.shootSpeed = 4;			
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "YtriumBar", 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}