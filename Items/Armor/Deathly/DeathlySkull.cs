using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Deathly
{
    [AutoloadEquip(EquipType.Head)]
    public class DeathlySkull : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Skull");
            Tooltip.SetDefault("9% Increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.value = 90000;
            item.rare = 4;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.09f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DeathlyRibguard") && legs.type == mod.ItemType("DeathlyGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DeathlySkullBonus");

            player.aggro -= 5;
            player.ammoCost80 = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpHood", 1);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(ItemID.ShadowScale, 5);
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpHood", 1);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(ItemID.TissueSample, 5);
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}