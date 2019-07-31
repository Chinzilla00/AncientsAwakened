using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Chaos : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos");
			Tooltip.SetDefault("Wrath and fury upon those struck by this discordian blade");
        }
		public override void SetDefaults()
		{
            
			item.damage = 140;
			item.melee = true;
			item.width = 84;
			item.height = 84;
			item.useTime = 23;
			item.useAnimation = 23;
			item.useStyle = 1;
			item.knockBack = 10;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChaosShot");
            item.shootSpeed = 14f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "TrueBlazingDawn", 1);
			recipe.AddIngredient(mod, "TrueAbyssalTwilight", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 500);
			target.AddBuff(BuffID.Venom, 500);
        }
	}
}
