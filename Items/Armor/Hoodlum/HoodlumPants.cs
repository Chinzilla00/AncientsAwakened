using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.Items.Armor.Hoodlum
{
    [AutoloadEquip(EquipType.Legs)]
	public class HoodlumPants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hopping Hoodlum Paws");
            Tooltip.SetDefault(@"10% increased movement speed
9% increased melee critical strike chance
+1 Max Minion
Enemies are more likely to target you
Hopping Mad.");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 16;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.defense = 17;
            item.rare = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .08f;
            player.meleeCrit += 8;
            player.maxMinions += 1;
            player.aggro += 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RajahPelt", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}