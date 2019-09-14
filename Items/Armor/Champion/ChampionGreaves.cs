using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Champion
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChampionGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Champion' Greaves");
            Tooltip.SetDefault(@"50% increased movement speed
15% increased damage
The armor of a champion feared across the land");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.defense = 30;
            item.rare = 9;
            AARarity = 14;
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

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .15f;
            player.moveSpeed += .5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HoodlumPants", 1);
            recipe.AddIngredient(null, "LeviathanGreaves", 1);
            recipe.AddIngredient(null, "ChampionPlate", 10);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}