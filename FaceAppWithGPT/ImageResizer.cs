using System.IO.Abstractions;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg; // Include this if you're saving as JPEG

public class ImageResizer
{
    private readonly IFileSystem _fileSystem;

    public ImageResizer(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public async Task ResizeImageAsync(string sourcePath, string destinationPath, int width, int height)
    {
        using var image = await Image.LoadAsync(_fileSystem.File.OpenRead(sourcePath));
        image.Mutate(x => x.Resize(width, height));
        await image.SaveAsync(_fileSystem.File.OpenWrite(destinationPath), new JpegEncoder()); // Specify JPEG encoder
    }
}