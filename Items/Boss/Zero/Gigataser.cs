using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Gigataser : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigataser");
            Tooltip.SetDefault(@"Fires void lightning");
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = false;
            item.damage = 100;
            item.noMelee = true;
            item.ranged = true;
            item.width = 74;
            item.height = 24;
            item.useTime = 45;
            item.useAnimation = 45; 
            item.useStyle = 5;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Shock");
            item.shoot = mod.ProjectileType("ZeroTaze");
            item.knockBack = 12;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.shootSpeed = 12f;
            item.crit += 5;
            item.rare = 9;
            AARarity = 13;
            item.autoReuse = true;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int num842 = 0; num842 < 3; num842++)
            {
                Vector2 vector82 = new Vector2(speedX, speedY);
                float ai = Main.rand.Next(100);
                Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.6)) * 14f;
                Projectile.NewProjectile(position.X, position.Y, vector83.X * 2, vector83.Y * 2, mod.ProjectileType<Projectiles.Zero.ZeroTaze>(), damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(null, "FulguriteTazerblaster");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}