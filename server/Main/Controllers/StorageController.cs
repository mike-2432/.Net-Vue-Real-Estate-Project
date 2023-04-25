using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Main.DTO;
using server.Main.Service;

namespace server.Main.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StorageController : ControllerBase
  {
    private readonly IAzureStorage _storage;
    public StorageController(IAzureStorage storage)
    {
      _storage = storage;
    }

    // GET ALL //
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      // Get all files at the Azure Storage Location and return them
      List<BlobDto>? files = await _storage.ListAsync();
      return Ok(files);
    }

    // GET SINGLE //
    [HttpGet("{filename}")]
    public async Task<IActionResult> GetSingle(string filename)
    {
      BlobDto? file = await _storage.DownloadAsync(filename);

      // Check if file was found, otherwise return an error to the client
      if (file == null)
        return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");

      // Return the file to the client
      return File(file.Content!, file.ContentType!, file.Name);
    }

    // UPLOAD //
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
      BlobResponseDto? response = await _storage.UploadAsync(file);
      // Throw an error if something went wrong
      if (response.Error)
        return StatusCode(StatusCodes.Status500InternalServerError, response.Status);

      // Return a response message to the client
      return Ok(response.Status);
    }

    // UPDATE //
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] IFormFile file)
    {
      BlobResponseDto? response = await _storage.UpdateAsync(file);
      // Throw an error if something went wrong
      if (response.Error)
        return StatusCode(StatusCodes.Status500InternalServerError, response.Status);

      // Return a response message to the client
      return Ok(response.Status);
    }

    // DELETE //
    [HttpDelete("{filename}")]
    public async Task<IActionResult> Delete(string filename)
    {
      BlobResponseDto response = await _storage.DeleteAsync(filename);

      // Throw an error if something went wrong
      if (response.Error)
        return StatusCode(StatusCodes.Status500InternalServerError, response.Status);

      // File has been successfully deleted
      return Ok(response.Status);
    }
  }
}