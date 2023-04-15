using Furion.DependencyInjection;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Yi.Framework.Module.ImageSharp;
public class ImageSharpManager:ISingleton
{
    public void ImageCompress(string fileName, Stream stream, string savePath)
    {
        var extensionName = Path.GetExtension(fileName).ToLower();
        if (extensionName == ".png")
        {
            PngImageCompress(stream, savePath);
        }
        else if (extensionName == ".jpg" || extensionName == ".jpeg")
        {
            JpgImageCompress(stream, savePath);
        }
        else
        {
            using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

    }

    public void PngImageCompress(Stream stream, string savePath)
    {
        using (var image = Image.Load(stream))
        {
            var encoder = new PngEncoder()
            {
                CompressionLevel = PngCompressionLevel.Level6,

            };
            if (image.Width > 300)
            {
                image.Mutate(a => a.Resize(image.Width / 2, image.Height / 2));
            }

            image.Save(savePath, encoder);
        }
    }
    public void JpgImageCompress(Stream stream, string savePath)
    {
        using (var image = Image.Load(stream))
        {
            var encoder = new JpegEncoder()
            {
                Quality = 30
            };
            if (image.Width > 300)
            {
                image.Mutate(a => a.Resize(image.Width / 2, image.Height / 2));
            }


            image.Save(savePath, encoder);
        }
    }

}