using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Kraken
{
    public class Tentacle2 : Tentacle1
    {
		public override string Texture
		{
			get
			{
				return "AAMod/NPCs/Bosses/Kraken/Tentacle1";
			}
		}			
		
        public override void SetDefaults()
        {
			base.SetDefaults();
			leftHand = false;
        }
	}
}
