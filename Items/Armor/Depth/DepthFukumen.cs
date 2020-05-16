using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Head)]
    public class DepthFukumen : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Depth Fukumen");
            Tooltip.SetDefault(@"25% increased movement speed
8% increased ranged damage
Weightless as shadow itself");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 7500;
            item.rare = ItemRarityID.Green;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += .08f;
            player.moveSpeed += .25f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DepthGi") && legs.type == mod.ItemType("DepthHakama");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DepthFukumenBonus");
            player.aggro -= 3;
            player.ammoCost80 = true;
            player.nightVision = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 15);
            recipe.AddIngredient(null, "HydraHide", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}