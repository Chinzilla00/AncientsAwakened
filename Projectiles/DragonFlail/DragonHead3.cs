using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace GRealm.Projectiles.Melee
{
	public class TrifectalBall3 : TrifectalBall
	{
		public override string Texture
		{
			get
			{
				return "GRealm/Projectiles/Melee/TrifectalBall";;
			}
		}		

		public override void SetStaticDefaults()
		{
            displayName = "Flare Ball";
		}		

        public override void SetDefaults()
        {
			base.SetDefaults();
			ballType = 2;
        }
	}
}
