using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class DwarvenGauntlet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.18f;
            player.meleeSpeed += 0.18f;
            player.aggro += 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dwarven Gauntlet");
            Tooltip.SetDefault(
@"Enemies are much more likely to target you
18% Increased Melee Damage and Speed
Increased Melee Knockback
Having this gauntlet allows you to handle the infinity stones without overloading
'Fine. I'll do it myself.'");
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().dwarvenGauntlet = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DemonGauntlet", 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 5);
                recipe.AddIngredient(ItemID.FragmentSolar, 5);
                recipe.AddIngredient(ItemID.FragmentVortex, 5);
                recipe.AddIngredient(ItemID.FragmentStardust, 5);
                recipe.AddIngredient(null, "DarkMatter", 10);
                recipe.AddIngredient(null, "RadiumBar", 10);
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }

    }
}