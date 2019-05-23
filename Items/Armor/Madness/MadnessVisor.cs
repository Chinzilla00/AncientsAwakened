using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Madness
{
    [AutoloadEquip(EquipType.Head)]
    public class MadnessVisor : ModItem
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
            item.defense = 5; //8
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MadnessPlate") && legs.type == mod.ItemType("MadnessBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+5% damage";
            player.meleeDamage += .05f;
            player.rangedDamage += .05f;
            player.magicDamage += .05f;
            player.minionDamage += .05f;
            player.thrownDamage += .05f;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .04f;
            player.rangedDamage += .04f;
            player.magicDamage += .04f;
            player.minionDamage += .04f;
            player.thrownDamage += .04f;
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