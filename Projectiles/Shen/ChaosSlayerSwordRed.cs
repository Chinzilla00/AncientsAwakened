using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Shen
{
    public class ChaosSlayerSwordRed : ChaosSlayerSword
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blade of Wrath");
		}

        public override void SetDefaults()
        {
           base.SetDefaults();
		   swordType = 1;
		   offsetLeft = false;
		}	
    }
}