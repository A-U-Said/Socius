namespace Socius.Helpers
{
	public static partial class AttachmentHelper
	{
		public static string? GetAttachmentType(string mediaUrl)
		{
			if (string.IsNullOrEmpty(mediaUrl))
			{
				return null;
			}

			var mediaUri = new Uri(mediaUrl);
			string mediaExtension = Path.GetExtension(mediaUri.AbsolutePath);

			//https://www.facebook.com/business/help/523719398041952?id=1240182842783684
			//https://www.facebook.com/help/218673814818907
			//https://www.converter365.com/blog/what-file-formats-does-instagram-support/
			return mediaExtension.ToLower() switch
			{
				".bmp" or
				".dib" or
				".gif" or
				".heic" or
				".heif" or
				".iff" or
				".jfif" or
				".jp2" or
				".jpe" or
				".jpeg" or
				".jpg" or
				".png" or
				".psd" or
				".tif" or
				".tiff" or
				".wbmp" or
				".webp" or
				".xbm" => "photo",
				".3g2" or
				".3gp" or
				".3gpp" or
				".asf" or
				".avi" or
				".dat" or
				".divx" or
				".dv" or
				".f4v" or
				".flv" or
				".m2ts" or
				".m4v" or
				".mkv" or
				".mod" or
				".mov" or
				".mp4" or
				".mpe" or
				".mpeg" or
				".mpeg4" or
				".mpg" or
				".mts" or
				".nsv" or
				".ogm" or
				".ogv" or
				".qt" or
				".tod" or
				".ts" or
				".vob" or
				".wmv" => "video",
				_ => "Unknown"
			};
		}
	}
}
