using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Hood");
            Tooltip.SetDefault(@"Increases maximum mana by 100
Increases magic damage by 17%
Increases magic crit by 15%");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 90000;
            item.rare = 7;
            item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.manaCost -= 0.3f;
            player.magicDamage += 0.17f;
            player.magicCrit += 15;
            player.statManaMax2 += 100;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TerraPlate") && legs.type == mod.ItemType("TerraGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"You can autodrink mana potions
Mana usage lowered by 40%
Pressing the Armor ability hotkey causes 2 terra rose stalks to grow around you.
Enemies struck by the stalks drop mana stars";

            player.manaCost *= 0.6f;
            player.manaFlower = true;
            player.GetModPlayer<AAPlayer>().TerraMa = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TribalHat", 1);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}