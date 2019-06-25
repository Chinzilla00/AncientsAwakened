using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ChaosChain : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Chaos Chain");
            Tooltip.SetDefault(@"Throws a volitile sphere of chaotic energy");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.knockBack = 15f;
            item.width = 20;
            item.height = 20;
            item.damage = 90;
            item.shoot = mod.ProjectileType("ChaosChain");
            item.shootSpeed = 14f;
            item.UseSound = SoundID.Item10;
            item.rare = 8;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.melee = true;
            item.noMelee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Ryusei", 1);
            recipe.AddIngredient(null, "ChaosCrystal",1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}