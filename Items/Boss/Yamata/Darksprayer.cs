using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class Darksprayer : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darksprayer");
            Tooltip.SetDefault(@"'Spouts of dark, leaves its mark'
Inflicts Moonrazed");           
        }

        public override void SetDefaults()
        {
            item.damage = 425;
            item.ranged = true;
            item.width = 44;
            item.height = 34;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.useAmmo = AmmoID.Rocket;
            item.knockBack = 8f;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item38;      
            item.autoReuse = true;   
            item.shootSpeed = 20f;
            item.shoot = mod.ProjectileType("Moonblow");
            item.rare = 9; AARarity = 13;
            item.noMelee = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Moonblow"), damage, knockBack, player.whoAmI, 0, 1);
            return false;
        }
	
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(ItemID.SnowmanCannon);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
