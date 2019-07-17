using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Lolkat : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Lolkat");
            Tooltip.SetDefault(@"WHAT DOES IT MEAN?!?!?!111!!11
Meowmere EX");
        }

        public override void SetDefaults()
        {

            item.damage = 550;
            item.melee = true;
            item.width = 64;
            item.height = 70;
            item.useTime = 10;
            item.useAnimation = 10;     
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.UseSound = new LegacySoundStyle(2, 57, Terraria.Audio.SoundType.Sound);
            item.autoReuse = true;
            item.useTurn = true;
            item.expert = true; item.expertOnly = true;
			item.shoot = 502;
			item.shootSpeed = 12f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
            glowmaskDrawType = GLOWMASKTYPE_SWORD;
            glowmaskDrawColor = Color.White;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 30f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 2; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Meowmere);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
