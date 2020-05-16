using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class ShootingStar : BaseAAItem
    {

        public override void SetDefaults()
        {
            item.damage = 100;
            item.noMelee = true;
            item.ranged = true;
            item.width = 36;
            item.height = 64;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileID.Shuriken;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 5;
            item.value = 10000;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 22f;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 12f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("RadiumArrow"), damage, knockBack, Main.myPlayer);
            }
            return true;
        }

        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shooting Star");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 16);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
