using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class Astroid : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Asteroid");
            Tooltip.SetDefault(@"Crashes into enemies with the force of an astroid crashing into earth
Inflicts Discordian Inferno");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Shen;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.rare = 1;

            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 7.5F;
            item.damage = 290;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("Astroid");
            item.shootSpeed = 20F;
            item.UseSound = SoundID.Item20;
            item.melee = true;
            item.autoReuse = true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Daycrusher", 1);
            recipe.AddIngredient(null, "Flairdra", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}