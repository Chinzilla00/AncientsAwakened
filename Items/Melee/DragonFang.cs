using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee
{
    public class DragonFang : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fang");
            Tooltip.SetDefault("Right click to slash at your foes with the grace of a Valkyrie");
        }

        public override void SetDefaults()
        {
            item.damage = 160;
            item.width = 48;
            item.height = 46;
            item.useTime = 4;
            item.useAnimation = 4;
            item.knockBack = 3;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("MadnessSlash");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.channel = false;
                item.useAnimation = 15;
                item.useTime = 15;
                item.useStyle = 1;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType<Projectiles.AsgardianIce>();
            }
            else
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.channel = true;
                item.useAnimation = 25;
                item.useTime = 5;
                item.useStyle = 5;
                item.autoReuse = false;
                item.shoot = mod.ProjectileType<Projectiles.ValkyrieSlash>();
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IceLongsword");
            recipe.AddIngredient(ItemID.Arkhalis);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
