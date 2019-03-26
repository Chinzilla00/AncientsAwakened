using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    [AutoloadEquip(EquipType.Shield)]
    public class BulwarkOfChaos : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 2;
            item.expert = true;
            item.accessory = true;
            item.defense = 3;
        }
        public override void SetStaticDefaults()
        {            DisplayName.SetDefault("Bulwark Of Chaos");
            Tooltip.SetDefault(
@"For every hit you land on an enemy, 5 true damage (damage unassigned to any class) is dealt
Allows you to dash into enemies, damaging them");
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
            player.dash = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ClawOfChaos", 1);
            recipe.AddIngredient(ItemID.EoCShield, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<Retriever.StormClaw>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<ClawOfChaos>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}