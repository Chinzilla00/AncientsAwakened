using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Head)]
    public class DoomiteVisor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Visor");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 3;
            item.defense = 8;
            item.value = 9000;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DoomiteBreastplate") && legs.type == mod.ItemType("DoomiteGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"20% increased ranged damage";
            player.rangedDamage += .2f;
            player.GetModPlayer<AAPlayer>(mod).doomite = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}