using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss
{
    public class StormCharm : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Charm");
            Tooltip.SetDefault(@"20% Increased Damage Resistance
40% Increased damage
10% Increased melee speed
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
            recipe.AddIngredient(null, "HoloCape", 1);
            recipe.AddIngredient(null, "StormPendant", 1);
            recipe.AddIngredient(null, "DragonSerpentNecklace", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.1f;
            player.meleeDamage *= 1.40f;
            player.rangedDamage *= 1.40f;
            player.magicDamage *= 1.40f;
            player.minionDamage *= 1.40f;
            player.thrownDamage *= 1.40f;
            player.GetModPlayer<AAPlayer>(mod).clawsOfChaos = true;
        }
    }
    
}