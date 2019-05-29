using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DynaskullJavelin : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynaskull Javelin");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            item.shoot = mod.ProjectileType("DynaskullJavelin");
            item.shootSpeed = 12f;
            item.damage = 40;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.width = 30;
            item.height = 30;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 5000;
            item.rare = 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BoneJavelin, 500);
            recipe.AddIngredient(ItemID.IceBoomerang);
            recipe.AddIngredient(null, "DragonFlamebow");
            recipe.AddIngredient(null, "DoomiteHolobow");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
