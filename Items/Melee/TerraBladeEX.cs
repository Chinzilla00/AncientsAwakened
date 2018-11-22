using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;//<---and this are used by the glowmask code.

namespace AAMod.Items.Melee
{
    public class TerraBladeEX : ModItem
	{
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Terra Blade");//<--- Item name here
			Tooltip.SetDefault(@"Terra Blade EX");
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
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
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.useStyle = 1;
			item.damage = 400;
			item.useAnimation = 12;
			item.useTime = 12;
			item.width = 62;
			item.height = 74;
			item.shoot = mod.ProjectileType("TerraShotEX");
			item.shootSpeed = 25f;
			item.knockBack = 7f;
			item.melee = true;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.autoReuse = true;
			item.crit = 8;
			item.glowMask = customGlowMask;
		}

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Terrablaze"), 600);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

