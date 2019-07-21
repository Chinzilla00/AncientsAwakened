using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChaosGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Greaves");
            Tooltip.SetDefault(@"10% increased movement speed
7% increased damage");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 16;
            item.defense = 22;
            item.rare = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .07f;
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(AAMod.instance);
            recipe.AddRecipeGroup("AAMod:ChaosBoots");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(null, "TruePaladinsSmeltery");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}