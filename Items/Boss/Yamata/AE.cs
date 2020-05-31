using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class AE : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Eruption");
			Tooltip.SetDefault(@"spews out abyssal acid that will cause enamys to explode of killed by its visious acid, also uses gel");
		}

	    public override void SetDefaults()
	    {
            item.damage = 350;
            item.ranged = true;
            item.width = 76;
            item.height = 34;
            item.useTime = 2;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item34;
            item.value = 1000000;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AcidFlame"); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 20f;
            item.useAmmo = 23;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("AcidFlame"), damage, knockBack, player.whoAmI);
            }
            shoot++;

            if (shoot % 6 != 0) return false;

            if (shoot >= 6)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AcidFlame"), damage * 2, knockBack, player.whoAmI);
                shoot = 0;
            }
            shoot = 0;
            return false;
        }

	
	    public override void AddRecipes()
	    {
	        ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "EventideAbyssium", 5);
	        recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "Toxithrower");
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
	    }
	}
}
