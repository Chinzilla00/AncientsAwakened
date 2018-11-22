using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueCopperShortsword : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Copper Shortsword");
			Tooltip.SetDefault("Literally just did it for the memes");
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Melee/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }
		public override void SetDefaults()
		{
            item.glowMask = customGlowMask;
			item.damage = 500;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 28;
			item.useAnimation = 40;
			item.useStyle = 3;
			item.knockBack =20;
			item.value = 10000000;
			item.rare = 9;
			item.expert = true;
			item.UseSound = SoundID.Item37;
			item.autoReuse = false;
            item.shoot = mod.ProjectileType("TrueCopperShot");
            item.shootSpeed = 20f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperShortsword, 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
