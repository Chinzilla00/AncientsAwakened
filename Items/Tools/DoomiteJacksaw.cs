using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DoomiteJacksaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Doomite Jacksaw");
            Tooltip.SetDefault("Engineered for ultimate tree and wall breaking action!");
		}

		public override void SetDefaults()
		{
			item.damage = 18;
			item.melee = true;
			item.width = 50;
			item.height = 18;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.useAnimation = 15;
			item.useTime = 12;
			item.hammer = 70;
			item.axe = 30;
			item.useStyle = 5;
			item.knockBack = 0;
			item.value = 15000;
			item.rare = 4;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DoomiteJacksaw");
			item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Doomite", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}