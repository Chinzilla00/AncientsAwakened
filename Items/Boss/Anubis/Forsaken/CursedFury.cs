using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class CursedFury : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Flamefury");
			Tooltip.SetDefault("50% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			item.damage = 70;
			item.ranged = true;
			item.width = 80;
			item.height = 38;
			item.useTime = 5;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item34;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.autoReuse = true;
			item.shoot = mod.ProjectileType("ForsakenFlame");
			item.shootSpeed = 10f;
			item.useAmmo = 23;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
	        for (int index = 0; index < 2; ++index)
	        {
	            float SpeedX = speedX + Main.rand.Next(-25, 26) * 0.05f;
	            float SpeedY = speedY + Main.rand.Next(-25, 26) * 0.05f;
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
	    	return false;
		}

	    public override bool ConsumeAmmo(Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 50)
	    		return false;
	    	return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SandstormThrower>(), 1);
			recipe.AddIngredient(null, "SoulFragment", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
