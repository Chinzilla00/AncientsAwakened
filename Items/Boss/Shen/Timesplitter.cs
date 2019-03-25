using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class Timesplitter : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Timesplitter");
            Tooltip.SetDefault(@"It has been said that this spear was used to divide time into day and night
Inflicts Daybroken and Moonraze
UNRELEASED");
        }

        public override void SetDefaults()
        {
            item.damage = 222;
            item.melee = true;
            item.width = 96;
            item.height = 96;
            item.scale = 1.1f;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 4.7f;
            item.UseSound = SoundID.Item20;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
			item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 8;
            item.shoot = mod.ProjectileType("TimesplitterP");  //put your Spear projectile name
            item.shootSpeed = 7f;
        }


        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RealityBar"), 5);
            recipe.AddIngredient(mod.ItemType("AbyssalYari"));
			recipe.AddIngredient(mod.ItemType("SunSpear"));
            recipe.AddTile(mod.TileType("ACS"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
