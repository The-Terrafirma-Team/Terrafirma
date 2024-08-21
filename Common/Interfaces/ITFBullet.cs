using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrafirma.Common.Interfaces
{
    /// <summary>
    /// Interface for Bullet items. Currently only used for specifying custom casing textures
    /// </summary>
    public interface ITFBullet
    {
        Asset<Texture2D> casingTexture { get; }
    }
}
