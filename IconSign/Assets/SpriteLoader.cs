using System.IO;
using System.Reflection;
using UnityEngine;

namespace IconSign.Assets
{
    public static class SpriteLoader
    {
        public static Sprite LoadBuildPieceIcon()
        {
            return LoadSpriteFromEmbeddedResource("IconSign.Assets.prefab-icon.png");
        }

        public static Sprite LoadSpriteFromEmbeddedResource(string resourceName)
        {
            // Get the assembly where the resource is embedded
            var assembly = Assembly.GetExecutingAssembly();

            // Load the PNG file as a stream
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    Debug.LogError($"Failed to find resource: {resourceName}");
                    return null;
                }

                // Read the stream into a byte array  
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                // Create a Texture2D from the byte array
                Texture2D texture = new Texture2D(2, 2); // Size will be overwritten by LoadImage
                texture.LoadImage(buffer); // LoadImage will automatically set the texture size

                // Create and return a Sprite from the Texture2D
                return Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f) // Set the pivot at the center
                );
            }
        }
    }
}