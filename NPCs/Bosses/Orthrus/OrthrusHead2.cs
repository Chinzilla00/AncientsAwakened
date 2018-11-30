using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class OrthrusHead2 : OrthrusHead1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            leftHead = true;
        }
    }
}