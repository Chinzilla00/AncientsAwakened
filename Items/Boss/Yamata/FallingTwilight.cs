using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
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
            item.useAnimation = 18;
            item.useTime = 18;
            item.reuseDelay = 0;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 20f;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
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
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(4);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * -45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 projectileOffset = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) *5f;
                projectileOffset.X *= MathHelper.Lerp(0.8f, 1.2f, (float)Main.rand.NextDouble());
                projectileOffset.Y *= MathHelper.Lerp(0.8f, 1.2f, (float)Main.rand.NextDouble());
                Vector2 newSpeed = new Vector2(speedX, speedY) * MathHelper.Lerp(0.8f, 1.2f, (float)Main.rand.NextDouble());
                Projectile.NewProjectile(position.X + projectileOffset.X, position.Y + projectileOffset.Y, newSpeed.X, newSpeed.Y, ModContent.ProjectileType<Projectiles.Yamata.NightSoul>(), damage, knockBack, player.whoAmI);
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
