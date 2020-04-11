using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraVisor : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Visor");
            Tooltip.SetDefault(@"24% Increased ranged damage
25% Reduced Ammo Consumption
Grants hunter & night vision");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.value = 90000;
            item.rare = 7;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.24f;
            player.rangedDamage += 0.24f;
            player.ammoCost75 = true;
            player.nightVision = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TerraPlate") && legs.type == mod.ItemType("TerraGreaves");
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.TerraVisorBonus");

            player.aggro -= 5;
            player.rangedCrit += 20;
            player.GetModPlayer<AAPlayer>().TerraRa = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DeathlySkull", 1);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}