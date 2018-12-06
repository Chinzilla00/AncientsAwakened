using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class ChaosScythe : ModItem
    {
        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.damage = 250;
            item.melee = true;           
            item.width = 56;              
            item.height = 56;          
            item.knockBack = 6;
            item.value = 10;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = false;
            item.useAnimation = 25;
            item.useTime = 28;
            item.shootSpeed = 5;
            item.shoot = mod.ProjectileType("ChaosScythe");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Scythe");
            Tooltip.SetDefault(@"'I CAN DO ANYTHING'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 2f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(new Color(85, 145, 93), new Color(64, 61, 99), Pie);
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = color1;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DeathSickle, 1);
            recipe.AddIngredient(ItemID.IceSickle, 1);
            recipe.AddIngredient(ItemID.Sickle, 1); ;
            recipe.AddIngredient(null, "Discord", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
