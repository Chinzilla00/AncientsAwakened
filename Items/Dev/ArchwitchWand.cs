using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class ArchwitchWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Wand");
            Tooltip.SetDefault(@"An old wand. It seems to have not been used recently.");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 120;
            item.magic = true;
            item.mana = 5;
            item.width = 56;
            item.height = 56;
            item.useTime = 20;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.rare = 11;
            item.UseSound = new LegacySoundStyle(2, 105, Terraria.Audio.SoundType.Sound);
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ArchwitchStorm");
            item.shootSpeed = 7f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(121, 21, 214);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CatsEyeRifle");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}