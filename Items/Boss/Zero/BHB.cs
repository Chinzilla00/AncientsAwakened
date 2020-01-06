using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{

    public class BHB : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blackhole Blaster");
            Tooltip.SetDefault(@"this weapon shoots in a 3 round burst and randomly shoots a homing rocket that explode");
        }

        public override void SetDefaults()
        {
            item.damage = 350;
            item.ranged = true;
            item.width = 66;
            item.height = 28;
            item.useTime = 5;
            item.useAnimation = 15;
            item.reuseDelay = 10;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 10f;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/BHB");
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("RealityLaser");
            item.useAmmo = AmmoID.Bullet;
            item.rare = 9;
            AARarity = 13;
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
			return new Vector2(-6, 0);
		}
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = Main.rand.Next(6) == 0 ? mod.ProjectileType("BHBR") : mod.ProjectileType("RealityLaser");
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.VortexBeater, 1);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}