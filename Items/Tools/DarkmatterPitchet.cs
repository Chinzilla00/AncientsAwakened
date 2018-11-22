using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class DarkmatterPitchet : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkmatter Pitchet");
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Tools/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }


        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 54;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 11;
		    item.pick = 235;
            item.axe = 200;
            item.tileBoost += 4;

            item.damage = 60;
            item.knockBack = 4;

            item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 19;

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.glowMask = customGlowMask;
            item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 20);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}