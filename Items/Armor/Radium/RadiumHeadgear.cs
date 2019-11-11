using AAMod.Items.Armor.Darkmatter;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
    public class RadiumHeadgear : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Headgear");
            Tooltip.SetDefault(@"20% increased Ranged damage
Shines with the light of a starry night sky");

        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 14;
            item.value = 300000;
            item.rare = 11;
            item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.20f;
            player.AddBuff(BuffID.Shine, 2);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.RadiumHeadgearBonus");


            player.GetModPlayer<VisorEffects>().setBonus = true;
            player.GetModPlayer<VisorEffects>().sunPortal = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}