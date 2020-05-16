using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{

    public class GenocideCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genocide Cannon");
            Tooltip.SetDefault(@"Fires highly explosive Rockets
25% chance to fire a glitched rocket that explodes into frag rockets");
        }

        public override void SetDefaults()
        {
            item.damage = 400;
            item.ranged = true;
            item.width = 66;
            item.height = 28;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 10f;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 24f;
            item.shoot = mod.ProjectileType("GRocket");
            item.useAmmo = AmmoID.Rocket;
            item.rare = ItemRarityID.Cyan;
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
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = Main.rand.Next(4) == 0 ? mod.ProjectileType("GRocket2") : mod.ProjectileType("GRocket");
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}