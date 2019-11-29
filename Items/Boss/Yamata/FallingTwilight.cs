using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.Projectiles.Yamata;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Yamata
{
    public class FallingTwilight : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Falling Twilight");
        }

        public override void SetDefaults()
        {
            item.damage = 170;
            item.ranged = true;
            item.width = 44;
            item.height = 76;
            item.useAnimation = 15;
            item.useTime = 5;
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 1;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Arrow;
            item.rare = 9; AARarity = 13;
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

        public override bool ConsumeAmmo(Player player)
        {
            return !(player.itemAnimation < item.useAnimation - 1);
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 20f * 0.1f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 4; i++)
            {
                float randomSpeed = baseSpeed - Main.rand.NextFloat() * 1f;
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, randomSpeed * (float)Math.Sin(offsetAngle), randomSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, item.owner);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(ItemID.Tsunami);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
