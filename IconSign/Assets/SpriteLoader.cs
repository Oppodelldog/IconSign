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

        private static Sprite LoadSpriteFromEmbeddedResource(string resourceName)
        {
            // Get the assembly where the resource is embedded
            var assembly = Assembly.GetExecutingAssembly();

            // Load the PNG file as a stream
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    Debug.LogError($"Failed to find resource: {resourceName}");
                    return null;
                }

                // Read the stream into a byte array  
                var buffer = new byte[stream.Length];
                var read = stream.Read(buffer, 0, buffer.Length);
                if (read != buffer.Length)
                {
                    Debug.LogError($"Failed to read resource: {resourceName}");
                    return null;
                }

                // Create a Texture2D from the byte array
                var texture = new Texture2D(2, 2); // Size will be overwritten by LoadImage
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