using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.Items.Armor.Leviathan
{
    [AutoloadEquip(EquipType.Body)]
	public class LeviathanPlate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Leviathan Plate");
            Tooltip.SetDefault(@"80 Increased maximum mana
7% increased magic critical strike chance
10% decreased mana usage
Enemies are less likely to target you
It smells like fish.");
        }


        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
            AARarity = 14;
            item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
            player.statManaMax2 += 80 ;
            player.magicCrit += 7;
            player.manaCost *= .9f;
            player.aggro -= 2;
        }
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FishronScale", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}