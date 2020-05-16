using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class ChaosScythe : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 350;
            item.melee = true;           
            item.width = 56;              
            item.height = 56;          
            item.knockBack = 6;
            item.value = 300000;
            item.autoReuse = true;   
            item.useTurn = false;
            item.expert = true; item.expertOnly = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.shootSpeed = 5;
            item.shoot = mod.ProjectileType("ChaosScythe");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Final Chaos");
            Tooltip.SetDefault(@"'I CAN DO ANYTHING'
Legendary Weapon");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 7f;
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
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
