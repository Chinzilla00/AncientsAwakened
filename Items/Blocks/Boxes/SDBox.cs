using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks.Boxes
{
    public class SDBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sleeping Dragon Music Box");

            Tooltip.SetDefault(@"Plays 'Sleeping Giants' by LordCakeSpy");
        }

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SDBox");
            item.width = 72;
			item.height = 36;
            item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            item.value = 10000;
			item.accessory = true;
            item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PagodaBox");
            recipe.AddIngredient(null, "LakeBox");
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
