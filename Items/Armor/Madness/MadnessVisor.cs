using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Madness
{
    [AutoloadEquip(EquipType.Head)]
    public class MadnessVisor : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Visor");
            Tooltip.SetDefault("4% increased damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 20000;
            item.rare = 1;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MadnessPlate") && legs.type == mod.ItemType("MadnessBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Lang.ArmorBonus("MadnessVisorBonus");
            player.allDamage += .05f;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .04f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MadnessFragment", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}