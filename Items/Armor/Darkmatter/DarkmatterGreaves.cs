using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Legs)]
	public class DarkmatterGreaves : ModItem
	{
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {

            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Armor/Darkmatter/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            item.glowMask = customGlowMask;
            DisplayName.SetDefault("Darkmatter Greaves");
			Tooltip.SetDefault(@"24% increased movement speed
Dark, yet still barely visible");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 300000;
			item.rare = 11;
			item.defense = 24;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.24f;
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 27);
            recipe.AddIngredient(null, "DarkEnergy", 15);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}