using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TerraBladeEX : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Terra Blade");//<--- Item name here
			Tooltip.SetDefault(@"Shoots homing projectiles that inflict terrablaze
Terra Blade EX");
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
			item.shootSpeed = 15f;
			item.knockBack = 7f;
			item.melee = true;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.autoReuse = true;
			item.crit = 8;
            item.expert = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
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
            recipe.AddIngredient(mod, "TerraCrystal", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

