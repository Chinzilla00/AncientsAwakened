using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss
{
    public class DragonSerpentNecklace : ModItem
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
            item.expert = true;
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

        public override void UpdateEquip(Player player)
        {
            player.endurance += .07f;
            player.meleeDamage += .07f;
            player.rangedDamage += .07f;
            player.magicDamage += .07f;
            player.minionDamage += .07f;
            player.thrownDamage += .07f;
            player.GetModPlayer<AAPlayer>(mod).clawsOfChaos = true;
        }
    }
    
}