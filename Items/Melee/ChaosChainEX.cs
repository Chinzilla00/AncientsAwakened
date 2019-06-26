using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ChaosChainEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Perfect Chaos Chain");
            Tooltip.SetDefault(@"Fires a spinning blade that shreds enemies
Chaos Chain EX");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.knockBack = 1f;
            item.width = 30;
            item.height = 10;
            item.damage = 250;
            item.shoot = mod.ProjectileType("ChaosChainEX");
            item.shootSpeed = 18f;
            item.UseSound = SoundID.Item116;
            item.rare = 9;
            item.expert = true;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosChain", 1);
            recipe.AddIngredient(null, "EXSoul",1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}