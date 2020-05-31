using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

using Terraria.DataStructures;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dyes
{
    public class DoomsdayDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomsday Dye");
            BaseUtility.AddTooltips(item, new string[] { "Adds a glitchy-look to whatever this dye is applied to" });
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 7));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 15;
            item.maxStack = 99;
            item.rare = ItemRarityID.Yellow;
            item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(item.type);
            item.value = BaseUtility.CalcValue(0, 10, 0, 0);
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "UnstableSingularity", 3);
            recipe.AddIngredient(Terraria.ID.ItemID.BottledWater);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}