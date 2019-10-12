using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Imp
{
    [AutoloadEquip(EquipType.Head)]
    public class ImpHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imp Hood");
            Tooltip.SetDefault("7% Increased Minion damage \n" + "+1 Minion slot");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 7000;
            item.rare = 2;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.07f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ImpGarb") && legs.type == mod.ItemType("ImpBoots");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Lang.ArmorBonus("ImpHoodBonus");

            player.GetModPlayer<AAPlayer>().impSet = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.Loom);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}