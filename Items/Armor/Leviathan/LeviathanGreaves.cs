using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.Items.Armor.Leviathan
{
    [AutoloadEquip(EquipType.Legs)]
	public class LeviathanGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leviathan Greaves");
            Tooltip.SetDefault(@"8% increased movement speed
12% increased ranged critical strike chance
20% increased chance to not consume ammo
Enemies are less likely to target you
It smells like fish.");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 16;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 15;
            item.rare = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .08f;
            player.rangedCrit += 12;
            player.ammoCost80 = true;
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