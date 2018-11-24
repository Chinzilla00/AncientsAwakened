using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Body)]
	public class DarkmatterBreastplate : ModItem
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
			DisplayName.SetDefault("Darkmatter Breastplate");
			Tooltip.SetDefault(@"20% increased damage
Dark, yet still barely visible");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 300000;
			item.rare = 11;
			item.defense = 36;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.20f;
            player.rangedDamage *= 1.20f;
            player.magicDamage *= 1.20f;
            player.minionDamage *= 1.20f;
            player.thrownDamage *= 1.20f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarkMatter", 30);
            recipe.AddIngredient(null, "DarkEnergy", 20);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}