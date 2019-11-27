using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss
{
    public class DragonSerpentNecklace : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Serpent Necklace");
            Tooltip.SetDefault(@"7% increased damage and damage resistance
Ignores 5 Enemy defense");
        }
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 54;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 2;
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
            item.defense = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonCape", 1);
            recipe.AddIngredient(null, "HydraPendant", 1);
            recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += .07f;
            player.allDamage += .07f;
            player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<Broodmother.DragonCape>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<Hydra.HydraPendant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}