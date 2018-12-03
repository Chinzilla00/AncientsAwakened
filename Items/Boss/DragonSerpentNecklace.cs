using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss
{
    public class DragonSerpentNecklace : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragontamer's Cloak");
            Tooltip.SetDefault(@"10% Increased Damage Resistance
12% Increased damage
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
            player.endurance *= 1.1f;
            player.meleeDamage *= 1.12f;
            player.rangedDamage *= 1.12f;
            player.magicDamage *= 1.12f;
            player.minionDamage *= 1.12f;
            player.thrownDamage *= 1.12f;
            player.GetModPlayer<AAPlayer>(mod).clawsOfChaos = true;
        }
    }
    
}