using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class Dawnstrike : BaseAAItem
    {
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dawnstrike");
            Tooltip.SetDefault(@"Charge the blaster to fire larger flame blasts");
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.damage = 600;
            item.noMelee = true;
            item.ranged = true;
            item.width = 74;
            item.height = 24;
            item.useTime = 65;
            item.useAnimation = 65;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("Dawnstrike");
            item.channel = true;
            item.knockBack = 12;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.shootSpeed = 8f;
            item.crit += 5;
            item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
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
            return new Vector2(-4, -2);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(99) < 49)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "TheVulcano");
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
