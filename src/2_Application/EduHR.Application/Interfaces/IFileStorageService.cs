using System.IO;
using System.Threading.Tasks;

namespace EduHR.Application.Interfaces;

/// <summary>
/// Defines the contract for a file storage service.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Uploads a file to the storage.
    /// </summary>
    /// <param name="fileStream">The stream of the file to upload.</param>
    /// <param name="fileName">The desired file name in the storage.</param>
    /// <param name="containerName">The container or folder to store the file in.</param>
    /// <returns>The public URL or path to the uploaded file.</returns>
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string containerName);

    /// <summary>
    /// Deletes a file from the storage.
    /// </summary>
    /// <param name="fileUrl">The URL or path of the file to delete.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteFileAsync(string fileUrl);
}