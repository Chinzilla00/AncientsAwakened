using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BreakingDawn : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Breaking Dawn");
        }

		public override void SetDefaults()
		{
            
			item.damage = 200;
			item.melee = true;
			item.width = 60;
			item.height = 60;
			item.useTime = 18;
            item.shoot = mod.ProjectileType("MorningStar");
            item.shootSpeed = 10f;
            item.useAnimation = 18;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 500000;
			item.UseSound = new LegacySoundStyle(2, 15, Terraria.Audio.SoundType.Sound);
			item.autoReuse = true;
            item.rare = 11;
		}


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.StarDust>(), 0f, 0f, 46, default, 1.25f);
			dust.noGravity = true;
        }
	}
}
