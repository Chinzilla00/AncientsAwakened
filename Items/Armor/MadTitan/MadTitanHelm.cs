using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.MadTitan
{
    [AutoloadEquip(EquipType.Head)]
	public class MadTitanHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mad Titan's Helm");
			Tooltip.SetDefault(@"+7 max minions 
160 Increased max mana");

		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 14;
			item.value = 3000000;
            item.rare = 9;
            AARarity = 14;
            item.defense = 40;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.maxMinions += 6;
            player.statManaMax2 += 160;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:DarkmatterHelmets");
            recipe.AddRecipeGroup("AAMod:RadiumHelmets");
            recipe.AddIngredient(null, "DreadScale", 20);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}