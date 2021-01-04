using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BeatSaverDownloader.Misc
{
    internal class Sprites
    {
        public static Sprite AddToFavorites;
        public static Sprite RemoveFromFavorites;
        public static Sprite StarFull;
        public static Sprite StarEmpty;
        public static Sprite DoubleArrow;

        public static Sprite ReviewIcon;

        //https://www.flaticon.com/free-icon/thumbs-up_70420
        public static Sprite ThumbUp;

        //https://www.flaticon.com/free-icon/dislike-thumb_70485
        public static Sprite ThumbDown;

        //https://www.flaticon.com/free-icon/playlist_727239
        public static Sprite PlaylistIcon;

        //https://www.flaticon.com/free-icon/musical-note_727218
        public static Sprite SongIcon;

        //https://www.flaticon.com/free-icon/download_724933
        public static Sprite DownloadIcon;

        //https://www.flaticon.com/free-icon/media-play-symbol_31128
        public static Sprite PlayIcon;

        //https://game-icons.net/1x1/delapouite/perspective-dice-six-faces-three.html
        public static Sprite RandomIcon;

        //https://www.flaticon.com/free-icon/waste-bin_70388
        public static Sprite DeleteIcon;

        public static Sprite BeatSaverIcon;
        public static Sprite ScoreSaberIcon;
        //by elliotttate#9942
        public static Sprite BeastSaberLogo;
        public static Sprite BeastSaberLogoSmall;
        public static void ConvertToSprites()
        {
            Plugin.log.Info("Creating sprites...");

            AddToFavorites = LoadSpriteFromResources("BeatSaverDownloader.Assets.AddToFavorites.png");
            RemoveFromFavorites = LoadSpriteFromResources("BeatSaverDownloader.Assets.RemoveFromFavorites.png");
            StarFull = LoadSpriteFromResources("BeatSaverDownloader.Assets.StarFull.png");
            StarEmpty = LoadSpriteFromResources("BeatSaverDownloader.Assets.StarEmpty.png");
            BeastSaberLogo = LoadSpriteFromResources("BeatSaverDownloader.Assets.BeastSaberLogo.png");
            BeastSaberLogoSmall = LoadSpriteFromResources("BeatSaverDownloader.Assets.BeastSaberLogoShrunk.png");
            ReviewIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.ReviewIcon.png");
            ThumbUp = LoadSpriteFromResources("BeatSaverDownloader.Assets.ThumbUp.png");
            ThumbDown = LoadSpriteFromResources("BeatSaverDownloader.Assets.ThumbDown.png");
            PlaylistIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.PlaylistIcon.png");
            SongIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.SongIcon.png");
            DownloadIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.DownloadIcon.png");
            PlayIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.PlayIcon.png");
            DoubleArrow = LoadSpriteFromResources("BeatSaverDownloader.Assets.DoubleArrow.png");
            RandomIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.RandomIcon.png");
            DeleteIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.DeleteIcon.png");
            BeatSaverIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.BeatSaver.png");
            ScoreSaberIcon = LoadSpriteFromResources("BeatSaverDownloader.Assets.ScoreSaber.png");

            Plugin.log.Info("Creating sprites... Done!");
        }

        public static string SpriteToBase64(Sprite input)
        {
            return Convert.ToBase64String(input.texture.EncodeToPNG());
        }

        public static Sprite Base64ToSprite(string input)
        {
            string base64 = input;
            if (input.Contains(","))
            {
                base64 = input.Substring(input.IndexOf(','));
            }
            Texture2D tex = Base64ToTexture2D(base64);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), (Vector2.one / 2f));
        }

        public static Texture2D Base64ToTexture2D(string encodedData)
        {
            byte[] imageData = Convert.FromBase64String(encodedData);
            Texture2D Tex2D = new Texture2D(2, 2);
            if (Tex2D.LoadImage(imageData))
                return Tex2D;
            else
                return null;
            //    Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false, true);
            //    texture.hideFlags = HideFlags.HideAndDontSave;
            //    texture.filterMode = FilterMode.Trilinear;
            //    texture.LoadImage(imageData);
            //    return texture;
        }

        // Image helpers

        public static Texture2D LoadTextureRaw(byte[] file)
        {
            if (file.Count() > 0)
            {
                Texture2D Tex2D = new Texture2D(2, 2);
                if (Tex2D.LoadImage(file))
                    return Tex2D;
            }
            return null;
        }

        public static Texture2D LoadTextureFromFile(string FilePath)
        {
            if (File.Exists(FilePath))
                return LoadTextureRaw(File.ReadAllBytes(FilePath));

            return null;
        }

        public static Texture2D LoadTextureFromResources(string resourcePath)
        {
            return LoadTextureRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath));
        }

        public static Sprite LoadSpriteRaw(byte[] image, float PixelsPerUnit = 100.0f)
        {
            return LoadSpriteFromTexture(LoadTextureRaw(image), PixelsPerUnit);
        }

        public static Sprite LoadSpriteFromTexture(Texture2D SpriteTexture, float PixelsPerUnit = 100.0f)
        {
            if (SpriteTexture)
                return Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            return null;
        }

        public static Sprite LoadSpriteFromFile(string FilePath, float PixelsPerUnit = 100.0f)
        {
            return LoadSpriteFromTexture(LoadTextureFromFile(FilePath), PixelsPerUnit);
        }

        public static Sprite LoadSpriteFromResources(string resourcePath, float PixelsPerUnit = 100.0f)
        {
            return LoadSpriteRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath), PixelsPerUnit);
        }

        public static byte[] GetResource(Assembly asm, string ResourceName)
        {
            System.IO.Stream stream = asm.GetManifestResourceStream(ResourceName);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return data;
        }
    }
}