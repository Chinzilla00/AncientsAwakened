using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class DarkmatterKunai : BaseAAItem
	{
		public override void SetDefaults()
		{

            item.damage = 60;            
            item.ranged = true;
            item.width = 20;
            item.height = 20;
			item.useTime = 8;
            item.maxStack = 999;
			item.useAnimation = 8;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 0;
			item.shootSpeed = 15f;
			item.shoot = mod.ProjectileType ("DMK");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.consumable = true;
            item.noMelee = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Kunai");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkEnergy", 1);
            recipe.AddIngredient(null, "DarkMatter");
		    recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
		}
    }
}
